using OktaTools.PC_DevInt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OktaTools
{
    public class GWPCController
    {
        private static FMGConnectServicePortTypeClient pcWebservice = new FMGConnectServicePortTypeClient("FMGConnectServiceSoap11Port");
        private static string currentTinyUrl = "dev0163:8180";
        internal ContactAccountDetail GetContactAccount(string contactId, EphemeralEnv chosenEnvironment)
        {
            try
            {
                SetWebserviceEndpointAddress(chosenEnvironment);

                var contactAccount = pcWebservice.getContactAccountAssociation(
                    authentication: new authentication { username = Form1.secrets.gw.username, password = Form1.secrets.gw.password },
                    gw_language: null,
                    gw_locale: null,
                    contactId: contactId
                    );

                return contactAccount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        internal void UpdateContact(string contactId, string fmgConnectLogin, EphemeralEnv chosenEnvironment)
        {
            SetWebserviceEndpointAddress(chosenEnvironment);

            try
            {
                pcWebservice.updateContact(
                        authentication: new authentication { username = Form1.secrets.gw.username, password = Form1.secrets.gw.password },
                        gw_language: null,
                        gw_locale: null,
                        c: new ContactSummary { ContactID = contactId, FMGConnectLogin = fmgConnectLogin }
                        );
            }
            catch (Exception)
            {

            }
        }

        private static void SetWebserviceEndpointAddress(EphemeralEnv chosenEnvironment)
        {
            var newEndPointAddress = pcWebservice.Endpoint.Address.ToString().Replace(currentTinyUrl, chosenEnvironment.TinyUrl);
            pcWebservice.Endpoint.Address = new System.ServiceModel.EndpointAddress(newEndPointAddress);
            currentTinyUrl = chosenEnvironment.TinyUrl;
        }
    }
}
