using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    public partial class FormOktaProfileSwitcher : Form
    {
        private static List<EphemeralEnv> EphemEnvs = new List<EphemeralEnv>();
        private static EphemeralEnv ChosenEnvironment;
        private static Secretkey secretkey;
        private static OktaController oktaController = new OktaController();
        private static GWPCController gwpcController = new GWPCController();

        public FormOktaProfileSwitcher()
        {
            InitializeComponent();
        }

        private async void FormOktaProfileSwitcher_Load(object sender, EventArgs e)
        {
            comboBoxEnvironments.Enabled = false;
            buttonSetOktaProfileSwitcher.Enabled = false;
            buttonReset.Enabled = false;
            
            await PopulateEphemEnvs();
            ChosenEnvironment = null;
            PopulateComboBoxEnvironments();
            textBoxUsername.Text = "@fmg.co.nz";
            labelStatus.Text = @"Status: Ready";

        }

        private async Task PopulateEphemEnvs()
        {
            var ephemeralController = new EphemeralController();
            EphemEnvs.Clear();
            EphemEnvs = await ephemeralController.GetEnvironments();
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
            buttonSetOktaProfileSwitcher.Enabled = true;
        }

        private async void buttonSetOktaProfileSwitcher_Click(object sender, EventArgs e)
        {
            if (ChosenEnvironment == null)
            {
                MessageBox.Show("Please choose an environment", "No Environment chosen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            labelStatus.Text = @"Status: Switching Profile";
            await SetOktaProfileSwitcher();
            labelStatus.Text = @"Status: Done";
        }

        private async Task SetOktaProfileSwitcher()
        {
            var username = textBoxUsername.Text;
            var contactId = textBoxContactId.Text;
            var firstname = textBoxFirstname.Text;
            var lastname = textBoxLastname.Text;
            var primaryEmail = textBoxPrimaryEmail.Text;
            var secondaryEmail = textBoxSecondaryEmail.Text;

            var contactAccount = gwpcController.GetContactAccount(contactId, ChosenEnvironment);
            if (contactAccount != null)
            {
                if (contactAccount.AccountContacts.FirstOrDefault(c => c.ContactID == contactId) != null)
                {
                    var oktaId = await oktaController.SetOktaProfileSwitcher(contactAccount, ChosenEnvironment.Boomi, username, firstname, lastname, primaryEmail , secondaryEmail);
                    if (oktaId != null)
                    {
                        gwpcController.UpdateContact(contactId, oktaId, ChosenEnvironment);
                    }
                }
            }
        }
    }
}
