using FormApp.Controllers;
using FormApp.Session;
using System;
using System.Windows.Forms;

namespace FormApp.Controllers
{
    public class AccountController
    {
        private readonly UserController _userController = new UserController();

        public bool Login(string username, string password)
        {
            try
            {
                string query = @"
                    SELECT User_ID, Username, Role_ID, Full_Name, Password 
                    FROM [User] 
                    WHERE Username = @Username";

                // Note: Better to use UserController method, but for direct login:
                // You already have logic in Form1, we can move it here later.

                // For now, returning success - you can integrate with existing login
                return true; // Placeholder - connect to your existing login logic
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message);
                return false;
            }
        }

        public bool ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            // Implement with BCrypt verification + update
            MessageBox.Show("Change Password feature coming soon.");
            return false;
        }
    }
}