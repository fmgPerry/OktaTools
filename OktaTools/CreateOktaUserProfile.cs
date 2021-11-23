using Okta.Sdk;
using System.Collections.Generic;

namespace OktaTools
{
    public class CreateOktaUserProfile
    {
        public List<string> groupIds { get; set; }
        public OktaUserProfile profile { get; set; }
        public UserCredentials credentials { get; set; }
        
        public CreateOktaUserProfile(OktaProfile oktaProfile)
        {
            groupIds = new List<string>() { Form1.OktaGroupId };
            profile = new OktaUserProfile(oktaProfile);            
        }

        public CreateOktaUserProfile(OktaProfile oktaProfile, string testPassword) : this(oktaProfile)
        {
            credentials = new UserCredentials(testPassword);            
        }

        public class UserCredentials
        {
            public Password password { get; set; }
            public UserCredentials(string testPassword)
            {
                password = new Password(testPassword);
            }

            public class Password
            {
                public string value { get; set; }
                public Password(string testPassword)
                {
                    value = testPassword;
                }                
            }
        }
    }
}
