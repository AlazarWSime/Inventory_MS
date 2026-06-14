using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class StockController
    {
        public List<InventoryStockModel> GetAllStock()
        {
            List<InventoryStockModel> stock = new List<InventoryStockModel>();

            string query = @"
                SELECT s.Stock_ID, s.Item_ID, s.Zone_ID, s.Quantity_On_Hand,
                       i.Item_Name, l.Zone_Name
                FROM Inventory_Stock s
                JOIN Item i ON s.Item_ID = i.Item_ID
                JOIN Location l ON s.Zone_ID = l.Zone_ID
                ORDER BY i.Item_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    stock.Add(new InventoryStockModel
                    {
                        Stock_ID = Guid.Parse(row["Stock_ID"].ToString()),
                        Item_ID = Guid.Parse(row["Item_ID"].ToString()),
                        Zone_ID = Guid.Parse(row["Zone_ID"].ToString()),
                        Quantity_On_Hand = Convert.ToDecimal(row["Quantity_On_Hand"]),
                        Item_Name = row["Item_Name"].ToString(),
                        Zone_Name = row["Zone_Name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stock: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return stock;
        }

        public bool UpdateStock(Guid itemId, Guid zoneId, decimal quantity)
        {
            try
            {
                string query = @"
                    MERGE Inventory_Stock AS target
                    USING (VALUES (@Item_ID, @Zone_ID, @Quantity)) 
                    AS source (Item_ID, Zone_ID, Quantity)
                    ON target.Item_ID = source.Item_ID AND target.Zone_ID = source.Zone_ID
                    WHEN MATCHED THEN 
                        UPDATE SET Quantity_On_Hand = source.Quantity
                    WHEN NOT MATCHED THEN 
                        INSERT (Stock_ID, Item_ID, Zone_ID, Quantity_On_Hand)
                        VALUES (NEWID(), source.Item_ID, source.Zone_ID, source.Quantity);";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Item_ID", itemId),
                    new SqlParameter("@Zone_ID", zoneId),
                    new SqlParameter("@Quantity", quantity)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating stock: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}