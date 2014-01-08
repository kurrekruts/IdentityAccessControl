using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericPrincipalDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // set-up Thread.CurrentPrincipal
            SetPrincipal();
            
            // use principal
            UsePrincipal();
        }

        private static void SetPrincipal()
        {
            var roles = new string[] { "Sales", "Marketing" };
            var id = new GenericIdentity("bob");
            var principal = new GenericPrincipal(id, roles);

            Thread.CurrentPrincipal = principal;
        }

        private static void UsePrincipal()
        {
            // IsInRole way
            if (Thread.CurrentPrincipal.IsInRole("Sales"))
            {
                // do business logic
            }

            // imperative PrincipalPermission
            try
            {
                new PrincipalPermission(null, "Management").Demand();
            }
            catch (SecurityException)
            {
                Console.WriteLine("Not in the Management role");
            }

            // declarative PrincipalPermission
            AddUser();
        }

        // will fail
        [PrincipalPermission(SecurityAction.Demand, Role = "Management")]
        private static void AddUser()
        {
            Console.WriteLine("OK");
        }

    }
}
