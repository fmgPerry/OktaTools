using System.Collections.Generic;

namespace OktaTools
{
    public class AccountRole
    {
        public string AccountNumber { get; set; }
        public string Auth { get; set; }
        public string Primary { get; set; }
        public List<string> PolicyNumbers { get; set; }
    }
}
