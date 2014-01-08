using System;
using System.Collections.Generic;
using System.Linq;
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
            var principal = Thread.CurrentPrincipal;

            Console.WriteLine(principal.Identity.Name);
            Console.WriteLine(principal.IsInRole("Sales"));
        }

    }
}
