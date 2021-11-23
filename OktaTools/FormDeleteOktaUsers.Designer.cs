namespace OktaTools
{
    partial class FormDeleteOktaUsers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDeleteOktaUsers));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonDeleteOktaUsers = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(53, 11);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 17;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonDeleteOktaUsers
            // 
            this.buttonDeleteOktaUsers.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.buttonDeleteOktaUsers.Location = new System.Drawing.Point(279, 38);
            this.buttonDeleteOktaUsers.Name = "buttonDeleteOktaUsers";
            this.buttonDeleteOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonDeleteOktaUsers.TabIndex = 13;
            this.buttonDeleteOktaUsers.Text = "Delete Okta Users";
            this.buttonDeleteOktaUsers.UseVisualStyleBackColor = true;
            this.buttonDeleteOktaUsers.Click += new System.EventHandler(this.buttonDeleteOktaUsers_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Choose Environment";
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(123, 40);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 9;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(252, 91);
            this.label1.TabIndex = 18;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // FormDeleteOktaUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 191);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonDeleteOktaUsers);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormDeleteOktaUsers";
            this.Text = "FormDeleteOktaUsers";
            this.Load += new System.EventHandler(this.FormDeleteOktaUsers_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonDeleteOktaUsers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.Label label1;
    }
}