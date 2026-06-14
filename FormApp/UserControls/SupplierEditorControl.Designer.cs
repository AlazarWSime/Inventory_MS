namespace FormApp.UserControls
{
    partial class SupplierEditorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ErrMsgLbl_SupplierEdit = new System.Windows.Forms.Label();
            this.SupDeleteBtn = new System.Windows.Forms.Button();
            this.SupCancelBtn = new System.Windows.Forms.Button();
            this.SupSaveBtn = new System.Windows.Forms.Button();
            this.SupAddressBox = new System.Windows.Forms.TextBox();
            this.AddressLbl = new System.Windows.Forms.Label();
            this.SupPhoneBox = new System.Windows.Forms.TextBox();
            this.ContactPhoneLbl = new System.Windows.Forms.Label();
            this.SupEmailBox = new System.Windows.Forms.TextBox();
            this.ContactEmailBox = new System.Windows.Forms.Label();
            this.SupNameBox = new System.Windows.Forms.TextBox();
            this.SupplierNameLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ErrMsgLbl_SupplierEdit
            // 
            this.ErrMsgLbl_SupplierEdit.AutoSize = true;
            this.ErrMsgLbl_SupplierEdit.Location = new System.Drawing.Point(118, 106);
            this.ErrMsgLbl_SupplierEdit.Name = "ErrMsgLbl_SupplierEdit";
            this.ErrMsgLbl_SupplierEdit.Size = new System.Drawing.Size(0, 16);
            this.ErrMsgLbl_SupplierEdit.TabIndex = 35;
            // 
            // SupDeleteBtn
            // 
            this.SupDeleteBtn.Location = new System.Drawing.Point(283, 354);
            this.SupDeleteBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupDeleteBtn.Name = "SupDeleteBtn";
            this.SupDeleteBtn.Size = new System.Drawing.Size(169, 48);
            this.SupDeleteBtn.TabIndex = 34;
            this.SupDeleteBtn.Text = "Delete Supplier";
            this.SupDeleteBtn.UseVisualStyleBackColor = true;
            this.SupDeleteBtn.Click += new System.EventHandler(this.SupDeleteBtn_Click);
            // 
            // SupCancelBtn
            // 
            this.SupCancelBtn.Location = new System.Drawing.Point(475, 354);
            this.SupCancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupCancelBtn.Name = "SupCancelBtn";
            this.SupCancelBtn.Size = new System.Drawing.Size(169, 48);
            this.SupCancelBtn.TabIndex = 33;
            this.SupCancelBtn.Text = "Cancel";
            this.SupCancelBtn.UseVisualStyleBackColor = true;
            this.SupCancelBtn.Click += new System.EventHandler(this.SupCancelBtn_Click);
            // 
            // SupSaveBtn
            // 
            this.SupSaveBtn.Location = new System.Drawing.Point(85, 354);
            this.SupSaveBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupSaveBtn.Name = "SupSaveBtn";
            this.SupSaveBtn.Size = new System.Drawing.Size(169, 48);
            this.SupSaveBtn.TabIndex = 32;
            this.SupSaveBtn.Text = "Save Supplier";
            this.SupSaveBtn.UseVisualStyleBackColor = true;
            this.SupSaveBtn.Click += new System.EventHandler(this.SupSaveBtn_Click);
            // 
            // SupAddressBox
            // 
            this.SupAddressBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.SupAddressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupAddressBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.SupAddressBox.Location = new System.Drawing.Point(381, 251);
            this.SupAddressBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupAddressBox.Multiline = true;
            this.SupAddressBox.Name = "SupAddressBox";
            this.SupAddressBox.Size = new System.Drawing.Size(279, 45);
            this.SupAddressBox.TabIndex = 31;
            this.SupAddressBox.Text = "Full address";
            // 
            // AddressLbl
            // 
            this.AddressLbl.AutoSize = true;
            this.AddressLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressLbl.Location = new System.Drawing.Point(379, 224);
            this.AddressLbl.Name = "AddressLbl";
            this.AddressLbl.Size = new System.Drawing.Size(60, 17);
            this.AddressLbl.TabIndex = 30;
            this.AddressLbl.Text = "Address";
            // 
            // SupPhoneBox
            // 
            this.SupPhoneBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.SupPhoneBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupPhoneBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.SupPhoneBox.Location = new System.Drawing.Point(53, 254);
            this.SupPhoneBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupPhoneBox.Multiline = true;
            this.SupPhoneBox.Name = "SupPhoneBox";
            this.SupPhoneBox.Size = new System.Drawing.Size(265, 45);
            this.SupPhoneBox.TabIndex = 29;
            this.SupPhoneBox.Text = "+2519....";
            // 
            // ContactPhoneLbl
            // 
            this.ContactPhoneLbl.AutoSize = true;
            this.ContactPhoneLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContactPhoneLbl.Location = new System.Drawing.Point(58, 229);
            this.ContactPhoneLbl.Name = "ContactPhoneLbl";
            this.ContactPhoneLbl.Size = new System.Drawing.Size(105, 17);
            this.ContactPhoneLbl.TabIndex = 28;
            this.ContactPhoneLbl.Text = "Contact_Phone";
            // 
            // SupEmailBox
            // 
            this.SupEmailBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.SupEmailBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupEmailBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.SupEmailBox.Location = new System.Drawing.Point(380, 172);
            this.SupEmailBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupEmailBox.Multiline = true;
            this.SupEmailBox.Name = "SupEmailBox";
            this.SupEmailBox.Size = new System.Drawing.Size(279, 45);
            this.SupEmailBox.TabIndex = 27;
            this.SupEmailBox.Text = "email@co.com";
            // 
            // ContactEmailBox
            // 
            this.ContactEmailBox.AutoSize = true;
            this.ContactEmailBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContactEmailBox.Location = new System.Drawing.Point(378, 146);
            this.ContactEmailBox.Name = "ContactEmailBox";
            this.ContactEmailBox.Size = new System.Drawing.Size(98, 17);
            this.ContactEmailBox.TabIndex = 26;
            this.ContactEmailBox.Text = "Contact_Email";
            // 
            // SupNameBox
            // 
            this.SupNameBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.SupNameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupNameBox.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.SupNameBox.Location = new System.Drawing.Point(52, 172);
            this.SupNameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupNameBox.Multiline = true;
            this.SupNameBox.Name = "SupNameBox";
            this.SupNameBox.Size = new System.Drawing.Size(265, 45);
            this.SupNameBox.TabIndex = 25;
            this.SupNameBox.Text = "Business name";
            // 
            // SupplierNameLbl
            // 
            this.SupplierNameLbl.AutoSize = true;
            this.SupplierNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SupplierNameLbl.Location = new System.Drawing.Point(57, 146);
            this.SupplierNameLbl.Name = "SupplierNameLbl";
            this.SupplierNameLbl.Size = new System.Drawing.Size(105, 17);
            this.SupplierNameLbl.TabIndex = 24;
            this.SupplierNameLbl.Text = "Supplier_Name";
            // 
            // SupplierEditorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ErrMsgLbl_SupplierEdit);
            this.Controls.Add(this.SupDeleteBtn);
            this.Controls.Add(this.SupCancelBtn);
            this.Controls.Add(this.SupSaveBtn);
            this.Controls.Add(this.SupAddressBox);
            this.Controls.Add(this.AddressLbl);
            this.Controls.Add(this.SupPhoneBox);
            this.Controls.Add(this.ContactPhoneLbl);
            this.Controls.Add(this.SupEmailBox);
            this.Controls.Add(this.ContactEmailBox);
            this.Controls.Add(this.SupNameBox);
            this.Controls.Add(this.SupplierNameLbl);
            this.Name = "SupplierEditorControl";
            this.Size = new System.Drawing.Size(705, 526);
            this.Load += new System.EventHandler(this.SupplierEditorControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ErrMsgLbl_SupplierEdit;
        private System.Windows.Forms.Button SupDeleteBtn;
        private System.Windows.Forms.Button SupCancelBtn;
        private System.Windows.Forms.Button SupSaveBtn;
        private System.Windows.Forms.TextBox SupAddressBox;
        private System.Windows.Forms.Label AddressLbl;
        private System.Windows.Forms.TextBox SupPhoneBox;
        private System.Windows.Forms.Label ContactPhoneLbl;
        private System.Windows.Forms.TextBox SupEmailBox;
        private System.Windows.Forms.Label ContactEmailBox;
        private System.Windows.Forms.TextBox SupNameBox;
        private System.Windows.Forms.Label SupplierNameLbl;
    }
}
