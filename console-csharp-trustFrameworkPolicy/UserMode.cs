using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graph;
using System.Net.Http;

namespace console_csharp_trustframeworkpolicy
{
    internal class UserMode
    {
        public static GraphServiceClient client;
        public static void UserModeRequests()
        {
            try
            {
                //*********************************************************************
                // setup Microsoft Graph Client for delegated user.
                //*********************************************************************
                if (Constants.ClientIdForUserAuthn != "ENTER_YOUR_CLIENT_ID")
                {
                    client = AuthenticationHelper.GetAuthenticatedClientForUser();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You haven't configured a value for ClientIdForUserAuthn in Constants.cs. Please follow the Readme instructions for configuring this application.");
                    Console.ResetColor();
                    Console.ReadKey();
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Acquiring a token failed with the following error: {0}", ex.Message);
                if (ex.InnerException != null)
                {
                    //You should implement retry and back-off logic per the guidance given here:http://msdn.microsoft.com/en-us/library/dn168916.aspx
                    //InnerException Message will contain the HTTP error status codes mentioned in the link above
                    Console.WriteLine("Error detail: {0}", ex.InnerException.Message);
                }
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nPlease login as a global admin of the tenant (example: admin@myb2c.onmicrosoft.com");
            Console.WriteLine("\n=============================\n\n");

            try
            {
                User user = client.Me.Request().GetAsync().Result;
                Console.WriteLine("Current user:    Id: {0}  UPN: {1}", user.Id, user.UserPrincipalName);

                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://graph.microsoft.com/testcpimtf/trustFrameworkPolicies");
                AuthenticationHelper.AddHeaders(request);

                Console.WriteLine(request.RequestUri);
                Console.WriteLine(request.Headers);

                Task<HttpResponseMessage> httpRequest = httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                httpRequest.Wait();

                Console.WriteLine("RESPONSE");
                Console.WriteLine(httpRequest.Result.Headers);
                Task<string> taskContentString = httpRequest.Result.Content.ReadAsStringAsync();
                taskContentString.Wait();
                Console.WriteLine(taskContentString.Result);
            }

            catch (Exception e)
            {
                Console.WriteLine("\nError getting /me user {0} {1}",
                     e.Message, e.InnerException != null ? e.InnerException.Message : "");
            }
        }

    }
}
