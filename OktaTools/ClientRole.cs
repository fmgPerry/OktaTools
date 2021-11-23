using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class ClientRole
    {
        public string LastUpdated { get; set; }
        public List<AccountRole> AccountRoles { get; set; }
    }
}
