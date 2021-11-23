namespace OktaTools
{
    partial class FormDeleteOktaUsersAllUnlinked
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeleteOktaUsersAllUnlinked));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonDeleteOktaUsers = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(12, 21);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 30;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonDeleteOktaUsers
            // 
            this.buttonDeleteOktaUsers.Location = new System.Drawing.Point(57, 53);
            this.buttonDeleteOktaUsers.Name = "buttonDeleteOktaUsers";
            this.buttonDeleteOktaUsers.Size = new System.Drawing.Size(200, 23);
            this.buttonDeleteOktaUsers.TabIndex = 29;
            this.buttonDeleteOktaUsers.Text = "Delete All Unlinked Okta Users";
            this.buttonDeleteOktaUsers.UseVisualStyleBackColor = true;
            this.buttonDeleteOktaUsers.Click += new System.EventHandler(this.buttonDeleteOktaUsers_Click);
            // 
            // FormDeleteOktaUsersAllUnlinked
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 362);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonDeleteOktaUsers);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeleteOktaUsersAllUnlinked";
            this.Text = "FormDeleteOktaUsersAllUnlinked";
            this.Load += new System.EventHandler(this.FormDeleteOktaUsersAllUnlinked_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonDeleteOktaUsers;
    }
}