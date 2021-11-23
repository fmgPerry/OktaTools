using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Okta.Sdk;
using OktaTools.PC_DevInt;

namespace OktaTools
{
    public class OktaProfile
    {
        
        public string ContactID { get; set; }
        public string Public_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string SecondEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
        public List<int> AccountNumbers { get; set; }
        public string ClientRole { get; set; }
        public string Status { get; set; }
        public string PrimaryAccountNumber { get; set; }
        public List<string> SearchEmails { get; set; }
        public List<string> SearchMobilePhones { get; set; }
        public List<string> SearchHomePhones { get; set; }

        private static string fmgapi_undefined = "fmgapi_undefined";
        
        public OktaProfile(IUser user)
        {
            try
            {
                Public_ID = user.Profile["Public_ID"]?.ToString() ?? "";
                ContactID = Obfuscation.TryReverseObfuscatePublicId(Public_ID, Form1.ChosenOktagroup);
                FirstName = user.Profile.FirstName;
                LastName = user.Profile.LastName;
                Login = user.Profile.Login;
                Email = user.Profile.Email;
                SecondEmail = user.Profile.SecondEmail;
                PrimaryPhone = user.Profile["primaryPhone"]?.ToString() ?? "";
                MobilePhone = user.Profile.MobilePhone ?? "";
                HomePhone = user.Profile["Home_Phone"]?.ToString() ?? "";
                AccountNumbers = GetAccountNumbers(user.Profile["FMG_Account_Numbers"]);                
                ClientRole = Obfuscation.Base64.Decode(user.Profile["Client_Role"]?.ToString() ?? "");
                Status = user.Status;
                SearchEmails = GetSearchItems(user.Profile["Search_email"]);
                SearchHomePhones = GetSearchItems(user.Profile["Search_homePhone"]);
                SearchMobilePhones = GetSearchItems(user.Profile["Search_mobilePhone"]);
            }
            catch(Exception)
            {

            }
        }

        
        public OktaProfile(ContactAccountDetail contactAccount, string envPrefix, string secondEmail, Secretkey secretkey)
        {
            try
            {
                var contact = contactAccount.AccountContacts.FirstOrDefault(c => c.ContactID == contactAccount.ContactID);
                var hasPhoneNumbers = contact.PhoneNumbers.FirstOrDefault(p => p.PhoneID != fmgapi_undefined) != null;
                var primaryPhone = contact.PhoneNumbers.FirstOrDefault(p => p.Primary);
                var primaryPhoneNull = primaryPhone == null;
                var loginPrefix = envPrefix == "prod" ? $@"UNSET." : $@"{envPrefix}.UNSET.";
                
                ContactID = contact.ContactID;
                Public_ID = TryObfuscate(ContactID, secretkey);
                FirstName = contact.Name.FirstName;
                LastName = contact.Name.LastName;
                Login = $@"{loginPrefix}{Public_ID}@fmg.co.nz";
                Email = Login;
                SecondEmail = secondEmail;
                PrimaryPhone = hasPhoneNumbers ? "" : primaryPhoneNull ? "" : primaryPhone?.PhoneNumber ?? "";
                MobilePhone = hasPhoneNumbers ? "" : primaryPhoneNull ? "" : primaryPhone?.PhoneType?.Code == "mobile" ? primaryPhone.PhoneNumber : contact.PhoneNumbers.FirstOrDefault(p => p?.PhoneType?.Code == "mobile")?.PhoneNumber ?? "";
                HomePhone = hasPhoneNumbers ? "" : contact.PhoneNumbers.FirstOrDefault(p => p?.PhoneType?.Code == "home")?.PhoneNumber ?? "";
                AccountNumbers = GetContactAccountNumbers(contactAccount);
                ClientRole = GetClientRole(contactAccount, contact.ContactID);

                SearchEmails = contactAccount.SearchEmails?.ToList();
                SearchHomePhones = contactAccount.SearchHomePhones?.ToList();
                SearchMobilePhones = contactAccount.SearchMobilePhones?.ToList();
            }
            catch (Exception ex)
            {

            }
        }

