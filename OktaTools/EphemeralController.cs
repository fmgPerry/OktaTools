using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    class EphemeralController
    {
        internal async Task<List<EphemeralEnv>> GetEnvironments()
        {
            var environments = new List<EphemeralEnv>();
            var client = new HttpClient();

            // add non-Ephemeral Environments            
            // prod
            environments.Add(new EphemeralEnv { OktaUserGroup = OktaGroup.prod, Name = "prod".PadRight(10) + "gwprod", TinyUrl = "gwprod", Boomi = "prod" });
            
            // test
            environments.Add(new EphemeralEnv { OktaUserGroup = OktaGroup.test, Name = "preprod".PadRight(10) + "dev0005", TinyUrl = "dev0005", Boomi = "preprod" });
            environments.Add(new EphemeralEnv { OktaUserGroup = OktaGroup.test, Name = "devint".PadRight(10) + "dev0163:8180", TinyUrl = "dev0163:8180", Boomi = "devint" });
            environments.Add(new EphemeralEnv { OktaUserGroup = OktaGroup.test, Name = "testc".PadRight(10) + "dev0026:8180", TinyUrl = "dev0026:8180", Boomi = "testc" });


            //scan all 20 ephemeral environments
            for (int counter = 1; counter <= 20; counter++)
            {
                var ephNumber = counter.ToString().PadLeft(2, '0');
                var url = $@"http://dev0085:7000/v2/servers/EPH00{ephNumber}";
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var ephEnvDetail = JsonConvert.DeserializeObject<EphemeralDetail>(responseString);

                    if (ephEnvDetail.Tags.boomienvironment != null)
                    {
                        var fullBoomiEnvironment = ephEnvDetail.Tags.boomienvironment.ToLower();
                        var indexDot = ephEnvDetail.Tags.boomienvironment.IndexOf(".");
                        var boomi = fullBoomiEnvironment.Substring(0, indexDot);
                        var tinyUrl = $@"{ephEnvDetail.ServerName.ToLower()}:8180";
                        var name = $@"{boomi.PadRight(10)}{tinyUrl}";
                        environments.Add(new EphemeralEnv { OktaUserGroup = OktaGroup.test, Name = name, TinyUrl = tinyUrl, Boomi = boomi });
                    }
                }
            }
            return environments;
        }
    }
}
