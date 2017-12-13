using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace console_csharp_trustframeworkpolicy
{
    class Program
    {
        static void Main(string[] args)
        {
            // record start DateTime of execution
            string currentDateTime = DateTime.Now.ToUniversalTime().ToString();

            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            Console.WriteLine("Run custom trust framework operations for signed-in admin\n");
            UserMode.UserModeRequests();

            Console.WriteLine("\nCompleted at {0} \n Press Any Key to Exit.", currentDateTime);
            Console.ReadKey();
        }
    }
}
