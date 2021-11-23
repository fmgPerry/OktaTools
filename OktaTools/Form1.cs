using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class Form1 : Form
    {
        public static OktaGroup ChosenOktagroup;
        public static string OktaGroupId;
        public static Secrets secrets;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ToggleButtonsEnabled(false); 
            labelStatus.Text = "Status: Ready";
            PopulateComboBoxOktaGroup();
            LoadSecrets();

#if !DEBUG
            buttonTest.Visible = false;
            //comboBoxOktaGroup.Enabled = false;
            //comboBoxOktaGroup.SelectedItem = OktaGroup.test;
            //ChosenOktagroup = OktaGroup.test;
            //ToggleButtonsEnabled(true);
            deleteAllUnlinkedToolStripMenuItem.Visible = false;
#endif


        }

        private void LoadSecrets()
        {
            string secretsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Secrets.json");
            var jsonfile = File.ReadAllText(secretsPath);
            secrets = JsonConvert.DeserializeObject<Secrets>(jsonfile);
        }

        private void ToggleButtonsEnabled(bool enabled, bool enableDump = false)
        {
#if DEBUG
            enabled = true;
#endif
            buttonOktaUsersDump.Enabled = enableDump ? true : enabled;
            
            buttonImportOktaUsers.Enabled = enableDump ? true : enabled;
            buttonDeleteOktaUsers.Enabled = enableDump ? true : enabled;
            buttonResyncOktaUsers.Enabled = enableDump ? true : enabled;

            buttonUpdateUserLogin.Enabled = enabled;
            //buttonResyncOktaUsers.Enabled = enabled;
            buttonOktaProfileSwitcher.Enabled = enabled;
            buttonTest.Enabled = enabled;
        }

        private void PopulateComboBoxOktaGroup()
        {
            comboBoxOktaGroup.Enabled = false;
            comboBoxOktaGroup.Items.Add(OktaGroup.test);
            comboBoxOktaGroup.Items.Add(OktaGroup.prod);
            comboBoxOktaGroup.Enabled = true;
        }

        private async void buttonOktaUsersDump_Click(object sender, EventArgs e)
        {
            if (comboBoxOktaGroup.SelectedItem == null)
            {
                MessageBox.Show("Please choose Okta Group", "No Okta Group chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (new FileController().CanDeleteCSVFile(CsvFilename.AllOktaProfiles))
                {

                    labelStatus.Text = $@"Status: Getting {ChosenOktagroup} Okta Users...";
                    ToggleButtonsEnabled(false);

                    await GetOktaUsersDumpToCSV();

                    labelStatus.Text = "Status: Ready";
                    ToggleButtonsEnabled(true); 
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void buttonImportOktaUsers_Click(object sender, EventArgs e)
        {
            if (comboBoxOktaGroup.SelectedItem == null)
            {
                MessageBox.Show("Please choose Okta Group", "No Okta Group chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var formImportOktaUsers = new FormImportOktaUsers();
            formImportOktaUsers.ShowDialog();
        }

        

        private static async Task GetOktaUsersDumpToCSV()
        {
            try
            {
                var oktaController = new OktaController();
                var groupUsers = await oktaController.GetUsersByGroupId(OktaGroupId);
                new FileController().CreateOktaUsersDumpFile(groupUsers, CsvFilename.AllOktaProfiles);
            }
            catch (Exception ex)
            {

            }
        }

        private static void SetOktaGroupId()
        {
            switch (ChosenOktagroup)
            {
                case OktaGroup.prod:
                    OktaGroupId = secrets.oktaGroupId.prod;
                    break;
                case OktaGroup.test:
                    OktaGroupId = secrets.oktaGroupId.nonProd;
                    break;
                default: break;
            }
        }

        private void buttonDeleteOktaUsers_Click(object sender, EventArgs e)
        {
            //do nothing, show contextmenustrip instead from MouseClick event
        }

        private void buttonDeleteOktaUsers_MouseClick(object sender, MouseEventArgs e)
        {
            if (comboBoxOktaGroup.SelectedItem == null)
            {
                MessageBox.Show("Please choose Okta Group", "No Okta Group chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (e.Button is MouseButtons.Left)
            {
                contextMenuStripDeleteOktaUsers.Show(buttonDeleteOktaUsers, new Point(e.X, e.Y));
            }
        }

        private void deleteByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDeleteOktaUserByID().ShowDialog();            
        }

        private void deleteByTestEnvironmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDeleteOktaUsers().ShowDialog();
        }

        private async void buttonTest_Click(object sender, EventArgs e)
        {
            var authLevels = new List<string>();
            //authLevels.Add("Act 234");
            //authLevels.Add("None ");
            //authLevels.Add("Full");
            //authLevels.Add("View 234");

            foreach (var auth in authLevels)
            {
                if (auth.EndsWith("234")) continue;

                var x = 0;
                x++;
            }

            var d = string.Join(", ", authLevels);

            var url = @"http://dev0085:7000/v2/servers/EPH0006";
            var client = new HttpClient();
            var ss = await client.GetAsync(url);
            var s = await ss.Content.ReadAsStringAsync();


        }

        private void deleteAllUnlinkedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormDeleteOktaUsersAllUnlinked().ShowDialog();
        }

        private void buttonUpdateUserLogin_Click(object sender, EventArgs e)
        {
            if (comboBoxOktaGroup.SelectedItem == null)
            {
                MessageBox.Show("Please choose Okta Group", "No Okta Group chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            new FormUpdateOktaUserLogin().ShowDialog();
        }

        private void comboBoxOktaGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenOktagroup = (OktaGroup)Enum.Parse(typeof(OktaGroup), comboBoxOktaGroup.Text);
            SetOktaGroupId();
            ToggleButtonsEnabled(ChosenOktagroup == OktaGroup.test, enableDump: true);
        }

        private void buttonResyncOktaUsers_Click(object sender, EventArgs e)
        {
            new FormResyncOktaUsers().ShowDialog();
        }

        private void buttonOktaProfileSwitcher_Click(object sender, EventArgs e)
        {
            new FormOktaProfileSwitcher().ShowDialog();
        }
    }
}
