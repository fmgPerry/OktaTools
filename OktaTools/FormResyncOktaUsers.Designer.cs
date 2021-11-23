namespace OktaTools
{
    partial class FormResyncOktaUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormResyncOktaUsers));
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxListType = new System.Windows.Forms.ComboBox();
            this.buttonGetContactIDs = new System.Windows.Forms.Button();
            this.checkBoxIsBulk = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonResyncOktaUsers = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxContactIDs = new System.Windows.Forms.TextBox();
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.openFileDialogGetContactIDs = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 13);
            this.label3.TabIndex = 42;
            this.label3.Text = "Choose List Type";
            // 
            // comboBoxListType
            // 
            this.comboBoxListType.FormattingEnabled = true;
            this.comboBoxListType.Location = new System.Drawing.Point(120, 71);
            this.comboBoxListType.Name = "comboBoxListType";
            this.comboBoxListType.Size = new System.Drawing.Size(150, 21);
            this.comboBoxListType.TabIndex = 41;
            this.comboBoxListType.SelectedIndexChanged += new System.EventHandler(this.comboBoxListType_SelectedIndexChanged);
            // 
            // buttonGetContactIDs
            // 
            this.buttonGetContactIDs.Location = new System.Drawing.Point(276, 69);
            this.buttonGetContactIDs.Name = "buttonGetContactIDs";
            this.buttonGetContactIDs.Size = new System.Drawing.Size(150, 23);
            this.buttonGetContactIDs.TabIndex = 40;
            this.buttonGetContactIDs.Text = "Get list from file";
            this.buttonGetContactIDs.UseVisualStyleBackColor = true;
            this.buttonGetContactIDs.Click += new System.EventHandler(this.buttonGetContactIDs_Click);
            // 
            // checkBoxIsBulk
            // 
            this.checkBoxIsBulk.AutoSize = true;
            this.checkBoxIsBulk.Location = new System.Drawing.Point(328, 44);
            this.checkBoxIsBulk.Name = "checkBoxIsBulk";
            this.checkBoxIsBulk.Size = new System.Drawing.Size(58, 17);
            this.checkBoxIsBulk.TabIndex = 39;
            this.checkBoxIsBulk.Text = "Is Bulk";
            this.checkBoxIsBulk.UseVisualStyleBackColor = true;
            this.checkBoxIsBulk.CheckedChanged += new System.EventHandler(this.checkBoxIsBulk_CheckedChanged);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(50, 13);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 38;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(120, 240);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 37;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonResyncOktaUsers
            // 
            this.buttonResyncOktaUsers.Location = new System.Drawing.Point(120, 211);
            this.buttonResyncOktaUsers.Name = "buttonResyncOktaUsers";
            this.buttonResyncOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonResyncOktaUsers.TabIndex = 36;
            this.buttonResyncOktaUsers.Text = "Resync Okta Users";
            this.buttonResyncOktaUsers.UseVisualStyleBackColor = true;
            this.buttonResyncOktaUsers.Click += new System.EventHandler(this.buttonResyncOktaUsers_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Choose Environment";
            // 
            // textBoxContactIDs
            // 
            this.textBoxContactIDs.Location = new System.Drawing.Point(276, 109);
            this.textBoxContactIDs.Multiline = true;
            this.textBoxContactIDs.Name = "textBoxContactIDs";
            this.textBoxContactIDs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContactIDs.Size = new System.Drawing.Size(150, 235);
            this.textBoxContactIDs.TabIndex = 34;
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(120, 42);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 33;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // openFileDialogGetContactIDs
            // 
            this.openFileDialogGetContactIDs.FileName = "openFileDialog1";
            this.openFileDialogGetContactIDs.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogGetContactIDs_FileOk);
            // 
            // FormResyncOktaUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 362);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxListType);
            this.Controls.Add(this.buttonGetContactIDs);
            this.Controls.Add(this.checkBoxIsBulk);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonResyncOktaUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxContactIDs);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormResyncOktaUsers";
            this.Text = "FormResyncOktaUsers";
            this.Load += new System.EventHandler(this.FormResyncOktaUsers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxListType;
        private System.Windows.Forms.Button buttonGetContactIDs;
        private System.Windows.Forms.CheckBox checkBoxIsBulk;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonResyncOktaUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxContactIDs;
        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.OpenFileDialog openFileDialogGetContactIDs;
    }
}