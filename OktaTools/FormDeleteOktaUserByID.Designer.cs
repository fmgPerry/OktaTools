namespace OktaTools
{
    partial class FormDeleteOktaUserByID
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeleteOktaUserByID));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonDeleteOktaUsers = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxContactIDs = new System.Windows.Forms.TextBox();
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.checkBoxIsBulk = new System.Windows.Forms.CheckBox();
            this.buttonGetContactIDs = new System.Windows.Forms.Button();
            this.openFileDialogGetContactIDs = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxListType = new System.Windows.Forms.ComboBox();
            this.checkBoxUpdateGW = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(53, 14);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 26;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(123, 241);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 25;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonDeleteOktaUsers
            // 
            this.buttonDeleteOktaUsers.Location = new System.Drawing.Point(123, 212);
            this.buttonDeleteOktaUsers.Name = "buttonDeleteOktaUsers";
            this.buttonDeleteOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonDeleteOktaUsers.TabIndex = 22;
            this.buttonDeleteOktaUsers.Text = "Delete Okta Users";
            this.buttonDeleteOktaUsers.UseVisualStyleBackColor = true;
            this.buttonDeleteOktaUsers.Click += new System.EventHandler(this.buttonDeleteOktaUsers_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Choose Environment";
            // 
            // textBoxContactIDs
            // 
            this.textBoxContactIDs.Location = new System.Drawing.Point(279, 110);
            this.textBoxContactIDs.Multiline = true;
            this.textBoxContactIDs.Name = "textBoxContactIDs";
            this.textBoxContactIDs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContactIDs.Size = new System.Drawing.Size(150, 235);
            this.textBoxContactIDs.TabIndex = 19;
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(123, 43);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 18;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // checkBoxIsBulk
            // 
            this.checkBoxIsBulk.AutoSize = true;
            this.checkBoxIsBulk.Location = new System.Drawing.Point(368, 45);
            this.checkBoxIsBulk.Name = "checkBoxIsBulk";
            this.checkBoxIsBulk.Size = new System.Drawing.Size(58, 17);
            this.checkBoxIsBulk.TabIndex = 28;
            this.checkBoxIsBulk.Text = "Is Bulk";
            this.checkBoxIsBulk.UseVisualStyleBackColor = true;
            this.checkBoxIsBulk.CheckedChanged += new System.EventHandler(this.checkBoxIsBulk_CheckedChanged);
            // 
            // buttonGetContactIDs
            // 
            this.buttonGetContactIDs.Location = new System.Drawing.Point(279, 70);
            this.buttonGetContactIDs.Name = "buttonGetContactIDs";
            this.buttonGetContactIDs.Size = new System.Drawing.Size(150, 23);
            this.buttonGetContactIDs.TabIndex = 29;
            this.buttonGetContactIDs.Text = "Get list from file";
            this.buttonGetContactIDs.UseVisualStyleBackColor = true;
            this.buttonGetContactIDs.Click += new System.EventHandler(this.buttonGetContactIDs_Click);
            // 
            // openFileDialogGetContactIDs
            // 
            this.openFileDialogGetContactIDs.FileName = "openFileDialog1";
            this.openFileDialogGetContactIDs.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogGetContactIDs_FileOk);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 31;
            this.label3.Text = "Choose List Type";
            // 
            // comboBoxListType
            // 
            this.comboBoxListType.FormattingEnabled = true;
            this.comboBoxListType.Location = new System.Drawing.Point(123, 72);
            this.comboBoxListType.Name = "comboBoxListType";
            this.comboBoxListType.Size = new System.Drawing.Size(150, 21);
            this.comboBoxListType.TabIndex = 30;
            this.comboBoxListType.SelectedIndexChanged += new System.EventHandler(this.comboBoxListType_SelectedIndexChanged);
            // 
            // checkBoxUpdateGW
            // 
            this.checkBoxUpdateGW.AutoSize = true;
            this.checkBoxUpdateGW.Location = new System.Drawing.Point(279, 45);
            this.checkBoxUpdateGW.Name = "checkBoxUpdateGW";
            this.checkBoxUpdateGW.Size = new System.Drawing.Size(83, 17);
            this.checkBoxUpdateGW.TabIndex = 32;
            this.checkBoxUpdateGW.Text = "Update GW";
            this.checkBoxUpdateGW.UseVisualStyleBackColor = true;
            this.checkBoxUpdateGW.CheckedChanged += new System.EventHandler(this.checkBoxUpdateGW_CheckedChanged);
            // 
            // FormDeleteOktaUserByID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 362);
            this.Controls.Add(this.checkBoxUpdateGW);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxListType);
            this.Controls.Add(this.buttonGetContactIDs);
            this.Controls.Add(this.checkBoxIsBulk);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonDeleteOktaUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxContactIDs);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeleteOktaUserByID";
            this.Text = "FormDeleteOktaUserByID";
            this.Load += new System.EventHandler(this.FormDeleteOktaUserByID_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonDeleteOktaUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxContactIDs;
        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.CheckBox checkBoxIsBulk;
        private System.Windows.Forms.Button buttonGetContactIDs;
        private System.Windows.Forms.OpenFileDialog openFileDialogGetContactIDs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxListType;
        private System.Windows.Forms.CheckBox checkBoxUpdateGW;
    }
}