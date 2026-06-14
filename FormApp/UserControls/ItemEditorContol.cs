using System;
using System.Drawing;
using System.Windows.Forms;
using FormApp.Controllers;
using FormApp.Models;

namespace FormApp.UserControls
{
    public partial class ItemEditorContol : UserControl
    {
        private ItemModel _currentItem;
        public DialogResult Result { get; private set; } = DialogResult.Cancel;

        // Dragging variables
        private bool isDragging = false;
        private Point startingPoint = new Point(0, 0);

        public ItemEditorContol(ItemModel item = null)
        {
            InitializeComponent();
            _currentItem = item;
        }

        private void ItemEditorContol_Load(object sender, EventArgs e)
        {
            LoadCategoriesIntoComboBox();
            LoadSuppliersIntoComboBox();

            if (_currentItem != null)
            {
                LoadItemData();
            }
        }

        // ====================== DRAGGING ======================
        private void ItemEditorContol_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                startingPoint = e.Location;
            }
        }

        private void ItemEditorContol_MouseMove(object sender, MouseEventArgs e)
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

        private void ItemEditorContol_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        // ====================== LOAD CATEGORIES ======================
        private void LoadCategoriesIntoComboBox()
        {
            try
            {
                CategoryController controller = new CategoryController();
                var categories = controller.GetAllCategories();

                if (categories == null || categories.Count == 0)
                {
                    MessageBox.Show("No categories found. Please add some categories first.", "Info");
                    return;
                }

                CnCategoryNamecomboBoxIM.DataSource = categories;
                CnCategoryNamecomboBoxIM.DisplayMember = "Category_Name";
                CnCategoryNamecomboBoxIM.ValueMember = "Category_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + ex.Message, "Error");
            }
        }

        // ====================== LOAD SUPPLIERS ======================
        private void LoadSuppliersIntoComboBox()
        {
            try
            {
                SupplierController controller = new SupplierController();
                var suppliers = controller.GetAllSuppliers();

                if (suppliers == null || suppliers.Count == 0)
                {
                    MessageBox.Show("No suppliers found. Please add some suppliers first.", "Info");
                    return;
                }

                CnSupplierNameComboBoxIM1.DataSource = suppliers;
                CnSupplierNameComboBoxIM1.DisplayMember = "Supplier_Name";
                CnSupplierNameComboBoxIM1.ValueMember = "Supplier_ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading suppliers: " + ex.Message, "Error");
            }
        }

        private void LoadItemData()
        {
            CnItemNameBoxIM.Text = _currentItem.Item_Name;
            CnDiscriptionBoxIM.Text = _currentItem.Description ?? "";
            CnPriceBoxIM.Text = _currentItem.Price.ToString("N2");
            CnReorderLevelBoxIM.Text = _currentItem.Reorder_Level.ToString();

            if (_currentItem.Category_ID != Guid.Empty)
                CnCategoryNamecomboBoxIM.SelectedValue = _currentItem.Category_ID;

            if (_currentItem.Supplier_ID != Guid.Empty)
                CnSupplierNameComboBoxIM1.SelectedValue = _currentItem.Supplier_ID;
        }

        // Rest of the code remains same (CnSaveBtnIM_Click, etc.)
        private void CnSaveBtnIM_Click(object sender, EventArgs e)
        {
            // ... (your existing save code - no change needed)
            ErrMsgLbl_Item.Text = "";
            ErrMsgLbl_Item.ForeColor = Color.Firebrick;

            string itemName = CnItemNameBoxIM.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemName))
            {
                ErrMsgLbl_Item.Text = "Item Name is required.";
                CnItemNameBoxIM.Focus();
                return;
            }

            if (CnCategoryNamecomboBoxIM.SelectedValue == null)
            {
                ErrMsgLbl_Item.Text = "Please select a Category.";
                CnCategoryNamecomboBoxIM.Focus();
                return;
            }

            if (CnSupplierNameComboBoxIM1.SelectedValue == null)
            {
                ErrMsgLbl_Item.Text = "Please select a Supplier.";
                CnSupplierNameComboBoxIM1.Focus();
                return;
            }

            if (!decimal.TryParse(CnPriceBoxIM.Text, out decimal price) || price <= 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Price.";
                CnPriceBoxIM.Focus();
                return;
            }

            if (!int.TryParse(CnReorderLevelBoxIM.Text, out int reorder) || reorder < 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Reorder Level.";
                CnReorderLevelBoxIM.Focus();
                return;
            }

            if (!int.TryParse(CnQuantityBoxIM.Text, out int quantity) || quantity < 0)
            {
                ErrMsgLbl_Item.Text = "Please enter a valid Quantity (0 or greater).";
                CnQuantityBoxIM.Focus();
                return;
            }

            ItemModel item = _currentItem ?? new ItemModel();

            item.Item_Name = itemName;
            item.Description = CnDiscriptionBoxIM.Text.Trim();
            item.Price = price;
            item.Reorder_Level = reorder;
            item.Total_Quantity = quantity;
            item.Category_ID = (Guid)CnCategoryNamecomboBoxIM.SelectedValue;
            item.Supplier_ID = (Guid)CnSupplierNameComboBoxIM1.SelectedValue;

            ItemController controller = new ItemController();
            bool success = (_currentItem == null)
                ? controller.CreateItem(item)
                : controller.UpdateItem(item);

            if (success)
            {
                ErrMsgLbl_Item.Text = _currentItem == null ? "Item created successfully!" : "Item updated successfully!";
                ErrMsgLbl_Item.ForeColor = Color.LimeGreen;

                Result = DialogResult.OK;
                System.Threading.Tasks.Task.Delay(1000)
                    .ContinueWith(t => this.ParentForm?.Close(),
                                  System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                ErrMsgLbl_Item.Text = "Failed to save item.";
            }
        }

        private void CnDeleteItemBtnIM_Click(object sender, EventArgs e)
        {
            if (_currentItem == null) return;

            if (MessageBox.Show($"Are you sure you want to delete '{_currentItem.Item_Name}'?",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool success = new ItemController().DeleteItem(_currentItem.Item_ID);

                if (success)
                {
                    MessageBox.Show("Item deleted successfully!", "Success");
                    Result = DialogResult.OK;
                    this.ParentForm?.Close();

                    if (this.ParentForm is Inventory_MS mainForm)
                    {
                        mainForm.RefreshDashboard();
                        mainForm.LoadItemsIntoGrid();   // Also refresh grid
                    }

                    this.ParentForm?.Close();
                }

            }
        }

        private void CnDoneBtnIM_Click(object sender, EventArgs e)
        {
            this.ParentForm?.Close();
        }

        private void ClearFields()
        {
            CnItemNameBoxIM.Clear();
            CnDiscriptionBoxIM.Clear();
            CnPriceBoxIM.Clear();
            CnReorderLevelBoxIM.Clear();
            CnQuantityBoxIM.Clear();
        }
    }
}