using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.Linq;
using System.Security.Claims;
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
            var p = ClaimsPrincipal.Current;

            if (p.HasClaim("http://myclaims/hascastle", "true"))
            {
                Console.WriteLine("Lucky you!");
            }
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
