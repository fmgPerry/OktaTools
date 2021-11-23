using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class OktaUserProfile
    {
        public string Public_ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string secondEmail { get; set; }
        public string primaryPhone { get; set; }
        public string mobilePhone { get; set; }
        public string Home_Phone { get; set; }
        public List<int> FMG_Account_Numbers { get; set; }
        public string Client_Role { get; set; }
        public List<string> Search_email { get; set; }
        public List<string> Search_homePhone { get; set; }
        public List<string> Search_mobilePhone { get; set; }
        public bool? confirmDetails { get; set; }

        public OktaUserProfile(OktaProfile oktaProfile)
        {
            Public_ID = oktaProfile.Public_ID;
            firstName = oktaProfile.FirstName;
            lastName = oktaProfile.LastName;
            login = oktaProfile.Login;
            email = oktaProfile.Email;
            secondEmail = oktaProfile.SecondEmail;
            primaryPhone = oktaProfile.PrimaryPhone;
            mobilePhone = oktaProfile.MobilePhone;
            Home_Phone = oktaProfile.HomePhone;
            FMG_Account_Numbers = oktaProfile.AccountNumbers;
            Client_Role = oktaProfile.ClientRole;
            Search_email = oktaProfile.SearchEmails;
            Search_homePhone = oktaProfile.SearchHomePhones;
            Search_mobilePhone = oktaProfile.SearchMobilePhones;
        }

    }
}
