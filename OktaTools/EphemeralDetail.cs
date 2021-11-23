using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class EphemeralDetail
    {
        public string ServerName { get; set; }
        public tags Tags { get; set; }
        public class tags
        {
            public string boomienvironment { get; set; }
            public string boomi2gw { get; set; }
        }
    }
}
