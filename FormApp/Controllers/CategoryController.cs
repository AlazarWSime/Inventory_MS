using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class CategoryController
    {
        // ================== READ ==================
        public List<ItemCategoryModel> GetAllCategories()
        {
            List<ItemCategoryModel> categories = new List<ItemCategoryModel>();

            string query = "SELECT Category_ID, Category_Name FROM Item_Category ORDER BY Category_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    categories.Add(new ItemCategoryModel
                    {
                        Category_ID = Guid.Parse(row["Category_ID"].ToString()),
                        Category_Name = row["Category_Name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return categories;
        }

        // ================== CREATE ==================
        public bool AddCategory(string categoryName)
        {
            try
            {
                string query = "INSERT INTO Item_Category (Category_ID, Category_Name) VALUES (NEWID(), @Category_Name)";

                SqlParameter[] parameters = { new SqlParameter("@Category_Name", categoryName.Trim()) };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding category: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== UPDATE ==================
        public bool UpdateCategory(Guid categoryId, string newCategoryName)
        {
            try
            {
                string query = "UPDATE Item_Category SET Category_Name = @Category_Name WHERE Category_ID = @Category_ID";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Category_Name", newCategoryName.Trim()),
                    new SqlParameter("@Category_ID", categoryId)
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating category: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ================== DELETE ==================
        public bool DeleteCategory(Guid categoryId)
        {
            try
            {
                string query = "DELETE FROM Item_Category WHERE Category_ID = @Category_ID";

                SqlParameter[] parameters = { new SqlParameter("@Category_ID", categoryId) };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting category: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}