using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClaimsPrincipalDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // set-up Thread.CurrentPrincipal
            SetPrincipal();
            
            // claims-aware code
            UsePrincipal();
        }

        private static void UsePrincipal()
        {
            // imperative
            try
            {
                ClaimsPrincipalPermission.CheckAccess("castle", "show");
            }
            catch (SecurityException)
            {
                Console.WriteLine("Not allowed to offer castle tours");
            }

            // declarative
            OfferCastleTour();
        }

        [ClaimsPrincipalPermission(SecurityAction.Demand,
            Resource = "castle",
            Operation = "show")]
        private static void OfferCastleTour()
        {
            Console.WriteLine("wonderful castle!");
        }

        private static void SetPrincipal()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "bob"),
                new Claim("http://myclaims/location", "Heidelberg")
            };

            var id = new ClaimsIdentity(claims, "Custom", ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(id);

            // call out to registered ClaimsAuthenticationManager
            var claimsAuthManager = FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthenticationManager;
            
            // set the transformed principal
            Thread.CurrentPrincipal = claimsAuthManager.Authenticate("demo", principal);
        }
    }
}