        private string GetClientRole(ContactAccountDetail contactAccount, string contactID)
        {
            var accountRoles = new List<AccountRole>();
            var accounts = new List<AccountSummary>();
            foreach(var account in contactAccount.Accounts)
            {
                if (account.AccountRoles.Where(r => r.ContactID == contactID).Count() > 0)
                {
                    accounts.Add(account);
                }
            }

            var hasNoPrimary = true;
            foreach(var account in accounts.OrderBy(a => a.AccountNumber))
            {
                // skip if there are no active PolicyNumbers
                if (account.PolicyNumbers.Count() > 0)
                {
                    var accountRole = new AccountRole();
                    accountRole.AccountNumber = account.AccountNumber;                    
                    accountRole.Auth = GetAuth(account.AccountRoles, contactID);
                    accountRole.PolicyNumbers = account.PolicyNumbers.ToList();
                    
                    if(hasNoPrimary && accountRole.Auth != AuthLevels.None.ToString())
                    {
                        accountRole.Primary = (account.AccountHolderID == contactID)? @",""Primary"":""true""" : "";                        
                        hasNoPrimary = string.IsNullOrEmpty(accountRole.Primary);
                        if (!hasNoPrimary)
                        {
                            PrimaryAccountNumber = account.AccountNumber;
                        }                        
                    }

                    accountRoles.Add(accountRole);
                }
            }

            accountRoles = accountRoles.OrderByDescending(r => Enum.Parse(typeof(AuthLevels), r.Auth)).ToList();
            
            var accountRolesList = new List<string>();
            
            foreach (var clientRole in accountRoles)
            {
                var quotedPolicyNumbers = new List<string>();
                foreach (var policyNumber in clientRole.PolicyNumbers)
                {
                    quotedPolicyNumbers.Add($@"""{policyNumber}""");
                }
                var policyNumbers = string.Join(",", quotedPolicyNumbers);
                accountRolesList.Add($@"{{""AccountNumber"":""{clientRole.AccountNumber}"",""Auth"":""{clientRole.Auth}""{clientRole.Primary},""PolicyNumbers"":[{policyNumbers}]}}");
            }

            var accountRolesString = $@"""AccountRoles"":[{string.Join(",", accountRolesList)}]";
            var clientRoleString = $@"{{""LastUpdated"":""{DateTime.Now.ToString("s")}"",{accountRolesString}}}";
            
            return Obfuscation.Base64.Encode(clientRoleString);
            
        }

        private string GetAuth(AccountRoleSummary[] accountRoles, string contactID)
        {
            var roles = new List<AuthLevels>();

            foreach(var accountRole in accountRoles.Where(ar => ar.ContactID == contactID))
            {
                if (accountRole.META_RestrictedSpecified)
                {
                    return AuthLevels.None.ToString();
                }
                else
                {
                    foreach (var role in accountRole.Roles)
                    {

                        roles.Add(MapGwRoleToBoomiRole(role.Code));
                    }
                }
            }
            
            return roles.Max().ToString();
        }

        private AuthLevels MapGwRoleToBoomiRole(string code)
        {
            switch (code)
            {
                case "accountholder":
                case "executor_fmg":
                case "jointaccountholder_fmg":
                case "namedinsured":
                case "powerofattorney_fmg":
                    return AuthLevels.Full;

                case "authoritytoact_fmg":
                case "lawyer_fmg":
                    return AuthLevels.Act;

                case "accountingcontact":
                case "othermigrated_fmg":
                case "additionalinterest":
                case "ownerofficer":
                    return AuthLevels.View;

                case "driver":
                case "employeeemployer_fmg":
                case "familyrelation_fmg":
                case "secondarycontact":
                case "shareholder_fmg":
                case "trustee_fmg":
                default:
                    return AuthLevels.None;

            }
        }

        private List<int> GetContactAccountNumbers(ContactAccountDetail contactAccount)
        {
            var accountNumbers = new List<int>();
            foreach (var account in contactAccount.Accounts)
            {
                int.TryParse(account.AccountNumber, out int accountNumber);
                accountNumbers.Add(accountNumber);
            }
            return accountNumbers;
        }


        private string TryObfuscate(string contactID, Secretkey secretkey)
        {
            try
            {
                return Obfuscation.Obfuscate(contactID, secretkey);
            }
            catch(Exception)
            {
                return contactID;
            }
        }
        
        private List<int> GetAccountNumbers(object FMG_Account_Numbers)
        {
            var accountNumbers = new List<int>();
            if (FMG_Account_Numbers != null)
            {
                foreach (var FMG_Account_Number in FMG_Account_Numbers as IList)
                {
                    int.TryParse(FMG_Account_Number.ToString(), out int accountNumber);
                    accountNumbers.Add(accountNumber);
                }
            }
            return accountNumbers;
        }
        private List<string> GetSearchItems(object Search_items)
        {
            var searchItems = new List<string>();
            if(Search_items != null)
            {
                foreach (var Search_item in Search_items as IList)
                {
                    searchItems.Add(Search_item.ToString());
                }
            }
            return searchItems;
        }

    }
}
