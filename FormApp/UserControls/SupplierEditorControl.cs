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

namespace FormApp.UserControls
{
    public partial class SupplierEditorControl : UserControl
    {
        private SupplierModel _currentSupplier;
        public DialogResult Result { get; private set; } = DialogResult.Cancel;

        public SupplierEditorControl(SupplierModel supplier = null)
        {
            InitializeComponent();
            _currentSupplier = supplier;
        }

        private void SupplierEditorControl_Load(object sender, EventArgs e)
        {
            if (_currentSupplier != null)
            {
                LoadSupplierData();
            }
            else
            {
                ClearFields();
            }
        }

        private void LoadSupplierData()
        {
            if (_currentSupplier == null) return;

            SupNameBox.Text = _currentSupplier.Supplier_Name ?? "";
            SupEmailBox.Text = _currentSupplier.Contact_Email ?? "";
            SupPhoneBox.Text = _currentSupplier.Contact_Phone ?? "";
            SupAddressBox.Text = _currentSupplier.Address ?? "";
        }

        private void ClearFields()
        {
            SupNameBox.Clear();
            SupEmailBox.Clear();
            SupPhoneBox.Clear();
            SupAddressBox.Clear();
        }

        private void SupSaveBtn_Click(object sender, EventArgs e)
        {
            ErrMsgLbl_SupplierEdit.Text = "";

            string supplierName = SupNameBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(supplierName))
            {
                ErrMsgLbl_SupplierEdit.Text = "Supplier Name is required.";
                SupNameBox.Focus();
                return;
            }

            // === IMPORTANT FIX FOR EDITING ===
            SupplierModel supplier = _currentSupplier != null
                ? _currentSupplier
                : new SupplierModel();

            supplier.Supplier_Name = supplierName;
            supplier.Contact_Email = SupEmailBox.Text.Trim();
            supplier.Contact_Phone = SupPhoneBox.Text.Trim();
            supplier.Address = SupAddressBox.Text.Trim();

            // Make sure Supplier_ID is preserved when editing
            if (_currentSupplier != null)
            {
                supplier.Supplier_ID = _currentSupplier.Supplier_ID;
            }

            SupplierController controller = new SupplierController();
            bool success = (_currentSupplier == null)
                ? controller.CreateSupplier(supplier)
                : controller.UpdateSupplier(supplier);

            if (success)
            {
                ErrMsgLbl_SupplierEdit.Text = _currentSupplier == null ?
                    "Supplier created successfully!" : "Supplier updated successfully!";
                ErrMsgLbl_SupplierEdit.ForeColor = Color.LimeGreen;

                RefreshMainForm();

                Result = DialogResult.OK;
                this.ParentForm?.Close();
            }
            else
            {
                ErrMsgLbl_SupplierEdit.Text = "Failed to save supplier.";
                ErrMsgLbl_SupplierEdit.ForeColor = Color.Firebrick;
            }
        }

        private void SupDeleteBtn_Click(object sender, EventArgs e)
        {
            if (_currentSupplier == null) return;

            if (MessageBox.Show($"Are you sure you want to delete supplier '{_currentSupplier.Supplier_Name}'?\n\nThis action cannot be undone.",
                "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool success = new SupplierController().DeleteSupplier(_currentSupplier.Supplier_ID);

                if (success)
                {
                    MessageBox.Show("Supplier deleted successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RefreshMainForm();

                    Result = DialogResult.OK;
                    this.ParentForm?.Close();
                }
                else
                {
                    MessageBox.Show("Failed to delete supplier.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RefreshMainForm()
        {
            if (this.ParentForm is Inventory_MS mainForm)
            {
                mainForm.LoadSuppliersIntoGrid();
                mainForm.LoadSuppliersIntoComboBox();
            }
        }

        private void SupCancelBtn_Click(object sender, EventArgs e)
        {
            this.ParentForm?.Close();
        }
    }
}