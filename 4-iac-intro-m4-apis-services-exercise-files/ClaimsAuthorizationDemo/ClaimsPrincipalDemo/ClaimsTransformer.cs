using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsAuthorizationDemo
{
    class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            var name = incomingPrincipal.Identity.Name;

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new SecurityException("No user name found");
            }

            var location = incomingPrincipal.FindFirst("http://myclaims/location").Value;

            if (string.IsNullOrWhiteSpace(location))
            {
                throw new SecurityException("No location found");
            }

            return CreateUserPrincipal(name, location);
        }

        private ClaimsPrincipal CreateUserPrincipal(string name, string location)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, "Users")
            };

            if (location.Equals("Heidelberg"))
            {
                claims.Add(new Claim("http://myclaims/hascastle", "true"));
            }

            return new ClaimsPrincipal(new ClaimsIdentity(claims, "Custom"));
        }
    }
}
