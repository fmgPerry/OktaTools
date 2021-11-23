using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormDeleteOktaUsersAllUnlinked : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        public static List<IUser> OktaUsersToDelete = new List<IUser>();
        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();

        public FormDeleteOktaUsersAllUnlinked()
        {
            InitializeComponent();
        }


        private async void FormDeleteOktaUsersAllUnlinked_Load(object sender, EventArgs e)
        {
            PopulateEphemeralEnvironmentsAsync();
            await PopulateOktaUsersAsync();
        }

        private async void PopulateEphemeralEnvironmentsAsync()
        {
            var ephemeralController = new EphemeralController();
            EphemEnvs.Clear();
            EphemEnvs = await ephemeralController.GetEnvironments();

        }

        private async Task PopulateOktaUsersAsync()
        {
            labelStatus.Text = @"Status: Preparing data...";
            buttonDeleteOktaUsers.Enabled = false;

            var client = Okta_Client.Get();
            var group = await client.Groups.GetGroupAsync(Form1.OktaGroupId);
            var groupUsers = await group.Users.ToListAsync();
            OktaUsers = groupUsers;
            OktaUsersToDelete = groupUsers;

            buttonDeleteOktaUsers.Enabled = true;
            labelStatus.Text = @"Status: Ready";
        }

        private async void buttonDeleteOktaUsers_Click(object sender, EventArgs e)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var fileController = new FileController();
            if (fileController.CanDeleteCSVFile(CsvFilename.CanBeDeletedOktaProfiles))
            {
                var newLine = Environment.NewLine;
                var result = MessageBox.Show(text: $@"Get All Unlinked Users then Delete?{newLine}Yes - Get a list then Delete{newLine}No - Get a list only"
                    ,caption: @"Get and/or Delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (result != DialogResult.Cancel)
                {
                    labelStatus.Text = @"Status: Getting Unlinked Users...";
                    buttonDeleteOktaUsers.Enabled = false;

                    GetAllUnlinkedOktaUsers();
                    fileController.CreateOktaUsersDumpFile(OktaUsersToDelete, CsvFilename.CanBeDeletedOktaProfiles);

                    if (result == DialogResult.Yes)
                    {
                        labelStatus.Text = @"Status: Deleting Unlinked Users...";
                        var deletedUsers = await DeleteAllUnlinkedOktaUsers();
                        fileController.CreateOktaUsersDumpFile(deletedUsers, CsvFilename.DeletedOktaProfiles);
                    }
                }
            }
            
            stopwatch.Stop();
            var elapsed = stopwatch.Elapsed;
            var elapsedString = $@"{elapsed.Minutes} minutes and {elapsed.Seconds} seconds";

            buttonDeleteOktaUsers.Enabled = true;
            labelStatus.Text = $@"Status: Done in {elapsedString}.";

        }

        private async Task<List<IUser>> DeleteAllUnlinkedOktaUsers()
        {
            var oktaController = new OktaController();
            var deletedUsers = await oktaController.DeleteUserByEnvironment(OktaUsersList.FormDeleteOktaUsers);
            return deletedUsers;
        }

        private static void GetAllUnlinkedOktaUsers()
        {
            var oktaController = new OktaController();
            var gwpcController = new GWPCController();

            var oktaUsersToRemoveFromList = new List<IUser>();

            Parallel.ForEach(EphemEnvs, ephemeralEnv =>
            //foreach (var ephemeralEnv in EphemEnvs)
            {
                var oktaUsersToRemoveFromListByEnv = new List<IUser>();

                Parallel.ForEach(OktaUsers, oktaUser =>
                //foreach (var oktaUser in OktaUsers)
                {
                    // remove from list if OktaProfileSwitcher
                    if (oktaUser.Profile.LastName.EndsWith("(OktaProfileSwitcher)"))
                    {
                        oktaUsersToRemoveFromList.Add(oktaUser);
                    }

                    //remove from list if alphero profile
                    if (oktaUser.Profile.Login.EndsWith("@alphero.com"))
                    {
                        oktaUsersToRemoveFromList.Add(oktaUser);
                    }

                    // remove from list if no Public_ID set
                    var publicId = oktaUser.Profile["Public_ID"]?.ToString() ?? "";
                    if (string.IsNullOrEmpty(publicId))
                    {
                        oktaUsersToRemoveFromListByEnv.Add(oktaUser);
                        oktaUsersToRemoveFromList.Add(oktaUser);
                    }

                    var contactId = Obfuscation.TryReverseObfuscatePublicId(publicId, Form1.ChosenOktagroup);
                    var contactAccount = gwpcController.GetContactAccount(contactId, ephemeralEnv);
                    var contact = contactAccount?.AccountContacts.FirstOrDefault(c => c.ContactID == contactId);

                    // linked if FMGConnectLogin and Okta login matches
                    if (contact == null || contact.FMGConnectLogin == oktaUser.Profile.Login)
                    {
                        oktaUsersToRemoveFromList.Add(oktaUser);
                    }
                }
                );

                // Optimisation, no need to look for it again if already found
                // fails on Parallel foreach
                //OktaUsers.RemoveAll(ud => oktaUsersToRemoveFromListByEnv.Any(ur => ur == ud));
            }
            );
            OktaUsersToDelete.RemoveAll(ud => oktaUsersToRemoveFromList.Any(ur => ur == ud));
        }
    }
}
