using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormDeleteOktaUserByID : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        public static List<IUser> DeletedOktaUsers = new List<IUser>();

        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;

        private static OktaController oktaController = new OktaController();
        private static GWPCController gwpcController = new GWPCController();

        private static Secretkey secretkey;
        private static string itemsFromFile = "Items from file";
        private static List<string> Items = new List<string>();
        private static List<List<string>> GroupedItems = new List<List<string>>();
        private bool isBulk = false;
        private bool isUpdateGW = false;


        private static int timeAlloted = 66000;
        private static int batchSize = 150;
        private static SemaphoreSlim SS = new SemaphoreSlim(20, 25);
        private static GetUserBy ChosenGetUserBy;


        public FormDeleteOktaUserByID()
        {
            InitializeComponent();
        }

        private async void FormDeleteOktaUserByID_Load(object sender, EventArgs e)
        {
            labelStatus.Text = @"Status: Ready";
            comboBoxEnvironments.Enabled = false;
            comboBoxListType.Enabled = false;
            buttonReset.Enabled = false;
            buttonGetContactIDs.Enabled = false;
            checkBoxUpdateGW.Visible = false;
            PopulateComboBoxListType();

            await PopulateEphemEnvs();
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();
            //await PopulateOktaUsersAsync();
        }

        private void PopulateComboBoxListType()
        {
            comboBoxListType.Items.Add(GetUserBy.contactId);
            comboBoxListType.Items.Add(GetUserBy.publicId);
            comboBoxListType.Items.Add(GetUserBy.login);
            comboBoxListType.Items.Add(GetUserBy.oktaId);

            comboBoxListType.Enabled = true;
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

            comboBoxEnvironments.Enabled = true;
        }

        private void comboBoxEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newEnvironment = comboBoxEnvironments.SelectedItem as EphemeralEnv;
            ChosenEnvironment = newEnvironment;
            secretkey = Obfuscation.GetSecretKey(ChosenEnvironment.Boomi);
        }

        private async void buttonDeleteOktaUsers_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(!isBulk)            
            {
                var contactIDs = textBoxContactIDs.Text.TrimEnd('\r', '\n').Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                Items = contactIDs.Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList();
                if (Items.Count > 100)
                {
                    MessageBox.Show("Please limit the contactIDs to 100", "100 ContactIDs Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

            }

            Items = Items.Distinct().ToList();

            if (Items.Count == 0)
            {
                MessageBox.Show("No ContactIDs to process", "No ContactIDs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if (new FileController().CanDeleteCSVFile(CsvFilename.DeletedOktaProfiles))
            {

                labelStatus.Text = @"Status: Deleting Okta Users...";
                buttonDeleteOktaUsers.Enabled = false;

                GroupItems(Items);

                await DeleteOktaUsers();
                labelStatus.Text = @"Status: Users deleted from Okta";
                new FileController().CreateOktaUsersDumpFile(DeletedOktaUsers.Distinct().ToList(), CsvFilename.DeletedOktaProfiles);
                buttonReset.Enabled = true;
            }
        }

        private void GroupItems(List<string> items)
        {
            var batch = items.Take(batchSize).ToList();
            GroupedItems.Add(batch);
            items = items.Except(batch).ToList();
            if (items.Count > 0)
            {
                GroupItems(items);
            }
        }

        private async Task DeleteOktaUsers()
        {
            var isDelay = GroupedItems.Count > 1;
            foreach (var items in GroupedItems)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var tasks = items.Select(i => Task.Run(() => DeleteUser(i)));
                await Task.WhenAll(tasks.ToArray());
                stopwatch.Stop();
                var timeRemaining = timeAlloted - stopwatch.ElapsedMilliseconds;
                if (isDelay && timeRemaining > 0)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(timeRemaining));
                }
            }

        }

        private async Task DeleteUser(string item)
        {
            try
            {
                await SS.WaitAsync();
                switch (ChosenGetUserBy)
                {
                    case GetUserBy.contactId:
                        await oktaController.DeleteUserByContactId(item, OktaUsersList.FormDeleteOktaUserByID, secretkey, updateList: true, userFromList: false);
                        if(isUpdateGW)
                        {
                            gwpcController.UpdateContact(item, string.Empty, ChosenEnvironment);
                        }
                        break;
                    case GetUserBy.publicId:
                        await oktaController.DeleteUserByPublicID(item, OktaUsersList.FormDeleteOktaUserByID);
                        break;
                    case GetUserBy.oktaId:
                    case GetUserBy.login:
                        await  oktaController.DeleteUserByOktaIDorLogin(item, OktaUsersList.FormDeleteOktaUserByID);
                        break;
                }

                

            }
            catch (Exception)
            {

            }
            finally
            {
                SS.Release();
            }

        }

        private async Task DeleteOktaUsers(List<string> contactIDs)
        {
            foreach (var contactId in contactIDs)
            {
                if (ChosenGetUserBy == GetUserBy.publicId)
                {
                    var publicId = contactId;
                    await oktaController.DeleteUserByPublicID(publicId, OktaUsersList.FormDeleteOktaUserByID);
                    var addressbookuid = Obfuscation.TryReverseObfuscatePublicId(publicId, Form1.ChosenOktagroup);
                    gwpcController.UpdateContact(addressbookuid, string.Empty, ChosenEnvironment);
                }
                else
                {
                    await oktaController.DeleteUserByContactId(contactId, OktaUsersList.FormDeleteOktaUserByID, secretkey, updateList: true, userFromList: false);
                    gwpcController.UpdateContact(contactId, string.Empty, ChosenEnvironment);
                }
                
            }
        }
        
        private async void buttonReset_Click(object sender, EventArgs e)
        {
            labelStatus.Text = @"Status: Ready";
            textBoxContactIDs.Text = "";
            OktaUsers.Clear();
            GroupedItems.Clear();
            DeletedOktaUsers.Clear();
            //await PopulateOktaUsersAsync();
            buttonDeleteOktaUsers.Enabled = true;
            buttonReset.Enabled = false;
        }

        private void checkBoxIsBulk_CheckedChanged(object sender, EventArgs e)
        {
            isBulk = checkBoxIsBulk.Checked;
            textBoxContactIDs.Text = string.Empty;
            textBoxContactIDs.Enabled = !isBulk;
            buttonGetContactIDs.Enabled = isBulk;
        }

        private void buttonGetContactIDs_Click(object sender, EventArgs e)
        {
            openFileDialogGetContactIDs.ShowDialog();
        }

        private void openFileDialogGetContactIDs_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var filename = openFileDialogGetContactIDs.FileName;
            Items = FileController.ReadCSVFile(filename);
            Items = Items.Distinct().ToList();
            textBoxContactIDs.Text = $@"{itemsFromFile}{Environment.NewLine}{Items.Count}";
            textBoxContactIDs.Enabled = false;
        }

        private void comboBoxListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenGetUserBy = (GetUserBy)Enum.Parse(typeof(GetUserBy), comboBoxListType.Text);
            buttonGetContactIDs.Text = $@"Get {ChosenGetUserBy}s from file";
            checkBoxUpdateGW.Visible = ChosenGetUserBy == GetUserBy.contactId;
            checkBoxUpdateGW.Checked = false;
            isUpdateGW = false;
        }

        private void checkBoxUpdateGW_CheckedChanged(object sender, EventArgs e)
        {
            isUpdateGW = checkBoxUpdateGW.Checked;
        }
    }
}
