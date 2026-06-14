using FormApp.DB;
using FormApp.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace FormApp.Controllers
{
    public class UserController
    {
        public bool CreateUser(string fullName, string username, string password, Guid roleId, Guid? createdBy)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

                string query = @"
                    INSERT INTO [User] 
                        (User_ID, Role_ID, Full_Name, Username, Password, Created_By, Created_On)
                    VALUES 
                        (NEWID(), @Role_ID, @Full_Name, @Username, @Password, @Created_By, GETDATE())";

                SqlParameter[] parameters =
                {
                    new SqlParameter("@Role_ID", roleId),
                    new SqlParameter("@Full_Name", fullName),
                    new SqlParameter("@Username", username),
                    new SqlParameter("@Password", hashedPassword),
                    new SqlParameter("@Created_By", (object)createdBy ?? DBNull.Value)
                 //The expression (object)createdBy ?? DBNull.Value does this:                 
                 //?? → This is called the Null - Coalescing Operator.
                 //It means:
                 //"If createdBy is not null, use it. If it is null, use DBNull.Value instead."
                 //(object)createdBy → Converts the Guid? to object(required by SqlParameter).
                };

                int result = DBConnection.ExecuteNonQuery(query, parameters);

                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public List<RoleModel> GetAllRoles()
        {
            List<RoleModel> roles = new List<RoleModel>();

            string query = "SELECT Role_ID, Role_Name FROM Role ORDER BY Role_Name";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);  

                foreach (DataRow row in dt.Rows)//mekdes
                {
                    roles.Add(new RoleModel
                    {
                        Role_ID = Guid.Parse(row["Role_ID"].ToString()),
                        Role_Name = row["Role_Name"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return roles;
        }


        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new List<UserModel>();

            string query = @"
        SELECT u.User_ID, u.Role_ID, u.Full_Name, u.Username, 
               u.Created_On, r.Role_Name
        FROM [User] u
        LEFT JOIN Role r ON u.Role_ID = r.Role_ID
        ORDER BY u.Created_On DESC";

            try
            {
                DataTable dt = DBConnection.ExecuteQuery(query);

                foreach (DataRow row in dt.Rows)
                {
                    users.Add(new UserModel
                    {
                        User_ID = Guid.Parse(row["User_ID"].ToString()),
                        Role_ID = Guid.Parse(row["Role_ID"].ToString()),
                        Full_Name = row["Full_Name"].ToString(),
                        Username = row["Username"].ToString(),
                        Created_On = Convert.ToDateTime(row["Created_On"]),
                        Role_Name = row["Role_Name"]?.ToString() ?? "N/A"
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message, "Database Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return users;
        }



        // ====================== UPDATE USER ======================
        public bool UpdateUser(UserModel user)
        {
            try
            {
                string query = @"
            UPDATE [User] 
            SET Full_Name = @Full_Name, 
                Username = @Username, 
                Role_ID = @Role_ID
            WHERE User_ID = @User_ID";

                SqlParameter[] parameters =
                {
            new SqlParameter("@User_ID", user.User_ID),
            new SqlParameter("@Full_Name", user.Full_Name),
            new SqlParameter("@Username", user.Username),
            new SqlParameter("@Role_ID", user.Role_ID)
        };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // ====================== DELETE USER ======================
        public bool DeleteUser(Guid userId)
        {
            try
            {
                string query = "DELETE FROM [User] WHERE User_ID = @User_ID";

                SqlParameter[] parameters =
                {
            new SqlParameter("@User_ID", userId)
        };

                int result = DBConnection.ExecuteNonQuery(query, parameters);
                return result > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting user: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        public void Changepassword() { }

    }
}
