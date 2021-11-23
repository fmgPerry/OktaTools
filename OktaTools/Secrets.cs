using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class Secrets
    {
        public string oktaDomain { get; set; }
        public GW gw { get; set; }
        public OktaApiToken oktaApiToken { get; set; }
        public OktaGroupId oktaGroupId { get; set; }
        public SecretKeyValue secretKeyValue { get; set; }

    }

    public class SecretKeyValue
    {
        public string test { get; set; }
        public string preProd { get; set; }
        public string prod { get; set; }
    }

    public class OktaGroupId
    {
        public string prod { get; set; }
        public string nonProd { get; set; }
    }

    public class OktaApiToken
    {
        public string prod { get; set; }
        public string nonProd { get; set; }
    }

    public class GW
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
