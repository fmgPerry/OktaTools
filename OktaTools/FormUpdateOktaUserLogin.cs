using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormUpdateOktaUserLogin : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        private static string results;
        private static string newLine = Environment.NewLine;
        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;
        private static Secretkey secretkey;

        public FormUpdateOktaUserLogin()
        {
            InitializeComponent();
        }

        private async void FormUpdateOktaUserLogin_Load(object sender, EventArgs e)
        {
            comboBoxEnvironments.Enabled = false;
            
            await PopulateEphemEnvs();
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();
            buttonUpdateOktaUsersLogin.Enabled = true;
            labelStatus.Text = @"Status: Ready";
        }

        private void PopulateComboBoxEnvironments()
        {
            comboBoxEnvironments.DisplayMember = "Name";
            comboBoxEnvironments.ValueMember = "TinyUrl";

            comboBoxEnvironments.Text = "";
            comboBoxEnvironments.SelectedItem = null;
            comboBoxEnvironments.Items.Clear();
            var ephEnvs = EphemEnvs.Where(e => e.OktaUserGroup == Form1.ChosenOktagroup).OrderBy(e => e.Name).ToList();

            foreach (var ephEnv in ephEnvs)
            {
                comboBoxEnvironments.Items.Add(ephEnv);
            }

            comboBoxEnvironments.Enabled = true;
        }

        private async Task PopulateEphemEnvs()
        {
            var ephemeralController = new EphemeralController();
            EphemEnvs.Clear();
            EphemEnvs = await ephemeralController.GetEnvironments();
        }

        private async void buttonUpdateOktaUsersLogin_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            labelStatus.Text = @"Status: Updating User Logins";
            buttonUpdateOktaUsersLogin.Enabled = false;

            var contactIDsLoginsText = Regex.Replace(textBoxContactIDsLogins.Text, @"\r\n|\n|\r", Environment.NewLine);
            var contactIDsLogins = contactIDsLoginsText.Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();

            foreach (var contactIdLogin in contactIDsLogins)
            {
                var contactIdLoginSplit = contactIdLogin.Split(',');
                if (contactIdLoginSplit.Count() == 2)
                {
                    try
                    {
                        var contactId = contactIdLoginSplit[0];
                        var newLogin = contactIdLoginSplit[1];

                        var updateResult = await UpdateOktaUser(contactId, newLogin);

                        results += $@"{contactIdLogin}: {updateResult}{newLine}";
                    }
                    catch (Exception ex)
                    {
                        results += $@"{contactIdLogin}: {ex.Message}{newLine}";
                    }
                }
                else
                {
                    results += $@"{contactIdLogin}: WRONG FORMAT{newLine}";
                }
            }

            labelStatus.Text = @"Status: User Logins Updated";
            textBoxResults.Text = results;
        }

        private async Task<string> UpdateOktaUser(string contactId, string newLogin)
        {
            var oktaController = new OktaController();
            var gwpcController = new GWPCController();

            var isUpdated = await oktaController.UpdateUserLogin(contactId, newLogin, secretkey);
            if (isUpdated)
            {
                // no longer needed to update GW record
                // gwpcController.UpdateContact(contactId, newLogin, ChosenEnvironment);
                return "SUCCESS";
            }
            else
            {
                return "No matching Okta User found";
            }

        }
        private void comboBoxEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newEnvironment = comboBoxEnvironments.SelectedItem as EphemeralEnv;
            ChosenEnvironment = newEnvironment;
            secretkey = Obfuscation.GetSecretKey(ChosenEnvironment.Boomi);
        }
        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxContactIDsLogins.Text = "";
            textBoxResults.Text = "";
            OktaUsers.Clear();
        }
    }
}
