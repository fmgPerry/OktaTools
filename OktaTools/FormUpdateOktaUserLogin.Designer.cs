namespace OktaTools
{
    partial class FormUpdateOktaUserLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormUpdateOktaUserLogin));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonUpdateOktaUsersLogin = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContactIDsLogins = new System.Windows.Forms.TextBox();
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxResults = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(16, 19);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 17;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(317, 76);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 16;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonUpdateOktaUsersLogin
            // 
            this.buttonUpdateOktaUsersLogin.Location = new System.Drawing.Point(317, 47);
            this.buttonUpdateOktaUsersLogin.Name = "buttonUpdateOktaUsersLogin";
            this.buttonUpdateOktaUsersLogin.Size = new System.Drawing.Size(150, 23);
            this.buttonUpdateOktaUsersLogin.TabIndex = 13;
            this.buttonUpdateOktaUsersLogin.Text = "Update Okta Users Logins";
            this.buttonUpdateOktaUsersLogin.UseVisualStyleBackColor = true;
            this.buttonUpdateOktaUsersLogin.Click += new System.EventHandler(this.buttonUpdateOktaUsersLogin_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Choose  GW Environment";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "ContactId,NewLogin    eg. abPROD:9999,PerryR@fmg.co.nz";
            // 
            // textBoxContactIDsLogins
            // 
            this.textBoxContactIDsLogins.Location = new System.Drawing.Point(15, 143);
            this.textBoxContactIDsLogins.Multiline = true;
            this.textBoxContactIDsLogins.Name = "textBoxContactIDsLogins";
            this.textBoxContactIDsLogins.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxContactIDsLogins.Size = new System.Drawing.Size(452, 89);
            this.textBoxContactIDsLogins.TabIndex = 10;
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(161, 49);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 9;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 237);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Results";
            // 
            // textBoxResults
            // 
            this.textBoxResults.Location = new System.Drawing.Point(15, 261);
            this.textBoxResults.Multiline = true;
            this.textBoxResults.Name = "textBoxResults";
            this.textBoxResults.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResults.Size = new System.Drawing.Size(452, 89);
            this.textBoxResults.TabIndex = 20;
            // 
            // FormUpdateOktaUserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 362);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxResults);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonUpdateOktaUsersLogin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxContactIDsLogins);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormUpdateOktaUserLogin";
            this.Text = "FormUpdateOktaUserLogin";
            this.Load += new System.EventHandler(this.FormUpdateOktaUserLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonUpdateOktaUsersLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContactIDsLogins;
        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxResults;
    }
}