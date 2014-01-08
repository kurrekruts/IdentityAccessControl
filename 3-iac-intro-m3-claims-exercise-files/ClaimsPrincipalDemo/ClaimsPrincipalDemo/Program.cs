using System;
using System.Collections.Generic;
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
            
            // old code
            UsePrincipalLegacyCode();
            
            // claims-aware code
            UsePrincipalNewCode();
        }

        private static void SetPrincipal()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "bob"),
                new Claim(ClaimTypes.Email, "bob@thinktecture.com"),
                new Claim(ClaimTypes.Role, "Geek"),
                new Claim("http://myclaims/location", "Heidelberg")
            };

            var id = new ClaimsIdentity(claims, "Custom", ClaimTypes.Name, ClaimTypes.Role);
            var principal = new ClaimsPrincipal(id);

            Thread.CurrentPrincipal = principal;
        }

        private static void UsePrincipalNewCode()
        {
            var p = ClaimsPrincipal.Current;

            Console.WriteLine(p.FindFirst(ClaimTypes.Email).Value);
        }

        private static void UsePrincipalLegacyCode()
        {
            Console.WriteLine(Thread.CurrentPrincipal.Identity.Name);
            Console.WriteLine(Thread.CurrentPrincipal.IsInRole("Geek"));
        }
    }
}
