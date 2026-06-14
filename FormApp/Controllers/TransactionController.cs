using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class TransactionController
    {
        public bool RecordTransaction(StockTransactionModel transaction)
        {
            SqlTransaction dbTransaction = null;

            try
            {
                if (!transaction.Zone_ID.HasValue)
                {
                    MessageBox.Show("Please select a stock zone.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (transaction.Quantity <= 0)
                {
                    MessageBox.Show("Quantity must be greater than zero.", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (DBConnection._Connection.State != ConnectionState.Open)
                    DBConnection.Initiate();

                dbTransaction = DBConnection._Connection.BeginTransaction();

                decimal quantityChange = GetQuantityChange(transaction.Transaction_Type, transaction.Quantity);

                if (quantityChange < 0 && !HasEnoughStock(transaction.Item_ID, transaction.Zone_ID.Value, Math.Abs(quantityChange), dbTransaction))
                {
                    MessageBox.Show("Not enough stock in the selected zone.", "Stock",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dbTransaction.Rollback();
                    return false;
                }

                string insertTransactionSql = @"
                    INSERT INTO Stock_Transaction 
                        (Transaction_ID, User_ID, Item_ID, Transaction_Type, 
                         Quantity, Transaction_Date, Zone_ID)
                    VALUES 
                        (NEWID(), @User_ID, @Item_ID, @Transaction_Type, 
                         @Quantity, GETDATE(), @Zone_ID)";

                using (SqlCommand cmd = new SqlCommand(insertTransactionSql, DBConnection._Connection, dbTransaction))
                {
                    cmd.Parameters.AddWithValue("@User_ID", transaction.User_ID);
                    cmd.Parameters.AddWithValue("@Item_ID", transaction.Item_ID);
                    cmd.Parameters.AddWithValue("@Transaction_Type", transaction.Transaction_Type);
                    cmd.Parameters.AddWithValue("@Quantity", transaction.Quantity);
                    cmd.Parameters.AddWithValue("@Zone_ID", transaction.Zone_ID.Value);
                    cmd.ExecuteNonQuery();
                }

                string updateStockSql = @"
                    IF EXISTS (
                        SELECT 1 FROM Inventory_Stock
                        WHERE Item_ID = @Item_ID AND Zone_ID = @Zone_ID
                    )
                    BEGIN
                        UPDATE Inventory_Stock
                        SET Quantity_On_Hand = Quantity_On_Hand + @QuantityChange
                        WHERE Item_ID = @Item_ID AND Zone_ID = @Zone_ID
                    END
                    ELSE
                    BEGIN
                        INSERT INTO Inventory_Stock
                            (Stock_ID, Item_ID, Zone_ID, Quantity_On_Hand)
                        VALUES
                            (NEWID(), @Item_ID, @Zone_ID, @QuantityChange)
                    END";

                using (SqlCommand cmd = new SqlCommand(updateStockSql, DBConnection._Connection, dbTransaction))
                {
                    cmd.Parameters.AddWithValue("@Item_ID", transaction.Item_ID);
                    cmd.Parameters.AddWithValue("@Zone_ID", transaction.Zone_ID.Value);
                    cmd.Parameters.AddWithValue("@QuantityChange", quantityChange);
                    cmd.ExecuteNonQuery();
                }

                string updateItemTotalSql = @"
                    UPDATE Item
                    SET Total_Quantity = ISNULL((
                        SELECT SUM(Quantity_On_Hand)
                        FROM Inventory_Stock
                        WHERE Item_ID = @Item_ID
                    ), 0)
                    WHERE Item_ID = @Item_ID";

                using (SqlCommand cmd = new SqlCommand(updateItemTotalSql, DBConnection._Connection, dbTransaction))
                {
                    cmd.Parameters.AddWithValue("@Item_ID", transaction.Item_ID);
                    cmd.ExecuteNonQuery();
                }

                dbTransaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (dbTransaction != null)
                    dbTransaction.Rollback();

                MessageBox.Show("Error recording transaction: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                if (dbTransaction != null)
                    dbTransaction.Dispose();
            }
        }

        private decimal GetQuantityChange(string transactionType, decimal quantity)
        {
            string normalizedType = (transactionType ?? "").Trim().ToLowerInvariant();

            if (normalizedType == "sale" || normalizedType == "stock out" || normalizedType == "transfer")
                return -quantity;

            return quantity;
        }

        private bool HasEnoughStock(Guid itemId, Guid zoneId, decimal requiredQuantity, SqlTransaction dbTransaction)
        {
            string query = @"
                SELECT ISNULL(Quantity_On_Hand, 0)
                FROM Inventory_Stock
                WHERE Item_ID = @Item_ID AND Zone_ID = @Zone_ID";

            using (SqlCommand cmd = new SqlCommand(query, DBConnection._Connection, dbTransaction))
            {
                cmd.Parameters.AddWithValue("@Item_ID", itemId);
                cmd.Parameters.AddWithValue("@Zone_ID", zoneId);

                object result = cmd.ExecuteScalar();
                decimal currentQuantity = result == null || result == DBNull.Value ? 0 : Convert.ToDecimal(result);

                return currentQuantity >= requiredQuantity;
            }
        }

        public List<StockTransactionModel> GetAllTransactions()
        {
            List<StockTransactionModel> transactions = new List<StockTransactionModel>();

            string query = @"
                SELECT t.Transaction_ID, t.User_ID, t.Item_ID, t.Transaction_Type, 
                       t.Quantity, t.Transaction_Date, t.Zone_ID,
                       i.Item_Name, u.Username, z.Zone_Name
                FROM Stock_Transaction t
                LEFT JOIN Item i ON t.Item_ID = i.Item_ID
                LEFT JOIN [User] u ON t.User_ID = u.User_ID
                LEFT JOIN Location z ON t.Zone_ID = z.Zone_ID
                ORDER BY t.Transaction_Date DESC";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    transactions.Add(new StockTransactionModel
                    {
                        Transaction_ID = Guid.Parse(row["Transaction_ID"].ToString()),
                        User_ID = Guid.Parse(row["User_ID"].ToString()),
                        Item_ID = Guid.Parse(row["Item_ID"].ToString()),
                        Zone_ID = row["Zone_ID"] == DBNull.Value ? (Guid?)null : Guid.Parse(row["Zone_ID"].ToString()),
                        Transaction_Type = row["Transaction_Type"].ToString(),
                        Quantity = Convert.ToDecimal(row["Quantity"]),
                        Transaction_Date = Convert.ToDateTime(row["Transaction_Date"]),
                        Item_Name = row["Item_Name"]?.ToString() ?? "N/A",
                        Username = row["Username"]?.ToString() ?? "N/A",
                        Zone_Name = row["Zone_Name"]?.ToString() ?? "N/A"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return transactions;
        }
    }
}
