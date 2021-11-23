namespace OktaTools
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonOktaUsersDump = new System.Windows.Forms.Button();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonImportOktaUsers = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonDeleteOktaUsers = new System.Windows.Forms.Button();
            this.contextMenuStripDeleteOktaUsers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteByIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteByTestEnvironmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllUnlinkedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonUpdateUserLogin = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxOktaGroup = new System.Windows.Forms.ComboBox();
            this.buttonResyncOktaUsers = new System.Windows.Forms.Button();
            this.buttonOktaProfileSwitcher = new System.Windows.Forms.Button();
            this.contextMenuStripDeleteOktaUsers.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOktaUsersDump
            // 
            this.buttonOktaUsersDump.Location = new System.Drawing.Point(12, 79);
            this.buttonOktaUsersDump.Name = "buttonOktaUsersDump";
            this.buttonOktaUsersDump.Size = new System.Drawing.Size(150, 23);
            this.buttonOktaUsersDump.TabIndex = 1;
            this.buttonOktaUsersDump.Text = "Okta Users Dump";
            this.buttonOktaUsersDump.UseVisualStyleBackColor = true;
            this.buttonOktaUsersDump.Click += new System.EventHandler(this.buttonOktaUsersDump_Click);
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(189, 79);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(150, 23);
            this.buttonTest.TabIndex = 3;
            this.buttonTest.Text = "Execute Test";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // buttonImportOktaUsers
            // 
            this.buttonImportOktaUsers.Location = new System.Drawing.Point(12, 108);
            this.buttonImportOktaUsers.Name = "buttonImportOktaUsers";
            this.buttonImportOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonImportOktaUsers.TabIndex = 4;
            this.buttonImportOktaUsers.Text = "Import Okta Users";
            this.buttonImportOktaUsers.UseVisualStyleBackColor = true;
            this.buttonImportOktaUsers.Click += new System.EventHandler(this.buttonImportOktaUsers_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.Location = new System.Drawing.Point(23, 15);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(64, 20);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "Status: ";
            // 
            // buttonDeleteOktaUsers
            // 
            this.buttonDeleteOktaUsers.Location = new System.Drawing.Point(12, 137);
            this.buttonDeleteOktaUsers.Name = "buttonDeleteOktaUsers";
            this.buttonDeleteOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonDeleteOktaUsers.TabIndex = 10;
            this.buttonDeleteOktaUsers.Text = "Delete Okta Users";
            this.buttonDeleteOktaUsers.UseVisualStyleBackColor = true;
            this.buttonDeleteOktaUsers.Click += new System.EventHandler(this.buttonDeleteOktaUsers_Click);
            this.buttonDeleteOktaUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonDeleteOktaUsers_MouseClick);
            // 
            // contextMenuStripDeleteOktaUsers
            // 
            this.contextMenuStripDeleteOktaUsers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteByIDToolStripMenuItem,
            this.deleteByTestEnvironmentToolStripMenuItem,
            this.deleteAllUnlinkedToolStripMenuItem});
            this.contextMenuStripDeleteOktaUsers.Name = "contextMenuStripDeleteOktaUsers";
            this.contextMenuStripDeleteOktaUsers.Size = new System.Drawing.Size(195, 70);
            // 
            // deleteByIDToolStripMenuItem
            // 
            this.deleteByIDToolStripMenuItem.Name = "deleteByIDToolStripMenuItem";
            this.deleteByIDToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteByIDToolStripMenuItem.Text = "Delete by Contact IDs";
            this.deleteByIDToolStripMenuItem.ToolTipText = "delete users by contact ID";
            this.deleteByIDToolStripMenuItem.Click += new System.EventHandler(this.deleteByIDToolStripMenuItem_Click);
            // 
            // deleteByTestEnvironmentToolStripMenuItem
            // 
            this.deleteByTestEnvironmentToolStripMenuItem.Name = "deleteByTestEnvironmentToolStripMenuItem";
            this.deleteByTestEnvironmentToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteByTestEnvironmentToolStripMenuItem.Text = "Delete by Environment";
            this.deleteByTestEnvironmentToolStripMenuItem.ToolTipText = "delete users that have their logins prefixed by test env";
            this.deleteByTestEnvironmentToolStripMenuItem.Click += new System.EventHandler(this.deleteByTestEnvironmentToolStripMenuItem_Click);
            // 
            // deleteAllUnlinkedToolStripMenuItem
            // 
            this.deleteAllUnlinkedToolStripMenuItem.Name = "deleteAllUnlinkedToolStripMenuItem";
            this.deleteAllUnlinkedToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteAllUnlinkedToolStripMenuItem.Text = "Delete All Unlinked";
            this.deleteAllUnlinkedToolStripMenuItem.Click += new System.EventHandler(this.deleteAllUnlinkedToolStripMenuItem_Click);
            // 
            // buttonUpdateUserLogin
            // 
            this.buttonUpdateUserLogin.Location = new System.Drawing.Point(12, 166);
            this.buttonUpdateUserLogin.Name = "buttonUpdateUserLogin";
            this.buttonUpdateUserLogin.Size = new System.Drawing.Size(150, 23);
            this.buttonUpdateUserLogin.TabIndex = 11;
            this.buttonUpdateUserLogin.Text = "Update User Login";
            this.buttonUpdateUserLogin.UseVisualStyleBackColor = true;
            this.buttonUpdateUserLogin.Click += new System.EventHandler(this.buttonUpdateUserLogin_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Choose Okta Group";
            // 
            // comboBoxOktaGroup
            // 
            this.comboBoxOktaGroup.FormattingEnabled = true;
            this.comboBoxOktaGroup.Location = new System.Drawing.Point(168, 46);
            this.comboBoxOktaGroup.Name = "comboBoxOktaGroup";
            this.comboBoxOktaGroup.Size = new System.Drawing.Size(150, 21);
            this.comboBoxOktaGroup.TabIndex = 22;
            this.comboBoxOktaGroup.SelectedIndexChanged += new System.EventHandler(this.comboBoxOktaGroup_SelectedIndexChanged);
            // 
            // buttonResyncOktaUsers
            // 
            this.buttonResyncOktaUsers.Location = new System.Drawing.Point(12, 195);
            this.buttonResyncOktaUsers.Name = "buttonResyncOktaUsers";
            this.buttonResyncOktaUsers.Size = new System.Drawing.Size(150, 23);
            this.buttonResyncOktaUsers.TabIndex = 24;
            this.buttonResyncOktaUsers.Text = "Resync Okta Users";
            this.buttonResyncOktaUsers.UseVisualStyleBackColor = true;
            this.buttonResyncOktaUsers.Click += new System.EventHandler(this.buttonResyncOktaUsers_Click);
            // 
            // buttonOktaProfileSwitcher
            // 
            this.buttonOktaProfileSwitcher.Location = new System.Drawing.Point(12, 224);
            this.buttonOktaProfileSwitcher.Name = "buttonOktaProfileSwitcher";
            this.buttonOktaProfileSwitcher.Size = new System.Drawing.Size(150, 23);
            this.buttonOktaProfileSwitcher.TabIndex = 25;
            this.buttonOktaProfileSwitcher.Text = "OktaProfileSwitcher";
            this.buttonOktaProfileSwitcher.UseVisualStyleBackColor = true;
            this.buttonOktaProfileSwitcher.Click += new System.EventHandler(this.buttonOktaProfileSwitcher_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 272);
            this.Controls.Add(this.buttonOktaProfileSwitcher);
            this.Controls.Add(this.buttonResyncOktaUsers);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxOktaGroup);
            this.Controls.Add(this.buttonUpdateUserLogin);
            this.Controls.Add(this.buttonDeleteOktaUsers);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonImportOktaUsers);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonOktaUsersDump);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Okta Management";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStripDeleteOktaUsers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOktaUsersDump;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonImportOktaUsers;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonDeleteOktaUsers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDeleteOktaUsers;
        private System.Windows.Forms.ToolStripMenuItem deleteByIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteByTestEnvironmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllUnlinkedToolStripMenuItem;
        private System.Windows.Forms.Button buttonUpdateUserLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxOktaGroup;
        private System.Windows.Forms.Button buttonResyncOktaUsers;
        private System.Windows.Forms.Button buttonOktaProfileSwitcher;
    }
}

