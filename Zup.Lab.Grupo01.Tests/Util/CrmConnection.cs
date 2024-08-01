using Microsoft.Xrm.Tooling.Connector;
using System.Web.Configuration;

namespace Zup.Lab.Grupo01.Tests.Util {
    public class CrmConnection {
        protected CrmServiceClient service { get; set; }

        public CrmConnection()
        {
            string Url = WebConfigurationManager.AppSettings["Url"];
            string AppId = WebConfigurationManager.AppSettings["AppId"];
            string ClientSecret = WebConfigurationManager.AppSettings["ClientSecret"];
            service = new CrmServiceClient($"AuthType=ClientSecret;url={Url};ClientId={AppId};ClientSecret={ClientSecret}");
        }

    }
}
