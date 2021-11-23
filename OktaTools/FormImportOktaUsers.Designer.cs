namespace OktaTools
{
    partial class FormImportOktaUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImportOktaUsers));
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.textBoxContactIDs = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonImportOktaUsers = new System.Windows.Forms.Button();
            this.textBoxSecondaryEmail = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBoxImportBy = new System.Windows.Forms.GroupBox();
            this.radioButtonUsingCode = new System.Windows.Forms.RadioButton();
            this.radioButtonUsingBoomi = new System.Windows.Forms.RadioButton();
            this.textBoxNotImportedContactIDs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonGetContactIDs = new System.Windows.Forms.Button();
            this.openFileDialogGetContactIDs = new System.Windows.Forms.OpenFileDialog();
            this.checkBoxDeleteFirst = new System.Windows.Forms.CheckBox();
            this.groupBoxImportBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(123, 45);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 0;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // textBoxContactIDs
            // 
            this.textBoxContactIDs.Location = new System.Drawing.Point(279, 73);
            this.textBoxContactIDs.Multiline = true;
            this.textBoxContactIDs.Name = "textBoxContactIDs";
            this.textBoxContactIDs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContactIDs.Size = new System.Drawing.Size(150, 296);
            this.textBoxContactIDs.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Contact IDs";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Choose Environment";
            // 
            // buttonImportOktaUsers
            // 
            this.buttonImportOktaUsers.Location = new System.Drawing.Point(123, 188);
            this.buttonImportOktaUsers.Name = "buttonImportOktaUsers";
            this.buttonImportOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonImportOktaUsers.TabIndex = 4;
            this.buttonImportOktaUsers.Text = "Import Users into Okta";
            this.buttonImportOktaUsers.UseVisualStyleBackColor = true;
            this.buttonImportOktaUsers.Click += new System.EventHandler(this.buttonImportOktaUsers_Click);
            // 
            // textBoxSecondaryEmail
            // 
            this.textBoxSecondaryEmail.Location = new System.Drawing.Point(123, 72);
            this.textBoxSecondaryEmail.Name = "textBoxSecondaryEmail";
            this.textBoxSecondaryEmail.Size = new System.Drawing.Size(150, 20);
            this.textBoxSecondaryEmail.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Secondary Email";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(123, 217);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 7;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(53, 16);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 8;
            this.labelStatus.Text = "Status: ";
            // 
            // groupBoxImportBy
            // 
            this.groupBoxImportBy.Controls.Add(this.radioButtonUsingCode);
            this.groupBoxImportBy.Controls.Add(this.radioButtonUsingBoomi);
            this.groupBoxImportBy.Location = new System.Drawing.Point(12, 98);
            this.groupBoxImportBy.Name = "groupBoxImportBy";
            this.groupBoxImportBy.Size = new System.Drawing.Size(261, 75);
            this.groupBoxImportBy.TabIndex = 9;
            this.groupBoxImportBy.TabStop = false;
            this.groupBoxImportBy.Text = "Import into Okta";
            // 
            // radioButtonUsingCode
            // 
            this.radioButtonUsingCode.AutoSize = true;
            this.radioButtonUsingCode.Location = new System.Drawing.Point(111, 42);
            this.radioButtonUsingCode.Name = "radioButtonUsingCode";
            this.radioButtonUsingCode.Size = new System.Drawing.Size(67, 17);
            this.radioButtonUsingCode.TabIndex = 1;
            this.radioButtonUsingCode.TabStop = true;
            this.radioButtonUsingCode.Text = "using C#";
            this.radioButtonUsingCode.UseVisualStyleBackColor = true;
            // 
            // radioButtonUsingBoomi
            // 
            this.radioButtonUsingBoomi.AutoSize = true;
            this.radioButtonUsingBoomi.Location = new System.Drawing.Point(111, 19);
            this.radioButtonUsingBoomi.Name = "radioButtonUsingBoomi";
            this.radioButtonUsingBoomi.Size = new System.Drawing.Size(82, 17);
            this.radioButtonUsingBoomi.TabIndex = 0;
            this.radioButtonUsingBoomi.TabStop = true;
            this.radioButtonUsingBoomi.Text = "using Boomi";
            this.radioButtonUsingBoomi.UseVisualStyleBackColor = true;
            this.radioButtonUsingBoomi.CheckedChanged += new System.EventHandler(this.radioButtonUsingBoomi_CheckedChanged);
            // 
            // textBoxNotImportedContactIDs
            // 
            this.textBoxNotImportedContactIDs.Location = new System.Drawing.Point(123, 264);
            this.textBoxNotImportedContactIDs.MaxLength = 100000;
            this.textBoxNotImportedContactIDs.Multiline = true;
            this.textBoxNotImportedContactIDs.Name = "textBoxNotImportedContactIDs";
            this.textBoxNotImportedContactIDs.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxNotImportedContactIDs.Size = new System.Drawing.Size(150, 105);
            this.textBoxNotImportedContactIDs.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 248);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Not Imported Contact IDs";
            // 
            // buttonGetContactIDs
            // 
            this.buttonGetContactIDs.Location = new System.Drawing.Point(279, 43);
            this.buttonGetContactIDs.Name = "buttonGetContactIDs";
            this.buttonGetContactIDs.Size = new System.Drawing.Size(150, 23);
            this.buttonGetContactIDs.TabIndex = 12;
            this.buttonGetContactIDs.Text = "Get ContactIDs from file";
            this.buttonGetContactIDs.UseVisualStyleBackColor = true;
            this.buttonGetContactIDs.Click += new System.EventHandler(this.buttonGetContactIDs_Click);
            // 
            // openFileDialogGetContactIDs
            // 
            this.openFileDialogGetContactIDs.FileName = "openFileDialog1";
            this.openFileDialogGetContactIDs.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialogGetContactIDs_FileOk);
            // 
            // checkBoxDeleteFirst
            // 
            this.checkBoxDeleteFirst.AutoSize = true;
            this.checkBoxDeleteFirst.Location = new System.Drawing.Point(34, 192);
            this.checkBoxDeleteFirst.Name = "checkBoxDeleteFirst";
            this.checkBoxDeleteFirst.Size = new System.Drawing.Size(79, 17);
            this.checkBoxDeleteFirst.TabIndex = 13;
            this.checkBoxDeleteFirst.Text = "Delete First";
            this.checkBoxDeleteFirst.UseVisualStyleBackColor = true;
            this.checkBoxDeleteFirst.CheckedChanged += new System.EventHandler(this.checkBoxDeleteFirst_CheckedChanged);
            // 
            // FormImportOktaUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 388);
            this.Controls.Add(this.checkBoxDeleteFirst);
            this.Controls.Add(this.buttonGetContactIDs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNotImportedContactIDs);
            this.Controls.Add(this.groupBoxImportBy);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxSecondaryEmail);
            this.Controls.Add(this.buttonImportOktaUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxContactIDs);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormImportOktaUsers";
            this.Text = "FormImportOktaUsers";
            this.Load += new System.EventHandler(this.FormImportOktaUsers_Load);
            this.groupBoxImportBy.ResumeLayout(false);
            this.groupBoxImportBy.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.TextBox textBoxContactIDs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonImportOktaUsers;
        private System.Windows.Forms.TextBox textBoxSecondaryEmail;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.GroupBox groupBoxImportBy;
        private System.Windows.Forms.RadioButton radioButtonUsingCode;
        private System.Windows.Forms.RadioButton radioButtonUsingBoomi;
        private System.Windows.Forms.TextBox textBoxNotImportedContactIDs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonGetContactIDs;
        private System.Windows.Forms.OpenFileDialog openFileDialogGetContactIDs;
        private System.Windows.Forms.CheckBox checkBoxDeleteFirst;
    }
}