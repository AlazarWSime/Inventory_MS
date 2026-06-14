using FormApp.Combobox;
using FormApp.Controllers;
using FormApp.DB;
using FormApp.Models;
using FormApp.Session;
using FormApp.UserControls;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormApp
{
    public partial class Inventory_MS : Form
    {
        public Inventory_MS()
        {
            InitializeComponent();
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void LoadRolesIntoComboBox()
        {
            try
            {
                UserController controller = new UserController();
                List<RoleModel> roles = controller.GetAllRoles();

                RoleNameComboBoxSA.Items.Clear();//Usually before adding new items to avoid duplicates. ".clear()"

                if (roles == null || roles.Count == 0)
                {

                    RoleNameComboBoxSA.Items.Add("No roles available");
                    return;
                }

                foreach (RoleModel role in roles)
                {
                    RoleNameComboBoxSA.Items.Add(new ComboBoxItem
                    {
                        Text = role.Role_Name,
                        Value = role.Role_ID
                    });
                }

                if (RoleNameComboBoxSA.Items.Count > 0)
                    RoleNameComboBoxSA.SelectedIndex = 0;   // Select first role by default
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading roles: " + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new FormApp.Data.AdminSeeder().SeedAdmin();
            Adim_Panel.Visible = false;

            UserTableGridView6.DataSource = null;
            SupplierTableGridView.DataSource = null;
            ItemTableGridViewIM.DataSource = null;

            LoadUsersIntoGrid();
            LoadRolesIntoComboBox();

            LoadCategoriesIntoComboBox();
            LoadSuppliersIntoComboBox();
            LoadItemsIntoGrid();
            LoadSuppliersIntoGrid();

            //DASHBOARD 
            LoadDashboardStats();

            LoadTransactionsIntoGrid();

            LoadTransactionComboBoxes();

                
                LoadCategoryFilter();        
                

            }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
            string Username = UsernameBox.Text.Trim();
            string Password = PasswordBox.Text.Trim();

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ErrMsgLbl_Login.Text = "Please provide both Username and Password";
                ErrMsgLbl_Login.ForeColor = Color.Firebrick;
                return;
            }

            string query = @"
        SELECT u.User_ID, u.Password, u.Role_ID, r.Role_Name, u.Full_Name
        FROM [User] u
        LEFT JOIN Role r ON u.Role_ID = r.Role_ID
        WHERE u.Username = @Username";

            DBConnection.Initiate();

            using (SqlCommand cmd = new SqlCommand(query, DBConnection._Connection))
            {
                cmd.Parameters.AddWithValue("@Username", Username);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string storedHash = reader["Password"].ToString();
                        bool isValid = BCrypt.Net.BCrypt.EnhancedVerify(Password, storedHash);

                        if (isValid)
                        {
                            // Set Session
                            UserSession.User_ID = reader.GetGuid(reader.GetOrdinal("User_ID"));
                            UserSession.Username = Username;
                            UserSession.Full_Name = reader["Full_Name"]?.ToString() ?? "";
                            UserSession.Role_Name = reader["Role_Name"]?.ToString() ?? "Unknown";
                            UserSession.Role_ID = reader.GetGuid(reader.GetOrdinal("Role_ID"));

                            ErrMsgLbl_Login.Text = "Logged in successfully!";
                            ErrMsgLbl_Login.ForeColor = Color.LimeGreen;

                            Adim_Panel.Visible = true;
                            tabControl1.Visible = true;
                            LoginPanel.Visible = false;

                            ApplyRoleBasedTabs();        // ← Important
                        }
                        else
                        {
                            ErrMsgLbl_Login.Text = "Invalid Username or Password";
                            ErrMsgLbl_Login.ForeColor = Color.Firebrick;
                        }
                    }
                    else
                    {
                        ErrMsgLbl_Login.Text = "Invalid Username or Password";
                        ErrMsgLbl_Login.ForeColor = Color.Firebrick;
                    }
                }
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignIn_pannel_Paint(object sender, PaintEventArgs e)
        {


        }

        private void RoleBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserName_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void SignUp_PageLbl_Click(object sendeer, EventArgs e)
        {

        }



        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Dashbord_SystemAdmin_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Adim_Panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == null) return;

            switch (tabControl1.SelectedTab.Name)
            {
                case "User_Management":
                    LoadUsersIntoGrid();

                    break;

                case "Supplier_Management":
                    LoadSuppliersIntoGrid();
                    break;

                case "ItemEditor_InventoryManager":
                    LoadItemsIntoGrid();
                    LoadCategoriesIntoComboBox();
                    LoadSuppliersIntoComboBox();
                    break;

                case "Transactions_InventoryManager":
                    LoadTransactionsIntoGrid();
                    break;

                case "Dashboard_SystemAdmin":
                case "Dashboard_InventoryManager":
                    LoadDashboardStats();
                    break;

                case "Transactions_InvetoryManager":
                case "Transactions_WarehouseStaff":
                    LoadTransactionsIntoGrid();
                    LoadTransactionComboBoxes();
                    break;

                case "Inventory_InventoryManager":
                    LoadItemsIntoGrid();
                    LoadCategoryFilter();           // ← Add this
                    break;
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void User_Management_Click(object sender, EventArgs e)
        {
            // LoadUsersIntoGrid();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Supplier_Management_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void Inventory_InventoryManager_Click(object sender, EventArgs e)
        {

        }

        private void ItemEditor_InventroyManager_Click(object sender, EventArgs e)
        {

        }

        private void Transactions_InvetoryManager_Click(object sender, EventArgs e)
        {

        }

        private void Reposrts_InventoryManager_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_WarehouseStaff_Click(object sender, EventArgs e)
        {

        }

        private void ItemEditor_WarehouseStaff_Click(object sender, EventArgs e)
        {

        }

        private void Transactions_WarehouseStaff_Click(object sender, EventArgs e)
        {

        }

        private void Dashboard_SalesManager_Click(object sender, EventArgs e)
        {

        }

        private void Inventory_SalaesManager_Click(object sender, EventArgs e)
        {

        }

        private void Transactions_SalesManager_Click(object sender, EventArgs e)
        {

        }

        private void Reports_SalesManager_Click(object sender, EventArgs e)
        {

        }

        private void LoginPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ErrMsgLbl_Login_Click(object sender, EventArgs e)
        {

        }

        private void PasswordBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void passLab_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void ErrMsgLbl_Click(object sender, EventArgs e)
        {

        }

        private void UserName_Box_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void Passsword_Box_TextChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView6_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == UserTableGridView6.Columns["btnEditColumn"].Index)
            {
                UserModel selectedUser = (UserModel)UserTableGridView6.Rows[e.RowIndex].DataBoundItem;

                if (selectedUser == null) return;

                Form editForm = new Form
                {
                    Text = "Edit User",
                    Size = new Size(850, 650),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.Sizable,   // Allows resizing
                    MaximizeBox = false,                         // No Maximize button
                    MinimizeBox = false,                         // No Minimize button
                    Padding = new Padding(10)
                };

                EditUserControl editControl = new EditUserControl(selectedUser);
                editControl.Dock = DockStyle.Fill;
                Controls.Add(editControl);

                if (editForm.ShowDialog() == DialogResult.OK || editControl.Result == DialogResult.OK)
                {
                    LoadUsersIntoGrid();
                }
            }
        }




        private void LoadUsersIntoGrid()
        {
            try
            {
                UserController controller = new UserController();
                List<UserModel> users = controller.GetAllUsers();

                UserTableGridView6.DataSource = users;

                // ================== HIDE UNWANTED COLUMNS ==================
                string[] columnsToHide = { "Password", "Created_By", "User_ID", "Role_ID" };

                foreach (string colName in columnsToHide)
                {
                    if (UserTableGridView6.Columns[colName] != null)
                    {
                        UserTableGridView6.Columns[colName].Visible = false;
                    }
                }

                // Optional: Rename column headers for better readability
                if (UserTableGridView6.Columns["Full_Name"] != null)
                    UserTableGridView6.Columns["Full_Name"].HeaderText = "Full Name";

                if (UserTableGridView6.Columns["Username"] != null)
                    UserTableGridView6.Columns["Username"].HeaderText = "Username";

                if (UserTableGridView6.Columns["Role_Name"] != null)
                    UserTableGridView6.Columns["Role_Name"].HeaderText = "Role";

                if (UserTableGridView6.Columns["Created_On"] != null)
                    UserTableGridView6.Columns["Created_On"].HeaderText = "Created On";

                // Auto adjust column sizes
                UserTableGridView6.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + ex.Message);
            }
        }



        private void ClearFields()
        {
            Full_NameBox.Clear();
            UsernameBoxUM.Clear();
            PasswordBoxUM.Clear();

            // Optional: Reset ComboBox to first item
            if (RoleNameComboBoxSA.Items.Count > 0)
                RoleNameComboBoxSA.SelectedIndex = 0;

            // Clear error label
            ErrMsgLbl_CreateUser.Text = "";
        }



        private void SaveUserBtnSA_Click(object sender, EventArgs e)
        {
            // Clear previous message
            ErrMsgLbl_CreateUser.Text = "";   // ← Change to your actual Label name

            string fullName = Full_NameBox.Text.Trim();
            string username = UsernameBoxUM.Text.Trim();
            string password = PasswordBoxUM.Text.Trim();

            // Validation
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ErrMsgLbl_CreateUser.Text = "Please fill Full Name, Username and Password";
                ErrMsgLbl_CreateUser.ForeColor = Color.Firebrick;
                return;
            }

            if (username.Length < 4)
            {
                ErrMsgLbl_CreateUser.Text = "Username must be at least 4 characters long.";
                ErrMsgLbl_CreateUser.ForeColor = Color.Firebrick;
                return;
            }

            if (RoleNameComboBoxSA.SelectedItem == null)
            {
                ErrMsgLbl_CreateUser.Text = "Please select a Role";
                ErrMsgLbl_CreateUser.ForeColor = Color.Firebrick;
                return;
            }

            ComboBoxItem selectedItem = (ComboBoxItem)RoleNameComboBoxSA.SelectedItem;
            Guid selectedRoleId = selectedItem.Value;

            try
            {
                bool success = new UserController().CreateUser(
                    fullName,
                    username,
                    password,
                    selectedRoleId,
                    null
                );

                if (success)
                {
                    ErrMsgLbl_CreateUser.Text = "User created successfully!";
                    ErrMsgLbl_CreateUser.ForeColor = Color.LimeGreen;

                    // Clear input fields
                    ClearFields();

                    LoadUsersIntoGrid();
                }
                else
                {
                    ErrMsgLbl_CreateUser.Text = "Failed to create user. Please try again.";
                    ErrMsgLbl_CreateUser.ForeColor = Color.Firebrick;
                }
            }
            catch (Exception ex)
            {
                ErrMsgLbl_CreateUser.Text = "Error: " + ex.Message;
                ErrMsgLbl_CreateUser.ForeColor = Color.Firebrick;
            }
        }


        private void textBox17_TextChanged(object sender, EventArgs e)
        {

        }

        private void label29_Click(object sender, EventArgs e)
        {

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox17_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label29_Click_1(object sender, EventArgs e)
        {

        }




        private void dataGridView9_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void label44_Click(object sender, EventArgs e)
        {

        }

        private void label45_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click_1(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click_2(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_3(object sender, EventArgs e)
        {

        }

        private void TotalItemStringLbl_Click(object sender, EventArgs e)
        {

        }

        private void CategoriesNumLbl_Click(object sender, EventArgs e)
        {

        }

        private void TotalItemLblSA_Click(object sender, EventArgs e)
        {

        }


        private void DeleteBtnSA_Click(object sender, EventArgs e)
        {

        }

        private void ClearBtnSA_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        
        public void LoadItemsIntoGrid()
        {
            try
            {
                ItemController controller = new ItemController();
                List<ItemModel> items = controller.GetAllItems();

                ItemTableGridViewIM.DataSource = items;

                // Hide ID columns
                if (ItemTableGridViewIM.Columns["Item_ID"] != null)
                    ItemTableGridViewIM.Columns["Item_ID"].Visible = false;

                if (ItemTableGridViewIM.Columns["Category_ID"] != null)
                    ItemTableGridViewIM.Columns["Category_ID"].Visible = false;

                if (ItemTableGridViewIM.Columns["Supplier_ID"] != null)
                    ItemTableGridViewIM.Columns["Supplier_ID"].Visible = false;

                // Improve Column Headers
                if (ItemTableGridViewIM.Columns["Item_Name"] != null)
                    ItemTableGridViewIM.Columns["Item_Name"].HeaderText = "Item Name";

                if (ItemTableGridViewIM.Columns["Category_Name"] != null)
                    ItemTableGridViewIM.Columns["Category_Name"].HeaderText = "Category";

                if (ItemTableGridViewIM.Columns["Supplier_Name"] != null)
                    ItemTableGridViewIM.Columns["Supplier_Name"].HeaderText = "Supplier";

                if (ItemTableGridViewIM.Columns["Price"] != null)
                    ItemTableGridViewIM.Columns["Price"].DefaultCellStyle.Format = "N2";

                if (ItemTableGridViewIM.Columns["Reorder_Level"] != null)
                    ItemTableGridViewIM.Columns["Reorder_Level"].HeaderText = "Reorder Level";

                // NEW: Show Quantity Column
                if (ItemTableGridViewIM.Columns["Total_Quantity"] != null)
                {
                    ItemTableGridViewIM.Columns["Total_Quantity"].HeaderText = "Quantity";
                    ItemTableGridViewIM.Columns["Total_Quantity"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                }

                ItemTableGridViewIM.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (SearchBoxIM != null) SearchBoxIM.Clear();
                if (CatagoryFilterComboBox != null) CatagoryFilterComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying items: " + ex.Message);
            }
        }

        private void SaveBtnIM_Click(object sender, EventArgs e)
        {
            // Clear previous message
            ErrMsgLbl_Item.Text = "";
            ErrMsgLbl_Item.ForeColor = Color.Firebrick;

            string itemName = ItemNameBoxIM.Text.Trim();
            string description = DiscriptionBoxIM.Text.Trim();
            string priceText = PriceBoxIM.Text.Trim();
            string reorderText = ReorderLevelBoxIM.Text.Trim();
            string quantityText = ItemQuantityBoxIM.Text.Trim();   // ← New Quantity

            // Validation
            if (string.IsNullOrWhiteSpace(itemName))
            {
                ErrMsgLbl_Item.Text = "Item Name is required.";
                ItemNameBoxIM.Focus();
                return;
            }

            if (CategoryNamecomboBoxIM.SelectedValue == null)
            {
                ErrMsgLbl_Item.Text = "Please select a Category.";
                CategoryNamecomboBoxIM.Focus();
                return;
            }

            if (SupplierNameComboBoxIM1.SelectedValue == null)
            {
                ErrMsgLbl_Item.Text = "Please select a Supplier.";
                SupplierNameComboBoxIM1.Focus();
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || price <= 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Price (greater than 0).";
                PriceBoxIM.Focus();
                return;
            }

            if (!int.TryParse(reorderText, out int reorderLevel) || reorderLevel < 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Reorder Level.";
                ReorderLevelBoxIM.Focus();
                return;
            }

            // === Quantity Validation ===
            if (!int.TryParse(quantityText, out int quantity) || quantity < 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Quantity (0 or greater).";
                ItemQuantityBoxIM.Focus();
                return;
            }

            // Create Item Object
            ItemModel newItem = new ItemModel
            {
                Item_Name = itemName,
                Description = description,
                Price = price,
                Reorder_Level = reorderLevel,
                Total_Quantity = quantity,                    // ← Added
                Category_ID = (Guid)CategoryNamecomboBoxIM.SelectedValue,
                Supplier_ID = (Guid)SupplierNameComboBoxIM1.SelectedValue
            };

            ItemController controller = new ItemController();
            bool success = controller.CreateItem(newItem);

            if (success)
            {
                ErrMsgLbl_Item.Text = "Item created successfully!";
                ErrMsgLbl_Item.ForeColor = Color.LimeGreen;

                ClearItemFields();
                LoadItemsIntoGrid();

                RefreshDashboard();
                {
                    LoadDashboardStats();
                }
            }
            else
            {
                ErrMsgLbl_Item.Text = "Failed to create item. Please try again.";
            }
        }
        private void ClearItemFields()
        {
            ItemNameBoxIM.Clear();
            DiscriptionBoxIM.Clear();
            PriceBoxIM.Clear();
            ReorderLevelBoxIM.Clear();

            if (CategoryNamecomboBoxIM.Items.Count > 0)
                CategoryNamecomboBoxIM.SelectedIndex = 0;

            if (SupplierNameComboBoxIM1.Items.Count > 0)
                SupplierNameComboBoxIM1.SelectedIndex = 0;

            ErrMsgLbl_Item.Text = "";
        }
        private void CategoryNamecomboBoxIM_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        public void LoadCategoriesIntoComboBox()
        {
            try
            {
                CategoryController controller = new CategoryController();
                List<ItemCategoryModel> categories = controller.GetAllCategories();

                if (categories == null || categories.Count == 0)
                {
                    //ErrMsgLbl_Item.Text = "No categories found. Please add categories first.";
                    MessageBox.Show("No categories found in database. Please add some categories first.", "Info");
                    return;
                }

                CategoryNamecomboBoxIM.DataSource = categories;
                CategoryNamecomboBoxIM.DisplayMember = "Category_Name";
                CategoryNamecomboBoxIM.ValueMember = "Category_ID";

                if (CategoryNamecomboBoxIM.Items.Count > 0)
                    CategoryNamecomboBoxIM.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error");
            }
        }

        public void LoadSuppliersIntoComboBox()
        {
            try
            {
                SupplierController controller = new SupplierController();
                List<SupplierModel> suppliers = controller.GetAllSuppliers();

                if (suppliers == null || suppliers.Count == 0)
                {
                    MessageBox.Show("No suppliers found.", "Info");
                    return;
                }

                // ================== CHANGE THIS LINE ==================
                // Replace "SupplierNameComboBoxIM1" with your actual ComboBox name
                SupplierNameComboBoxIM1.DataSource = suppliers;
                SupplierNameComboBoxIM1.DisplayMember = "Supplier_Name";
                SupplierNameComboBoxIM1.ValueMember = "Supplier_ID";

                if (SupplierNameComboBoxIM1.Items.Count > 0)
                    SupplierNameComboBoxIM1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadSuppliersIntoGrid()
        {
            try
            {
                SupplierController controller = new SupplierController();
                List<SupplierModel> suppliers = controller.GetAllSuppliers();

                SupplierTableGridView.DataSource = suppliers;

                // Hide ID column
                if (SupplierTableGridView.Columns["Supplier_ID"] != null)
                    SupplierTableGridView.Columns["Supplier_ID"].Visible = false;

                // Improve column headers
                if (SupplierTableGridView.Columns["Supplier_Name"] != null)
                    SupplierTableGridView.Columns["Supplier_Name"].HeaderText = "Supplier Name";

                if (SupplierTableGridView.Columns["Contact_Email"] != null)
                    SupplierTableGridView.Columns["Contact_Email"].HeaderText = "Email";

                if (SupplierTableGridView.Columns["Contact_Phone"] != null)
                    SupplierTableGridView.Columns["Contact_Phone"].HeaderText = "Phone";

                SupplierTableGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSupplierFields()
        {
            SupplierNameBox.Clear();
            ContactEmailBoxSA.Clear();
            ContactPhoneBox.Clear();
            AddressBox.Clear();

            ErrMsgLbl_Supplier.Text = "";   // Clear error label
        }


        private void ItemTableGridViewIM_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Check if the user clicked the Edit button column
            if (e.ColumnIndex == ItemTableGridViewIM.Columns["btnEditItem"]?.Index)
            {
                ItemModel selectedItem = (ItemModel)ItemTableGridViewIM.Rows[e.RowIndex].DataBoundItem;

                if (selectedItem == null) return;

                // Open Edit Form with UserControl
                Form editForm = new Form
                {
                    Text = "Edit Item",
                    Size = new Size(850, 680),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                ItemEditorContol editControl = new ItemEditorContol(selectedItem);
                editControl.Dock = DockStyle.Fill;
                editForm.Controls.Add(editControl);

                if (editForm.ShowDialog() == DialogResult.OK || editControl.Result == DialogResult.OK)
                {
                    LoadItemsIntoGrid();   // Refresh the grid
                }
            }
        }

        private void SaveSupplierBtn_Click(object sender, EventArgs e)
        {
            ErrMsgLbl_Supplier.Text = "";

            string supplierName = SupplierNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(supplierName))
            {
                ErrMsgLbl_Supplier.Text = "Supplier Name is required.";
                SupplierNameBox.Focus();
                return;
            }

            SupplierModel supplier = new SupplierModel
            {
                Supplier_Name = supplierName,
                Contact_Email = ContactEmailBox.Text.Trim(),
                Contact_Phone = ContactPhoneBox.Text.Trim(),
                Address = AddressBox.Text.Trim()
            };

            SupplierController controller = new SupplierController();
            bool success = controller.CreateSupplier(supplier);

            if (success)
            {
                ErrMsgLbl_Supplier.Text = "Supplier saved successfully!";
                ErrMsgLbl_Supplier.ForeColor = Color.LimeGreen;

                ClearSupplierFields();
                LoadSuppliersIntoGrid();


                LoadSuppliersIntoComboBox();

                RefreshDashboard();
            }
            else
            {
                ErrMsgLbl_Supplier.Text = "Failed to save supplier.";
                ErrMsgLbl_Supplier.ForeColor = Color.Firebrick;
            }
        }

        private void DeleteItemBtnIM_Click(object sender, EventArgs e)
        {

        }

        private void ClearField()
        {
            ItemNameBoxIM.Clear();
            ItemQuantityBoxIM.Clear();
            PriceBoxIM.Clear();
            DiscriptionBoxIM.Clear();
            ReorderLevelBoxIM.Clear();


            // Optional: Reset ComboBox to first item
            if (CategoryNamecomboBoxIM.Items.Count > 0)
                CategoryNamecomboBoxIM.SelectedIndex = 0;
            if (SupplierNameComboBoxIM1.Items.Count > 0)
                SupplierNameComboBoxIM1.SelectedIndex = 0;
            // Clear error label
            ErrMsgLbl_Item.Text = "";
        }



        private void ClearBtnIM_Click(object sender, EventArgs e)
        {
            ClearField();
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            ClearSupplierFields();
        }

        private void SupplierTableGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == SupplierTableGridView.Columns["btnEditSupplier"].Index)
            {
                SupplierModel selectedSupplier = (SupplierModel)SupplierTableGridView.Rows[e.RowIndex].DataBoundItem;

                if (selectedSupplier == null) return;

                // Open Edit Form
                Form editForm = new Form
                {
                    Text = "Edit Supplier",
                    Size = new Size(720, 550),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                SupplierEditorControl editControl = new SupplierEditorControl(selectedSupplier);
                editControl.Dock = DockStyle.Fill;
                editForm.Controls.Add(editControl);

                if (editForm.ShowDialog() == DialogResult.OK || editControl.Result == DialogResult.OK)
                {
                    LoadSuppliersIntoGrid();   // Refresh grid
                    LoadSuppliersIntoComboBox(); // Refresh combobox
                }
            }
        }
        // ====================== DASHBOARD METHODS ======================

        // ====================== DASHBOARD - INVENTORY MANAGER ======================

        public void LoadDashboardStats()
        {
            try
            {
                ItemController itemController = new ItemController();
                CategoryController categoryController = new CategoryController();
                SupplierController supplierController = new SupplierController();

                // Total Items
                int totalItems = itemController.GetAllItems().Count;
                TotalItemNumLblSA.Text = totalItems.ToString("N0");

                // Total Categories
                int totalCategories = categoryController.GetAllCategories().Count;
                CategoriesNumLbl.Text = totalCategories.ToString("N0");

                // Total Suppliers (if you want to show it)
                int totalSuppliers = supplierController.GetAllSuppliers().Count;
                // You can add a label for this later if needed

                // Low Stock Items
                LoadLowStockItems();

                // TODO: Later - Total Transactions
                // TransactionsNumLblSA.Text = "0"; // Will implement when Transaction module is ready
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard: " + ex.Message,
                                "Dashboard", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadLowStockItems()
        {
            try
            {
                string query = @"
            SELECT 
                i.Item_Name AS [Item Name],
                i.Reorder_Level AS [Reorder Level],
                ISNULL(SUM(s.Quantity_On_Hand), 0) AS [Current Stock]
            FROM Item i
            LEFT JOIN Inventory_Stock s ON i.Item_ID = s.Item_ID
            GROUP BY i.Item_Name, i.Reorder_Level
            HAVING ISNULL(SUM(s.Quantity_On_Hand), 0) <= i.Reorder_Level 
                OR ISNULL(SUM(s.Quantity_On_Hand), 0) = 0
            ORDER BY [Current Stock] ASC";

                DataTable dt = DBConnection.ExecuteQuery(query);

                // Update Low Stock Count
                LowStockNumLblSA.Text = dt.Rows.Count.ToString("N0");

                // Update Alert Message
                if (dt.Rows.Count > 0)
                {
                    Reorder_LeveAlertLbl.Text = $"{dt.Rows.Count} items are below their Reorder Level - action needed";
                    Reorder_LeveAlertLbl.ForeColor = Color.DarkRed;
                }
                else
                {
                    Reorder_LeveAlertLbl.Text = "All items are well stocked ✓";
                    Reorder_LeveAlertLbl.ForeColor = Color.Green;
                }

                // Load data into Low Stock Grid
                if (LowStockTableGridView != null)
                {
                    LowStockTableGridView.DataSource = dt;
                    LowStockTableGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading low stock: " + ex.Message, "Warning");
                LowStockNumLblSA.Text = "Error";
            }
        }
        public object GetCategoryDataSource()
        {
            return CategoryNamecomboBoxIM.DataSource;
        }

        public object GetSupplierDataSource()
        {
            return SupplierNameComboBoxIM1.DataSource;
        }

        public void RefreshDashboard()
        {
            LoadDashboardStats();
        }

        private void LoadTransactionsIntoGrid()
        {
            try
            {
                TransactionController controller = new TransactionController();
                List<StockTransactionModel> transactions = controller.GetAllTransactions();

                // Change this name if your grid has a different name
                StockTransactionGridView.DataSource = transactions;

                // Hide unnecessary ID columns
                if (StockTransactionGridView.Columns["Transaction_ID"] != null)
                    StockTransactionGridView.Columns["Transaction_ID"].Visible = false;

                if (StockTransactionGridView.Columns["User_ID"] != null)
                    StockTransactionGridView.Columns["User_ID"].Visible = false;

                if (StockTransactionGridView.Columns["Item_ID"] != null)
                    StockTransactionGridView.Columns["Item_ID"].Visible = false;

                if (StockTransactionGridView.Columns["Zone_ID"] != null)
                    StockTransactionGridView.Columns["Zone_ID"].Visible = false;

                // Improve column headers
                if (StockTransactionGridView.Columns["Item_Name"] != null)
                    StockTransactionGridView.Columns["Item_Name"].HeaderText = "Item";

                if (StockTransactionGridView.Columns["Username"] != null)
                    StockTransactionGridView.Columns["Username"].HeaderText = "User";

                if (StockTransactionGridView.Columns["Transaction_Type"] != null)
                    StockTransactionGridView.Columns["Transaction_Type"].HeaderText = "Type";

                if (StockTransactionGridView.Columns["Quantity"] != null)
                    StockTransactionGridView.Columns["Quantity"].HeaderText = "Qty";

                if (StockTransactionGridView.Columns["Transaction_Date"] != null)
                    StockTransactionGridView.Columns["Transaction_Date"].HeaderText = "Date";

                if (StockTransactionGridView.Columns["Zone_Name"] != null)
                    StockTransactionGridView.Columns["Zone_Name"].HeaderText = "Zone";

                StockTransactionGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transactions: " + ex.Message, "Error");
            }
        }

        private void LoadTransactionComboBoxes()
        {
            try
            {
                ItemController itemController = new ItemController();
                LocationController locationController = new LocationController();

                var items = itemController.GetAllItems();
                var zones = locationController.GetAllZones();

                // For Inventory Manager Tab
                BindComboBox(ItemNameTranscomboBox, items, "Item_Name", "Item_ID");
                BindComboBox(ZoneNamecomboBoxIM, zones, "Zone_Name", "Zone_ID");
                BindTransactionType(TransactionTypecomboBox);

                // For Warehouse Staff Tab (if needed)
                BindComboBox(ItemNameTranscomboBox, items, "Item_Name", "Item_ID");
                BindComboBox(ZoneNamecomboBoxIM, zones, "Zone_Name", "Zone_ID");
                BindTransactionType(TransactionTypecomboBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transaction fields: " + ex.Message);
            }
        }

        private void BindComboBox(ComboBox combo, object dataSource, string displayMember, string valueMember)
        {
            combo.DataSource = dataSource;
            combo.DisplayMember = displayMember;
            combo.ValueMember = valueMember;
            if (combo.Items.Count > 0) combo.SelectedIndex = 0;
        }

        private void BindTransactionType(ComboBox combo)
        {
            combo.Items.Clear();
            combo.Items.Add("Purchase");
            combo.Items.Add("Sale");
            combo.Items.Add("Adjustment");
            combo.Items.Add("Transfer");
            combo.SelectedIndex = 0;
        }

        private void ReorderTransactionBtnIM_Click(object sender, EventArgs e)
        {
            RecordTransactionFromUI(ItemNameTranscomboBox, ZoneNamecomboBoxIM,
                           TransactionTypecomboBox, QuantityBoxIM);
        }
        private void RecordTransactionFromUI(ComboBox itemCombo, ComboBox zoneCombo,
                                     ComboBox typeCombo, TextBox qtyBox)
        {
            // Validation
            if (itemCombo.SelectedValue == null)
            {
                MessageBox.Show("Please select an Item.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (zoneCombo.SelectedValue == null)
            {
                MessageBox.Show("Please select a Zone.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (typeCombo.SelectedItem == null)
            {
                MessageBox.Show("Please select Transaction Type.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(qtyBox.Text.Trim(), out decimal quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity greater than 0.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtyBox.Focus();
                return;
            }

            // Create Transaction Object
            StockTransactionModel transaction = new StockTransactionModel
            {
                User_ID = UserSession.User_ID,                    // ← Corrected
                Item_ID = (Guid)itemCombo.SelectedValue,
                Zone_ID = (Guid)zoneCombo.SelectedValue,
                Transaction_Type = typeCombo.SelectedItem.ToString(),
                Quantity = quantity
            };

            // Record Transaction
            TransactionController controller = new TransactionController();
            bool success = controller.RecordTransaction(transaction);

            if (success)
            {
                MessageBox.Show("Transaction recorded successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                ClearTransactionFields(itemCombo, zoneCombo, typeCombo, qtyBox);
                LoadTransactionsIntoGrid();
                LoadItemsIntoGrid();           // Refresh stock levels
                RefreshDashboard();
            }
            else
            {
                MessageBox.Show("Failed to record transaction. Please try again.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ClearTransactionFields(ComboBox itemCombo, ComboBox zoneCombo,
                                    ComboBox typeCombo, TextBox qtyBox)
        {
            qtyBox.Clear();

            if (itemCombo.Items.Count > 0) itemCombo.SelectedIndex = 0;
            if (zoneCombo.Items.Count > 0) zoneCombo.SelectedIndex = 0;
            if (typeCombo.Items.Count > 0) typeCombo.SelectedIndex = 0;
        }


        private void ApplyRoleBasedTabs()
        {
            // Remove all tabs first
            tabControl1.TabPages.Clear();

            string role = UserSession.Role_Name.ToLower().Trim();

            if (role.Contains("admin"))
            {
                tabControl1.TabPages.Add(Dashbord_SystemAdmin);   // Use your actual TabPage names
                tabControl1.TabPages.Add(User_Management);
                tabControl1.TabPages.Add(Supplier_Management);
            }
            else if (role.Contains("inventory"))
            {
                tabControl1.TabPages.Add(Dashboard_InventoryManager);
                tabControl1.TabPages.Add(Inventory_InventoryManager);
                tabControl1.TabPages.Add(ItemEditor_InventroyManager);
                tabControl1.TabPages.Add(Transactions_InvetoryManager);
                tabControl1.TabPages.Add(Reposrts_InventoryManager);
            }
            else if (role.Contains("warehouse"))
            {
                tabControl1.TabPages.Add(Dashboard_WarehouseStaff);
                tabControl1.TabPages.Add(ItemEditor_InventroyManager);
                tabControl1.TabPages.Add(Inventory_InventoryManager);
                tabControl1.TabPages.Add(Transactions_InvetoryManager);
            }
            else
            {
                // Default
                tabControl1.TabPages.Add(Dashbord_SystemAdmin);
            }

            if (tabControl1.TabCount > 0)
                tabControl1.SelectedIndex = 0;
        }

        private void Logout()
        {
            // Clear Session
            UserSession.Clear();

            // Hide main interface
            Adim_Panel.Visible = false;
            tabControl1.Visible = false;

            // Show Login Panel
            LoginPanel.Visible = true;

            // Clear login fields
            UsernameBox.Clear();
            PasswordBox.Clear();
            ErrMsgLbl_Login.Text = "";

            // Optional: Reset tab control
            tabControl1.TabPages.Clear();
        }

        private void LogOutBtnWM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logout();
            }
        }

        private void LogOutBtnSA_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logout();
            }
        }

        private void LogOutBtnIM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Logout",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logout();
            }
        }

        // Load Categories into Filter ComboBox
        private void LoadCategoryFilter()
        {
            try
            {
                CategoryController controller = new CategoryController();
                List<ItemCategoryModel> categories = controller.GetAllCategories();

                // Create a new list with "All Categories" at the top
                var filterList = new List<ItemCategoryModel>
        {
            new ItemCategoryModel
            {
                Category_ID = Guid.Empty,
                Category_Name = "All Categories"
            }
        };

                filterList.AddRange(categories);

                // Bind the list
                CatagoryFilterComboBox.DataSource = filterList;
                CatagoryFilterComboBox.DisplayMember = "CategLoadItemsIntoGrid()ory_Name";
                CatagoryFilterComboBox.ValueMember = "Category_ID";

                CatagoryFilterComboBox.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading category filter: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filter Method
        private void SearchAndFilterItems()
        {
            try
            {
                ItemController controller = new ItemController();
                List<ItemModel> allItems = controller.GetAllItems();

                string searchText = SearchBoxIM.Text.Trim().ToLower();
                Guid? selectedCategoryId = null;

                if (CatagoryFilterComboBox.SelectedIndex > 0 &&
                    CatagoryFilterComboBox.SelectedValue != null)
                {
                    selectedCategoryId = (Guid)CatagoryFilterComboBox.SelectedValue;
                }

                var filteredItems = allItems.Where(item =>
                {
                    bool matchSearch = string.IsNullOrEmpty(searchText) ||
                                       item.Item_Name.ToLower().Contains(searchText) ||
                                       (item.Description != null && item.Description.ToLower().Contains(searchText));

                    bool matchCategory = selectedCategoryId == null ||
                                         item.Category_ID == selectedCategoryId.Value;

                    return matchSearch && matchCategory;
                }).ToList();

                // Force refresh the grid
                ItemTableGridViewIM.DataSource = null;
                ItemTableGridViewIM.DataSource = filteredItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error filtering: " + ex.Message);
            }
        }

        private void SearchBtnIM_Click(object sender, EventArgs e)
        {
            SearchAndFilterItems();
        }

        private void SearchBoxIM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchAndFilterItems();
            }
        }

        private void CatagoryFilterComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAndFilterItems();
        }
    }
 }

