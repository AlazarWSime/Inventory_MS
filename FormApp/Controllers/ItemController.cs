using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;   // ← Important
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class ItemController
    {
        public List<ItemModel> GetAllItems()
        {
            List<ItemModel> items = new List<ItemModel>();

            string query = @"
        SELECT i.Item_ID, i.Item_Name, i.Description, i.Price, 
               i.Reorder_Level, i.Total_Quantity, 
               i.Category_ID, i.Supplier_ID,
               c.Category_Name, s.Supplier_Name
        FROM Item i
        LEFT JOIN Item_Category c ON i.Category_ID = c.Category_ID
        LEFT JOIN Supplier s ON i.Supplier_ID = s.Supplier_ID
        ORDER BY i.Item_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    items.Add(new ItemModel
                    {
                        Item_ID = Guid.Parse(row["Item_ID"].ToString()),
                        Item_Name = row["Item_Name"].ToString(),
                        Description = row["Description"]?.ToString(),
                        Price = Convert.ToDecimal(row["Price"]),
                        Reorder_Level = Convert.ToInt32(row["Reorder_Level"]),
                        Total_Quantity = Convert.ToInt32(row["Total_Quantity"]),   // ← Updated for INT
                        Category_ID = Guid.Parse(row["Category_ID"].ToString()),
                        Supplier_ID = Guid.Parse(row["Supplier_ID"].ToString()),
                        Category_Name = row["Category_Name"]?.ToString() ?? "N/A",
                        Supplier_Name = row["Supplier_Name"]?.ToString() ?? "N/A"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return items;
        }


        public bool CreateItem(ItemModel item)
        {
            try
            {
                string query = @"
    INSERT INTO Item 
        (Item_ID, Item_Name, Description, Price, Reorder_Level, 
         Total_Quantity, Category_ID, Supplier_ID)   -- ← Add this
    VALUES 
        (NEWID(), @Item_Name, @Description, @Price, @Reorder_Level, 
         @Total_Quantity, @Category_ID, @Supplier_ID)";   // ← Add this

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Item_Name", item.Item_Name),
                    new SqlParameter("@Description", item.Description ?? (object)DBNull.Value),
                    new SqlParameter("@Price", item.Price),
                    new SqlParameter("@Reorder_Level", item.Reorder_Level),
                    new SqlParameter("@Category_ID", item.Category_ID),
                    new SqlParameter("@Supplier_ID", item.Supplier_ID),
                    new SqlParameter("@Total_Quantity", item.Total_Quantity),
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating item: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool UpdateItem(ItemModel item)
        {
            try
            {
                string query = @"
            UPDATE Item 
            SET Item_Name = @Item_Name,
                Description = @Description,
                Price = @Price,
                Reorder_Level = @Reorder_Level,
                Total_Quantity = @Total_Quantity,
                Category_ID = @Category_ID,
                Supplier_ID = @Supplier_ID
            WHERE Item_ID = @Item_ID";

                SqlParameter[] parameters =
                {
            new SqlParameter("@Item_ID", item.Item_ID),
            new SqlParameter("@Item_Name", item.Item_Name),
            new SqlParameter("@Description", item.Description ?? (object)DBNull.Value),
            new SqlParameter("@Price", item.Price),
            new SqlParameter("@Total_Quantity", item.Total_Quantity),
            new SqlParameter("@Reorder_Level", item.Reorder_Level),
            new SqlParameter("@Category_ID", item.Category_ID),
            new SqlParameter("@Supplier_ID", item.Supplier_ID)
        };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating item: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        // ====================== DELETE ITEM ======================
        public bool DeleteItem(Guid itemId)
        {
            try
            {
                string query = "DELETE FROM Item WHERE Item_ID = @Item_ID";

                SqlParameter[] parameters =
                {
            new SqlParameter("@Item_ID", itemId)
        };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting item: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}