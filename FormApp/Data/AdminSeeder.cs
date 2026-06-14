using FormApp.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp.Data
{
    class AdminSeeder
    {
        private readonly Guid RoleID = new Guid("124a1384-5056-4a08-9844-f9dc43435b78");
        private readonly Guid UserID = new Guid("22c3fcf7-722b-4ae3-aaff-7472a0bb0305");

        private readonly string HashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword("Admin@123");
        //Password is generated at runtime that why i used readonly
        public void SeedAdmin()
        { 
            SqlCommand chkCmd = null;
            SqlCommand cmd = null;
            SqlTransaction transaction = null;

            try
            {
                DBConnection.Initiate();

                // check if admin already exists
                string chkSql = @"SELECT COUNT(*) FROM [User] WHERE User_ID = @UserID;";

                using (chkCmd = new SqlCommand(chkSql, DBConnection._Connection)) //The using statement is used to automatically clean up resources (like database connections, commands, files, etc.) after you're done using them.
                {
                    chkCmd.Parameters.AddWithValue("@UserID", UserID);


                    int count = Convert.ToInt32(chkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // Admin already exists
                        return;
                    }
                }


                // start Transaction
                transaction = DBConnection._Connection.BeginTransaction();

                string insertSql = @"
                    INSERT INTO [User] 
                        (User_ID, Role_ID, Full_Name, Username, Password, Created_By, Created_On)
                    VALUES 
                        (@User_ID, @Role_ID, @Full_Name, @Username, @Password, @Created_By, GETDATE())";


                using (cmd = new SqlCommand(insertSql, DBConnection._Connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@User_ID", UserID);
                    cmd.Parameters.AddWithValue("@Role_ID", RoleID);
                    cmd.Parameters.AddWithValue("@Full_Name", "Super Administrator");
                    cmd.Parameters.AddWithValue("@Username", "Super_Admin@0305");
                    cmd.Parameters.AddWithValue("@Password", HashedPassword);
                    cmd.Parameters.AddWithValue("@Created_By", DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        transaction.Commit(); //If all operations succeed → tr.Commit() (save all changes)
                        MessageBox.Show("SystemAdmin account seeded successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                  
                        transaction.Rollback();
                    
                    
                }

                MessageBox.Show($"Record Creation Failed\n{ex.Message}", "Failure",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {


                if (transaction != null)
                {
                    transaction.Dispose();
                    transaction = null;
                }

                // Optional: Close connection only if you want (not always necessary)
                if (DBConnection._Connection.State == System.Data.ConnectionState.Open)
                    DBConnection._Connection.Close();
            }
        }
    }
}
//it checkes wheather there is an admin the adds the admi if it doesn't existis