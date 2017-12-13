using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace console_csharp_trustframeworkpolicy
{
    internal class Constants
    {
        // update with your app_client_id and tenant_name
        public const string ClientIdForUserAuthn = "ENTER_YOUR_CLIENT_ID";
        public const string Tenant = "ENTER_YOUR_TENANT_NAME";

        // leave these as-is
        public const string AuthorityUri = "https://login.microsoftonline.com/" + Tenant + "/oauth2/v2.0/token";
        public const string RedirectUriForAppAuthn = "https://login.microsoftonline.com";
    }
}
