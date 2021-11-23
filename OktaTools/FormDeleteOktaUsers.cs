using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormDeleteOktaUsers : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        public static List<IUser> OktaUsersToDelete = new List<IUser>();
        public static List<IUser> ChosenEnvironmentOktaUsers = new List<IUser>();

        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;
        private bool DeleteUnlinkedOnly;

        private static OktaController oktaController = new OktaController();
        private static GWPCController gwpcController = new GWPCController();


        public FormDeleteOktaUsers()
        {
            InitializeComponent();
        }

        private async void FormDeleteOktaUsers_Load(object sender, EventArgs e)
        {
            comboBoxEnvironments.Enabled = false;
            buttonDeleteOktaUsers.Enabled = false;

            await PopulateEphemEnvs();
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();
            await PopulateOktaUsersAsync();
        }

        private async Task PopulateEphemEnvs()
        {
            var ephemeralController = new EphemeralController();
            EphemEnvs.Clear();
            EphemEnvs = await ephemeralController.GetEnvironments();
        }

        private async Task PopulateOktaUsersAsync()
        {
            labelStatus.Text = @"Status: Preparing data...";
            
            var client = Okta_Client.Get();
            var group = await client.Groups.GetGroupAsync(Form1.OktaGroupId);
            var groupUsers = await group.Users.ToListAsync();
            OktaUsers = groupUsers;

            comboBoxEnvironments.Enabled = true;
            buttonDeleteOktaUsers.Enabled = true;
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

        }

        private void comboBoxEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newEnvironment = comboBoxEnvironments.SelectedItem as EphemeralEnv;
            ChosenEnvironment = newEnvironment;
            ChosenEnvironmentOktaUsers = OktaUsers.Where(u => u.Profile.Login.StartsWith($@"{ChosenEnvironment.Boomi}.")).ToList();
        }

        private async void buttonDeleteOktaUsers_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newLine = Environment.NewLine;
            var result = MessageBox.Show(text: $@"Delete All or Unlinked Users Only?{newLine}Yes - Delete All{newLine}No - Unlinked Users Only"
                    , caption: @"Delete All or Unlinked Only", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;

            DeleteUnlinkedOnly = result == DialogResult.No;

            labelStatus.Text = @"Status: Deleting Okta Users...";
            buttonDeleteOktaUsers.Enabled = false;

            await DeleteOktaUsers();
            labelStatus.Text = @"Status: Users deleted from Okta";
        }

        private async Task DeleteOktaUsers()
        {
            foreach(var oktaUser in ChosenEnvironmentOktaUsers)
            {
                if (OkToDelete(oktaUser))
                {
                    var contactId = Obfuscation.ReverseObfuscate(oktaUser.Profile["Public_ID"].ToString());
                    var contactAccount = gwpcController.GetContactAccount(contactId, ChosenEnvironment);
                    var contact = contactAccount.AccountContacts.FirstOrDefault(c => c.ContactID == contactId);

                    if (DeleteUnlinkedOnly)
                    {
                        //link broken if FMGConnectLogin and Okta login do not match 
                        if (contact.FMGConnectLogin != oktaUser.Profile.Login)
                        {
                            OktaUsersToDelete.Add(oktaUser);
                        }
                    }
                    else
                    {
                        OktaUsersToDelete.Add(oktaUser);
                    }
                }
            }

            var deletedUsers = await oktaController.DeleteUserByEnvironment(OktaUsersList.FormDeleteOktaUsers);

            foreach (var oktaUser in deletedUsers)
            {
                var contactId = Obfuscation.ReverseObfuscate(oktaUser.Profile["Public_ID"].ToString());
                gwpcController.UpdateContact(contactId, string.Empty, ChosenEnvironment);
            }

            OktaUsersToDelete.RemoveAll(ud => deletedUsers.Any(du => du == ud));

            new FileController().CreateOktaUsersDumpFile(deletedUsers, CsvFilename.DeletedOktaProfiles);

        }

        private bool OkToDelete(IUser oktaUser)
        {
            var publicId = oktaUser.Profile["Public_ID"]?.ToString() ?? "";
            var okToDelete = true;
            okToDelete = okToDelete && !string.IsNullOrEmpty(publicId);
            okToDelete = okToDelete && (!oktaUser.Profile.LastName.EndsWith("(OktaProfileSwitcher)"));
            okToDelete = okToDelete && (!oktaUser.Profile.Login.EndsWith("@alphero.com"));

            return okToDelete;
        }
    }
}
