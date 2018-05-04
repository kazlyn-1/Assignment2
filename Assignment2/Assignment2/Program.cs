using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FullContactCSharp;
using Nito.AsyncEx;

namespace Assignment2
{
    class Program
    {
        private static FullContactClient _api;

        private static async Task AsyncMain()
        {
            string apiKey = "VY8Vi17Gx5K8U2h7k0dEK0eAHDa61LTm";
            _api = new FullContactClient(apiKey);

            while (true)
            {
                Console.Write("Press 'Q' to exit or enter email address:");
                string cmd = Console.ReadLine();

                if (!string.IsNullOrEmpty(cmd) && cmd[0] == 'q')
                    break;

                if (IsValidEmail(cmd))
                {
                    FullContactPerson person = await _api.LookupPersonByEmailAsync(cmd);
                    if (person != null)
                        Console.WriteLine(person);
                    else
                    {
                        Console.WriteLine("Error or nothing found when looking up email: {0}", cmd);
                    }
                }
            }
        }

        private static bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        static void Main(string[] args)
        {
            AsyncContext.Run(AsyncMain);
        }
    }
}
