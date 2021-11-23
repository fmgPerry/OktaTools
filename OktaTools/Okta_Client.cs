using Okta.Sdk;
using Okta.Sdk.Configuration;
using System.Net;

namespace OktaTools
{
    public class Okta_Client
    {
        private static int thirtyMinsInSecs = 1800;

        public static OktaClient Get()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            return new OktaClient(new OktaClientConfiguration
            {
                OktaDomain = $@"{Form1.secrets.oktaDomain}",
                Token = Form1.ChosenOktagroup == OktaGroup.test ? $@"{Form1.secrets.oktaApiToken.nonProd}" : $@"{Form1.secrets.oktaApiToken.prod}",
                RequestTimeout = thirtyMinsInSecs

            });


        }

    }
}
