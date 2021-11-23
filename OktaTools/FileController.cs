using Newtonsoft.Json;
using Okta.Sdk;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OktaTools
{
    class FileController
    {
        private static string csvFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\";
        private static string logFilePath = @"C:\Logs\OktaTool.txt";

        internal void CreateOktaUsersDumpFile(List<IUser> oktaUsers, CsvFilename csvFilename)
        {
            // No need to create file if list is empty
            if (oktaUsers.Count == 0) return;

            var oktaProfiles = new List<OktaProfile>();
            var notProcessedUsers = new List<IUser>();

            foreach (var user in oktaUsers)
            {
                try
                {
                    oktaProfiles.Add(new OktaProfile(user));
                }
                catch (Exception ex)
                {
                    notProcessedUsers.Add(user);
                }
            }

            oktaProfiles = oktaProfiles.OrderBy(op => op.Login).ToList();

            var csvProfiles = new StringBuilder();
            // first row as header
            csvProfiles.AppendLine(@"ContactID,FirstName,LastName,Login,Email,SecondEmail,PrimaryPhone,MobilePhone,HomePhone,AccountNumbers,AssociatedAccounts,LastUpdated,ClientRole,Status,SearchEmails,SearchHomePhones,SearchMobilePhones");

            foreach (var profile in oktaProfiles)
            {
                var lastUpdated = "";
                var associatedAccounts = new List<int>();

                if (!string.IsNullOrEmpty(profile.ClientRole))
                {
                    var jsonClientRole = JsonConvert.DeserializeObject<ClientRole>(profile.ClientRole);

                    lastUpdated = jsonClientRole?.LastUpdated;
                    associatedAccounts = new List<int>();

                    foreach (var accountRole in jsonClientRole?.AccountRoles)
                    {
                        int.TryParse(accountRole.AccountNumber, out int accountNumber);
                        associatedAccounts.Add(accountNumber);
                    }

                    associatedAccounts.RemoveAll(aa => profile.AccountNumbers.Any(an => aa == an));
                }
                var rowEntry = GetRowEntryForOktaUsersDumpFile(profile, associatedAccounts, lastUpdated);
                csvProfiles.AppendLine(rowEntry);
            }

            var csvFullPath = GetCSVFullPath(csvFilename);
            File.AppendAllText(csvFullPath, csvProfiles.ToString());

            var result = MessageBox.Show("Open CSV File?", "CSV file created", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                Process.Start(csvFullPath);
            }

        }

        internal bool CanDeleteLogFile()
        {
            try
            {
                //delete file if exists
                if (File.Exists(logFilePath))
                {
                    //File.Delete(logFilePath);
                    File.WriteAllText(logFilePath, string.Empty);                    
                }
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($@"Please close {Environment.NewLine}{logFilePath}{Environment.NewLine} before continuing. {ex.Message}");
#else
                MessageBox.Show($@"Please close {Environment.NewLine}{logFilePath}{Environment.NewLine} before continuing.");
#endif
                return false;
            }
        }
        internal bool CanDeleteCSVFile(CsvFilename csvFilename)
        {
            var csvFullPath = GetCSVFullPath(csvFilename);
            try
            {
                //delete file if exists
                if (File.Exists(csvFullPath))
                {
                    File.Delete(csvFullPath);
                }
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                MessageBox.Show($@"Please close {Environment.NewLine}{csvFullPath}{Environment.NewLine} before continuing. {ex.Message}");
#else
                MessageBox.Show($@"Please close {Environment.NewLine}{csvFullPath}{Environment.NewLine} before continuing.");
#endif
                return false;
            }
        }

        private string GetCSVFullPath(CsvFilename csvFilename)
        {
            return  $@"{csvFilePath}{Form1.ChosenOktagroup}{csvFilename}.csv";
        }

        private static string GetRowEntryForOktaUsersDumpFile(OktaProfile profile, List<int> associatedAccounts, string lastUpdated)
        {
            var columns = new StringBuilder();

            columns.Append($@"""{profile.ContactID}""");
            columns.Append($@",""{profile.FirstName}""");
            columns.Append($@",""{profile.LastName}""");
            columns.Append($@",""{profile.Login}""");
            columns.Append($@",""{profile.Email}""");
            columns.Append($@",""{profile.SecondEmail}""");
            columns.Append($@",""{profile.PrimaryPhone}""");
            columns.Append($@",""{profile.MobilePhone}""");
            columns.Append($@",""{profile.HomePhone}""");
            columns.Append($@",""{string.Join(", ", profile.AccountNumbers)}""");
            columns.Append($@",""{string.Join(", ", associatedAccounts)}""");
            columns.Append($@",""{lastUpdated}""");
            columns.Append($@",""{Regex.Replace(profile.ClientRole, @"\t|\n|\r", "").Replace("\"", "").Replace(",", ", ")}""");
            columns.Append($@",""{profile.Status}""");
            columns.Append($@",""{string.Join(", ", profile.SearchEmails)}""");
            columns.Append($@",""{string.Join(", ", profile.SearchHomePhones)}""");
            columns.Append($@",""{string.Join(", ", profile.SearchMobilePhones)}""");

            return columns.ToString();
        }


        internal static List<string> ReadCSVFile(string filename)
        {
            var lines = new List<string>();
            var lineCount = 0;
            using (var fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    ++lineCount;
                    lines.Add(line);
                }
            }
            return lines;
        }

    }
}
