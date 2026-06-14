using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormApp.Controllers;
using FormApp.Models;
using FormApp.Combobox;

namespace FormApp.UserControls
{
    public partial class EditUserControl : UserControl
    {
        private UserModel _currentUser;
        public DialogResult Result { get; private set; } = DialogResult.Cancel;

        // Dragging variables
        private bool isDragging = false;
        private Point startingPoint = new Point(0, 0);

        public EditUserControl(UserModel user)
        {
            InitializeComponent();

            if (user == null)
            {
                MessageBox.Show("Error: No user data received.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Load += (s, e) => this.ParentForm?.Close();
                return;
            }

            _currentUser = user;
        }

        private void EditUserControl_Load(object sender, EventArgs e)
        {
            if (_currentUser == null) return;

            LoadRolesIntoComboBox();
            LoadUserData();
        }

        // ====================== DRAGGING ======================
        private void EditUserControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startingPoint = e.Location;
            }
        }

        private void EditUserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point newPos = new Point(
                    this.Location.X + (e.X - startingPoint.X),
                    this.Location.Y + (e.Y - startingPoint.Y)
                );
                this.Location = newPos;
            }
        }

        private void EditUserControl_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // ====================== RESIZING ======================
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            // Optional: Keep buttons at bottom when resized
            ConSaveUserBtn.Location = new Point(ConSaveUserBtn.Location.X, this.Height - 70);
            ConDeleteBtnSA.Location = new Point(ConDeleteBtnSA.Location.X, this.Height - 70);
            ConClearBtnSA.Location = new Point(ConClearBtnSA.Location.X, this.Height - 70);
        }

        private void LoadRolesIntoComboBox()
        {
            UserController controller = new UserController();
            var roles = controller.GetAllRoles();

            ConRoleNameComboBox.DataSource = roles;
            ConRoleNameComboBox.DisplayMember = "Role_Name";
            ConRoleNameComboBox.ValueMember = "Role_ID";
        }

        private void LoadUserData()
        {
            ConFull_NameBox.Text = _currentUser.Full_Name;
            ConUsernameBoxUM.Text = _currentUser.Username;

            if (_currentUser.Role_ID != Guid.Empty)
                ConRoleNameComboBox.SelectedValue = _currentUser.Role_ID;
        }
        private void RoleNameComboBoxSA_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConSaveUserBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ConFull_NameBox.Text) || string.IsNullOrEmpty(ConFull_NameBox.Text))
            {
                ConErrMsgLbl_EditUser.Text = "Full Name and Username are required";
                ConErrMsgLbl_EditUser.ForeColor = Color.Firebrick;
                return;
            }

            _currentUser.Full_Name = ConFull_NameBox.Text.Trim();
            _currentUser.Username = ConUsernameBoxUM.Text.Trim();
            _currentUser.Role_ID = (Guid)ConRoleNameComboBox.SelectedValue;

            UserController controller = new UserController();
            bool success = controller.UpdateUser(_currentUser);

            if (success)
            {
                ConErrMsgLbl_EditUser.Text = "User updated successfully!";
                ConErrMsgLbl_EditUser.ForeColor = Color.LimeGreen;
                Result = DialogResult.OK;
                this.ParentForm?.Close();   // Close the form containing this control
            }
            else
            {
                ConErrMsgLbl_EditUser.Text = "Failed to update user";
                ConErrMsgLbl_EditUser.ForeColor = Color.Firebrick;
            }
        }

        private void ConDeleteBtnSA_Click(object sender, EventArgs e)
        {
            if (_currentUser == null) return;

            // Confirmation dialog
            DialogResult confirm = MessageBox.Show(
                $"Are you sure you want to delete user '{_currentUser.Full_Name}'?\n\nThis action cannot be undone.",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                UserController controller = new UserController();
                bool success = controller.DeleteUser(_currentUser.User_ID);

                if (success)
                {
                    MessageBox.Show("User deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Result = DialogResult.OK;
                    this.ParentForm?.Close();   // Close the edit window
                }
                else
                {
                    MessageBox.Show("Failed to delete user. Please try again.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
