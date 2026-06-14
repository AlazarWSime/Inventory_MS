namespace FormApp.UserControls
{
    partial class EditUserControl
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
            this.ConErrMsgLbl_EditUser = new System.Windows.Forms.Label();
            this.ConClearBtnSA = new System.Windows.Forms.Button();
            this.ConDeleteBtnSA = new System.Windows.Forms.Button();
            this.ConSaveUserBtn = new System.Windows.Forms.Button();
            this.ConRoleNameComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ConPasswordBox = new System.Windows.Forms.TextBox();
            this.PasswordLblUM = new System.Windows.Forms.Label();
            this.ConUsernameBoxUM = new System.Windows.Forms.TextBox();
            this.UsernameLbl = new System.Windows.Forms.Label();
            this.ConFull_NameBox = new System.Windows.Forms.TextBox();
            this.Full_NameLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ConErrMsgLbl_EditUser
            // 
            this.ConErrMsgLbl_EditUser.AutoSize = true;
            this.ConErrMsgLbl_EditUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConErrMsgLbl_EditUser.Location = new System.Drawing.Point(118, 91);
            this.ConErrMsgLbl_EditUser.Name = "ConErrMsgLbl_EditUser";
            this.ConErrMsgLbl_EditUser.Size = new System.Drawing.Size(0, 16);
            this.ConErrMsgLbl_EditUser.TabIndex = 28;
            // 
            // ConClearBtnSA
            // 
            this.ConClearBtnSA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConClearBtnSA.Location = new System.Drawing.Point(508, 506);
            this.ConClearBtnSA.Name = "ConClearBtnSA";
            this.ConClearBtnSA.Size = new System.Drawing.Size(121, 47);
            this.ConClearBtnSA.TabIndex = 27;
            this.ConClearBtnSA.Text = "Clear";
            this.ConClearBtnSA.UseVisualStyleBackColor = true;
            // 
            // ConDeleteBtnSA
            // 
            this.ConDeleteBtnSA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConDeleteBtnSA.Location = new System.Drawing.Point(304, 506);
            this.ConDeleteBtnSA.Name = "ConDeleteBtnSA";
            this.ConDeleteBtnSA.Size = new System.Drawing.Size(121, 47);
            this.ConDeleteBtnSA.TabIndex = 26;
            this.ConDeleteBtnSA.Text = "Delete";
            this.ConDeleteBtnSA.UseVisualStyleBackColor = true;
            this.ConDeleteBtnSA.Click += new System.EventHandler(this.ConDeleteBtnSA_Click);
            // 
            // ConSaveUserBtn
            // 
            this.ConSaveUserBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ConSaveUserBtn.Location = new System.Drawing.Point(74, 506);
            this.ConSaveUserBtn.Name = "ConSaveUserBtn";
            this.ConSaveUserBtn.Size = new System.Drawing.Size(162, 47);
            this.ConSaveUserBtn.TabIndex = 25;
            this.ConSaveUserBtn.Text = "Save User";
            this.ConSaveUserBtn.UseVisualStyleBackColor = true;
            this.ConSaveUserBtn.Click += new System.EventHandler(this.ConSaveUserBtn_Click);
            // 
            // ConRoleNameComboBox
            // 
            this.ConRoleNameComboBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ConRoleNameComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConRoleNameComboBox.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.ConRoleNameComboBox.FormattingEnabled = true;
            this.ConRoleNameComboBox.Items.AddRange(new object[] {
            "System Administration",
            "Inventory manager",
            "Warehouse staff",
            "Salace manager"});
            this.ConRoleNameComboBox.Location = new System.Drawing.Point(95, 323);
            this.ConRoleNameComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConRoleNameComboBox.Name = "ConRoleNameComboBox";
            this.ConRoleNameComboBox.Size = new System.Drawing.Size(481, 33);
            this.ConRoleNameComboBox.TabIndex = 24;
            this.ConRoleNameComboBox.SelectedIndexChanged += new System.EventHandler(this.RoleNameComboBoxSA_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(92, 304);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(165, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "Role-AssignRole(RoleID)";
            // 
            // ConPasswordBox
            // 
            this.ConPasswordBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.ConPasswordBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConPasswordBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ConPasswordBox.Location = new System.Drawing.Point(96, 259);
            this.ConPasswordBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConPasswordBox.Name = "ConPasswordBox";
            this.ConPasswordBox.Size = new System.Drawing.Size(481, 34);
            this.ConPasswordBox.TabIndex = 22;
            this.ConPasswordBox.Text = "Password";
            // 
            // PasswordLblUM
            // 
            this.PasswordLblUM.AutoSize = true;
            this.PasswordLblUM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordLblUM.Location = new System.Drawing.Point(93, 239);
            this.PasswordLblUM.Name = "PasswordLblUM";
            this.PasswordLblUM.Size = new System.Drawing.Size(69, 17);
            this.PasswordLblUM.TabIndex = 21;
            this.PasswordLblUM.Text = "Password";
            // 
            // ConUsernameBoxUM
            // 
            this.ConUsernameBoxUM.BackColor = System.Drawing.SystemColors.InfoText;
            this.ConUsernameBoxUM.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConUsernameBoxUM.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ConUsernameBoxUM.Location = new System.Drawing.Point(96, 202);
            this.ConUsernameBoxUM.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConUsernameBoxUM.Name = "ConUsernameBoxUM";
            this.ConUsernameBoxUM.Size = new System.Drawing.Size(481, 34);
            this.ConUsernameBoxUM.TabIndex = 20;
            this.ConUsernameBoxUM.Text = "Unique username";
            // 
            // UsernameLbl
            // 
            this.UsernameLbl.AutoSize = true;
            this.UsernameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UsernameLbl.Location = new System.Drawing.Point(93, 181);
            this.UsernameLbl.Name = "UsernameLbl";
            this.UsernameLbl.Size = new System.Drawing.Size(73, 17);
            this.UsernameLbl.TabIndex = 19;
            this.UsernameLbl.Text = "Username";
            // 
            // ConFull_NameBox
            // 
            this.ConFull_NameBox.BackColor = System.Drawing.SystemColors.InfoText;
            this.ConFull_NameBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConFull_NameBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.ConFull_NameBox.Location = new System.Drawing.Point(94, 145);
            this.ConFull_NameBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ConFull_NameBox.Name = "ConFull_NameBox";
            this.ConFull_NameBox.Size = new System.Drawing.Size(481, 34);
            this.ConFull_NameBox.TabIndex = 18;
            this.ConFull_NameBox.Text = "Legal full name";
            // 
            // Full_NameLbl
            // 
            this.Full_NameLbl.AutoSize = true;
            this.Full_NameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Full_NameLbl.Location = new System.Drawing.Point(93, 127);
            this.Full_NameLbl.Name = "Full_NameLbl";
            this.Full_NameLbl.Size = new System.Drawing.Size(75, 17);
            this.Full_NameLbl.TabIndex = 17;
            this.Full_NameLbl.Text = "Full_Name";
            // 
            // EditUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.ConClearBtnSA);
            this.Controls.Add(this.ConDeleteBtnSA);
            this.Controls.Add(this.ConSaveUserBtn);
            this.Controls.Add(this.ConRoleNameComboBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ConPasswordBox);
            this.Controls.Add(this.PasswordLblUM);
            this.Controls.Add(this.ConUsernameBoxUM);
            this.Controls.Add(this.UsernameLbl);
            this.Controls.Add(this.ConFull_NameBox);
            this.Controls.Add(this.Full_NameLbl);
            this.Controls.Add(this.ConErrMsgLbl_EditUser);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "EditUserControl";
            this.Size = new System.Drawing.Size(734, 659);
            this.Load += new System.EventHandler(this.EditUserControl_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EditUserControl_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EditUserControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EditUserControl_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConErrMsgLbl_EditUser;
        private System.Windows.Forms.Button ConClearBtnSA;
        private System.Windows.Forms.Button ConDeleteBtnSA;
        private System.Windows.Forms.Button ConSaveUserBtn;
        private System.Windows.Forms.ComboBox ConRoleNameComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ConPasswordBox;
        private System.Windows.Forms.Label PasswordLblUM;
        private System.Windows.Forms.TextBox ConUsernameBoxUM;
        private System.Windows.Forms.Label UsernameLbl;
        private System.Windows.Forms.TextBox ConFull_NameBox;
        private System.Windows.Forms.Label Full_NameLbl;

    }
}
