using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormImportOktaUsers : Form
    {
        public static List<IUser> OktaUsers = new List<IUser>();
        public static List<IUser> ImportedOktaUsers = new List<IUser>();
        private static List<string> PublicIDs = new List<string>();
        private static List<string> ContactIDs = new List<string>();
        private static List<List<string>> GroupedContactIDs = new List<List<string>>();
        private static List<List<string>> GroupedContactIDsToDelete = new List<List<string>>();
        private static List<string> NotImportedContactIDs = new List<string>();
        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;
        private static string ChosenSecondaryEmail;
        private static ImportMethod ChosenImportMethod;

        private static string contactIDsFromFile = "ContactIDs from file";
        private static string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        private static int timeAlloted = 66000;
        private static int batchSize = 150;
        private static SemaphoreSlim SS = new SemaphoreSlim(20, 25);

        private static OktaController oktaController = new OktaController();
        private static GWPCController gwpcController = new GWPCController();

        private static Secretkey secretkey;

        private static bool deleteFirst = false;

        public FormImportOktaUsers()
        {
            InitializeComponent();
        }

        private async void FormImportOktaUsers_Load(object sender, EventArgs e)
        {
            comboBoxEnvironments.Enabled = false;
            buttonImportOktaUsers.Enabled = false;
            buttonReset.Enabled = false;
            groupBoxImportBy.Visible = false;
            openFileDialogGetContactIDs.InitialDirectory = initialDirectory;
            openFileDialogGetContactIDs.Filter = "CSV|*.csv|Text|*.txt";

            if (Form1.ChosenOktagroup == OktaGroup.test)
            {
#if DEBUG
                textBoxSecondaryEmail.Text = "perry.rosales@fmg.co.nz";
#else
                textBoxSecondaryEmail.Text = "jean.flack@fmg.co.nz";
#endif
            }

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

            if (deleteFirst)
            {
                await GetOktaUsersAsync();
            }

            buttonImportOktaUsers.Enabled = true;
            labelStatus.Text = @"Status: Ready";
        }

        private static async Task GetOktaUsersAsync()
        {
            var client = Okta_Client.Get();
            var group = await client.Groups.GetGroupAsync(Form1.OktaGroupId);
            var groupUsers = await group.Users.ToListAsync();
            OktaUsers = groupUsers;
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

            if(addByBoomiAvailable())
            {
                groupBoxImportBy.Visible = true;
                radioButtonUsingBoomi.Checked = true;
            }
            else
            {
                groupBoxImportBy.Visible = false;
                radioButtonUsingBoomi.Checked = false;
            }
            
        }

        private bool addByBoomiAvailable()
        {
            return ChosenEnvironment.Boomi == "devint" 
                || ChosenEnvironment.Boomi == "preprod"
                || ChosenEnvironment.Boomi == "testa" 
                || ChosenEnvironment.Boomi == "testb"
                || ChosenEnvironment.Boomi == "testd";
        }

        private async void buttonImportOktaUsers_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!textBoxContactIDs.Text.StartsWith(contactIDsFromFile))
            {
                var contactIDs = textBoxContactIDs.Text.TrimEnd('\r', '\n').Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                ContactIDs = contactIDs.Where(s => !string.IsNullOrEmpty(s)).ToList();
            }

            ContactIDs = ContactIDs.Distinct().ToList();

            if (ContactIDs.Count == 0)
            {
                MessageBox.Show("No ContactIDs to process", "No ContactIDs", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (new FileController().CanDeleteLogFile())
            {
                if (new FileController().CanDeleteCSVFile(CsvFilename.ImportedOktaProfiles))
                {
                    buttonReset.Enabled = true;
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    labelStatus.Text = @"Status: Importing Users into Okta...";
                    buttonImportOktaUsers.Enabled = false;

                    ChosenSecondaryEmail = textBoxSecondaryEmail.Text;

                    GroupContactIDs();

                    await DeleteAndAddOktaUsers();

                    stopwatch.Stop();
                    var elapsed = stopwatch.Elapsed;
                    var elapsedString = $@"{elapsed:hh\:mm\:ss}";

                    labelStatus.Text = $@"Status: Imported in {elapsedString}.";
                    textBoxNotImportedContactIDs.Text = String.Join(Environment.NewLine, NotImportedContactIDs.Distinct());

                    new FileController().CreateOktaUsersDumpFile(ImportedOktaUsers, CsvFilename.ImportedOktaProfiles);
                }
            }
        }

        private void GroupContactIDs()
        {
            if (deleteFirst)
            {
                PublicIDs = ContactIDs.Select(c => Obfuscation.Obfuscate(c, secretkey)).ToList();
                var existingPublicIDs = OktaUsers.Select(u => u.Profile["Public_ID"]?.ToString()).ToList();
                existingPublicIDs = existingPublicIDs.Where(e => !string.IsNullOrEmpty(e)).ToList();
                var existingContactIDs = existingPublicIDs.Select(p => Obfuscation.TryReverseObfuscatePublicId(p, Form1.ChosenOktagroup)).ToList();
                var contactIDsToDelete = ContactIDs.Intersect(existingContactIDs).Distinct().ToList();

                GroupContactIDsToDelete(contactIDsToDelete); 
            }

            GroupContactIDsToImport(ContactIDs);
        }
        private void GroupContactIDsToDelete(List<string> contactIDs)
        {
            
            var batch = contactIDs.Take(batchSize).ToList();
            GroupedContactIDsToDelete.Add(batch);
            contactIDs = contactIDs.Except(batch).ToList();
            if (contactIDs.Count > 0)
            {
                GroupContactIDsToDelete(contactIDs);
            }            
        }
        private void GroupContactIDsToImport(List<string> contactIDs)
        {
            var batch = contactIDs.Take(batchSize).ToList();
            GroupedContactIDs.Add(batch);
            contactIDs = contactIDs.Except(batch).ToList();
            if (contactIDs.Count > 0)
            {
                GroupContactIDsToImport(contactIDs);
            }
            
        }

        private async Task DeleteAndAddOktaUsers()
        {

            if (deleteFirst)
            {
                Trace.TraceInformation($@"time: {DateTime.Now:s} Start Delete Users");
                await DeleteUsers();  
            }

            Trace.TraceInformation($@"time: {DateTime.Now:s} Start Create Users");
            await CreateUsers();
            
            Trace.TraceInformation($@"time: {DateTime.Now:s} Start Reporting");
            //await GetImportedOktaUsers();
            
            Trace.Flush();
        }

        private async Task GetImportedOktaUsers()
        {
            await GetOktaUsersAsync();
            ImportedOktaUsers = OktaUsers.Where(u => PublicIDs.Any(p => p == u.Profile["Public_ID"]?.ToString())).ToList();
            var importedPublicIDs = PublicIDs.Where(p => ImportedOktaUsers.Any(i => p == i.Profile["Public_ID"]?.ToString())).ToList();
            var notImportedPublicIDs = PublicIDs.Except(importedPublicIDs).ToList();
            NotImportedContactIDs = notImportedPublicIDs.Select(n => Obfuscation.TryReverseObfuscatePublicId(n, Form1.ChosenOktagroup)).ToList();
            
        }

        private async Task CreateUsers()
        {
            var isDelay = GroupedContactIDs.Count > 1;
            foreach (var contactIDs in GroupedContactIDs)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var tasks = contactIDs.Select(c => Task.Run(() => CreateUser(c)));
                await Task.WhenAll(tasks.ToArray());                
                stopwatch.Stop();
                var elapsed = stopwatch.ElapsedMilliseconds;
                var timeRemaining = timeAlloted - elapsed;
                if(isDelay && timeRemaining > 0)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(timeRemaining));
                }
                Trace.TraceInformation($@"batch elapsed: {elapsed}");
                Trace.Flush();                
            }
        }

        private async Task CreateUser(string contactId)
        {
            
            try
            {
                await SS.WaitAsync();                
                await CreateOktaProfile(contactId);
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                SS.Release();
            }
        }

        private static async Task CreateOktaProfile(string contactId)
        {
            if (ChosenImportMethod == ImportMethod.Boomi)
            {
                var oktaId = await oktaController.CreateOktaProfile(contactId, ChosenEnvironment.Boomi);                
            }
            else if (ChosenImportMethod == ImportMethod.Code)
            {
                var contactAccount = gwpcController.GetContactAccount(contactId, ChosenEnvironment);
                if (contactAccount != null)
                {
                    if (contactAccount.AccountContacts.FirstOrDefault(c => c.ContactID == contactId) != null)
                    {
                        var oktaId = await oktaController.CreateOktaProfile(contactAccount, ChosenEnvironment.Boomi, ChosenSecondaryEmail);
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
        }

        private async Task DeleteUsers()
        {
            var isDelay = GroupedContactIDs.Count > 1;
            foreach (var contactIDs in GroupedContactIDsToDelete)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var tasks = contactIDs.Select(c => Task.Run(() => DeleteUser(c)));
                await Task.WhenAll(tasks.ToArray());
                stopwatch.Stop();
                var timeRemaining = timeAlloted - stopwatch.ElapsedMilliseconds;
                if (isDelay && timeRemaining > 0)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(timeRemaining));
                }
            }
        }

        private async Task DeleteUser(string contactId)
        {
            try
            {
                await SS.WaitAsync();
                await oktaController.DeleteUserByContactId(contactId, OktaUsersList.FormImportOktaUsers, secretkey, updateList: false, userFromList: true);
            }
            catch (Exception)
            {
                
            }
            finally
            {
                SS.Release();
            }
            
        }
                
        private async void buttonReset_Click(object sender, EventArgs e)
        {
            buttonReset.Enabled = false;
            textBoxContactIDs.Text = "";
            textBoxContactIDs.Enabled = true;
            textBoxNotImportedContactIDs.Text = "";
            OktaUsers.Clear();
            ImportedOktaUsers.Clear();
            ContactIDs.Clear();
            GroupedContactIDs.Clear();
            NotImportedContactIDs.Clear();
            await PopulateOktaUsersAsync();
        }

        private void comboBoxOktaGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();
        }

        private void radioButtonUsingBoomi_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButtonUsingBoomi.Checked)
            {
                ChosenImportMethod = ImportMethod.Boomi;
            }
            else
            {
                ChosenImportMethod = ImportMethod.Code;
            }
        }

        private void buttonGetContactIDs_Click(object sender, EventArgs e)
        {
            var result = openFileDialogGetContactIDs.ShowDialog();
        }

        private void openFileDialogGetContactIDs_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var filename = openFileDialogGetContactIDs.FileName;
            ContactIDs = FileController.ReadCSVFile(filename);
            textBoxContactIDs.Text = $@"{contactIDsFromFile}{Environment.NewLine}{ContactIDs.Count}";
            textBoxContactIDs.Enabled = false;
        }

        private async void checkBoxDeleteFirst_CheckedChanged(object sender, EventArgs e)
        {
            deleteFirst = checkBoxDeleteFirst.Checked;
            if(deleteFirst)
            {
                if(OktaUsers.Count == 0)
                {
                    buttonImportOktaUsers.Enabled = false;
                    await PopulateOktaUsersAsync();
                }
            }
        }
    }
}
