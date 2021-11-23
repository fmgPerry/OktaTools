using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormResyncOktaUsers : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        public static List<IUser> ResyncedOktaUsers = new List<IUser>();

        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;

        private static OktaController oktaController = new OktaController();
        private static GWPCController gwpcController = new GWPCController();

        private static Secretkey secretkey;
        private static string itemsFromFile = "Items from file";
        private static List<string> Items = new List<string>();
        private static List<List<string>> GroupedItems = new List<List<string>>();
        private bool isBulk = false;
        private bool isUpdateGW = true;


        private static int timeAlloted = 66000;
        private static int batchSize = 150;
        private static SemaphoreSlim SS = new SemaphoreSlim(20, 25);
        private static GetUserBy ChosenGetUserBy;
        public FormResyncOktaUsers()
        {
            InitializeComponent();
        }

        private async void FormResyncOktaUsers_Load(object sender, EventArgs e)
        {
            labelStatus.Text = @"Status: Ready";
            comboBoxEnvironments.Enabled = false;
            comboBoxListType.Enabled = false;
            buttonReset.Enabled = false;
            buttonGetContactIDs.Enabled = false;            
            PopulateComboBoxListType();

            await PopulateEphemEnvs();
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();

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

        private void PopulateComboBoxListType()
        {
            comboBoxListType.Items.Add(GetUserBy.contactId);
            comboBoxListType.Items.Add(GetUserBy.publicId);
            comboBoxListType.Items.Add(GetUserBy.login);
            comboBoxListType.Items.Add(GetUserBy.oktaId);

            comboBoxListType.Enabled = true;
        }

        private void comboBoxEnvironments_SelectedIndexChanged(object sender, EventArgs e)
        {
            var newEnvironment = comboBoxEnvironments.SelectedItem as EphemeralEnv;
            ChosenEnvironment = newEnvironment;
            secretkey = Obfuscation.GetSecretKey(ChosenEnvironment.Boomi);
        }

        private void comboBoxListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenGetUserBy = (GetUserBy)Enum.Parse(typeof(GetUserBy), comboBoxListType.Text);
            buttonGetContactIDs.Text = $@"Get {ChosenGetUserBy}s from file";            
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

        private void openFileDialogGetContactIDs_FileOk(object sender, CancelEventArgs e)
        {
            var filename = openFileDialogGetContactIDs.FileName;
            Items = FileController.ReadCSVFile(filename);
            Items = Items.Distinct().ToList();
            textBoxContactIDs.Text = $@"{itemsFromFile}{Environment.NewLine}{Items.Count}";
            textBoxContactIDs.Enabled = false;
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            labelStatus.Text = @"Status: Ready";
            textBoxContactIDs.Text = "";
            OktaUsers.Clear();
            GroupedItems.Clear();
            ResyncedOktaUsers.Clear();
            //await PopulateOktaUsersAsync();
            buttonResyncOktaUsers.Enabled = true;
            buttonReset.Enabled = false;
        }

        private async void buttonResyncOktaUsers_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!isBulk)
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

            if (new FileController().CanDeleteCSVFile(CsvFilename.ResyncedOktaProfiles))
            {

                labelStatus.Text = @"Status: Resyncing Okta Users...";
                buttonResyncOktaUsers.Enabled = false;

                GroupItems(Items);

                await ResyncOktaUsers();
                labelStatus.Text = @"Status: Users resynced to Okta";
                new FileController().CreateOktaUsersDumpFile(ResyncedOktaUsers.Distinct().ToList(), CsvFilename.ResyncedOktaProfiles);
                buttonReset.Enabled = true;
            }
        }
        private async Task ResyncUser(string item)
        {
            try
            {
                await SS.WaitAsync();
                await ResyncOktaProfile(item);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                SS.Release();
            }

        }

        private static async Task ResyncOktaProfile(string contactId)
        {
            var contactAccount = gwpcController.GetContactAccount(contactId, ChosenEnvironment);
            if (contactAccount != null)
            {
                if (contactAccount.AccountContacts.FirstOrDefault(c => c.ContactID == contactId) != null)
                {
                    var oktaId = await oktaController.ResyncOktaProfile(contactAccount, ChosenEnvironment.Boomi);
                    if (oktaId != null)
                    {
                        // prepend oktaId with test env if test
                        if (Form1.ChosenOktagroup == OktaGroup.test)
                        {
                            oktaId = $@"{ChosenEnvironment.Boomi}.{oktaId}";
                        }
                        gwpcController.UpdateContact(contactId, oktaId, ChosenEnvironment);
                    }
                }
            }
            
        }
        private async Task ResyncOktaUsers()
        {
            var isDelay = GroupedItems.Count > 1;
            foreach (var items in GroupedItems)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var tasks = items.Select(i => Task.Run(() => ResyncUser(i)));
                await Task.WhenAll(tasks.ToArray());
                stopwatch.Stop();
                var timeRemaining = timeAlloted - stopwatch.ElapsedMilliseconds;
                if (isDelay && timeRemaining > 0)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(timeRemaining));
                }
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
    }
}
