using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class UpdateOktaUserProfile
    {
        public OktaUserProfile profile { get; set; }
        public UpdateOktaUserProfile(OktaProfile oktaProfile)
        {
            profile = new OktaUserProfile(oktaProfile);
        }

    }
}
