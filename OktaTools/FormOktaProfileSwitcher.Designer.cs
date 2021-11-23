namespace OktaTools
{
    partial class FormOktaProfileSwitcher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOktaProfileSwitcher));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonSetOktaProfileSwitcher = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxEnvironments = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxContactId = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFirstname = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxLastname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxSecondaryEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPrimaryEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(53, 23);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 36;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(123, 288);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(150, 23);
            this.buttonReset.TabIndex = 35;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // buttonSetOktaProfileSwitcher
            // 
            this.buttonSetOktaProfileSwitcher.Location = new System.Drawing.Point(123, 259);
            this.buttonSetOktaProfileSwitcher.Name = "buttonSetOktaProfileSwitcher";
            this.buttonSetOktaProfileSwitcher.Size = new System.Drawing.Size(150, 23);
            this.buttonSetOktaProfileSwitcher.TabIndex = 34;
            this.buttonSetOktaProfileSwitcher.Text = "Set OktaProfileSwitcher";
            this.buttonSetOktaProfileSwitcher.UseVisualStyleBackColor = true;
            this.buttonSetOktaProfileSwitcher.Click += new System.EventHandler(this.buttonSetOktaProfileSwitcher_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 33;
            this.label2.Text = "Choose Environment";
            // 
            // comboBoxEnvironments
            // 
            this.comboBoxEnvironments.FormattingEnabled = true;
            this.comboBoxEnvironments.Location = new System.Drawing.Point(123, 52);
            this.comboBoxEnvironments.Name = "comboBoxEnvironments";
            this.comboBoxEnvironments.Size = new System.Drawing.Size(150, 21);
            this.comboBoxEnvironments.TabIndex = 32;
            this.comboBoxEnvironments.SelectedIndexChanged += new System.EventHandler(this.comboBoxEnvironments_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 38;
            this.label3.Text = "Username";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Location = new System.Drawing.Point(123, 79);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(150, 20);
            this.textBoxUsername.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 40;
            this.label1.Text = "AddressBookUID";
            // 
            // textBoxContactId
            // 
            this.textBoxContactId.Location = new System.Drawing.Point(123, 105);
            this.textBoxContactId.Name = "textBoxContactId";
            this.textBoxContactId.Size = new System.Drawing.Size(150, 20);
            this.textBoxContactId.TabIndex = 39;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 42;
            this.label4.Text = "** Firstname";
            // 
            // textBoxFirstname
            // 
            this.textBoxFirstname.Location = new System.Drawing.Point(123, 131);
            this.textBoxFirstname.Name = "textBoxFirstname";
            this.textBoxFirstname.Size = new System.Drawing.Size(150, 20);
            this.textBoxFirstname.TabIndex = 41;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 44;
            this.label5.Text = "** Lastname";
            // 
            // textBoxLastname
            // 
            this.textBoxLastname.Location = new System.Drawing.Point(123, 157);
            this.textBoxLastname.Name = "textBoxLastname";
            this.textBoxLastname.Size = new System.Drawing.Size(150, 20);
            this.textBoxLastname.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "** Secondary Email";
            // 
            // textBoxSecondaryEmail
            // 
            this.textBoxSecondaryEmail.Location = new System.Drawing.Point(123, 209);
            this.textBoxSecondaryEmail.Name = "textBoxSecondaryEmail";
            this.textBoxSecondaryEmail.Size = new System.Drawing.Size(150, 20);
            this.textBoxSecondaryEmail.TabIndex = 47;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 13);
            this.label7.TabIndex = 48;
            this.label7.Text = "** Primary Email";
            // 
            // textBoxPrimaryEmail
            // 
            this.textBoxPrimaryEmail.Location = new System.Drawing.Point(123, 183);
            this.textBoxPrimaryEmail.Name = "textBoxPrimaryEmail";
            this.textBoxPrimaryEmail.Size = new System.Drawing.Size(150, 20);
            this.textBoxPrimaryEmail.TabIndex = 45;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 331);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "** - Optional / Override";
            // 
            // FormOktaProfileSwitcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 361);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxPrimaryEmail);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxSecondaryEmail);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxLastname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxFirstname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxContactId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonSetOktaProfileSwitcher);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxEnvironments);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormOktaProfileSwitcher";
            this.Text = "FormOktaProfileSwitcher";
            this.Load += new System.EventHandler(this.FormOktaProfileSwitcher_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonSetOktaProfileSwitcher;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxEnvironments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxContactId;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFirstname;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxLastname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxSecondaryEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPrimaryEmail;
        private System.Windows.Forms.Label label8;
    }
}