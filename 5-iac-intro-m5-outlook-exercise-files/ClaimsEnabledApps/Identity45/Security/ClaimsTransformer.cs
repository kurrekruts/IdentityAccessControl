using System;
using System.Collections.Generic;
using System.IdentityModel.Services;
using System.IdentityModel.Services.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.ServiceModel;
using System.Web;

namespace Security
{
    public class ClaimsTransformer : ClaimsAuthenticationManager
    {
        public override ClaimsPrincipal Authenticate(string resourceName, ClaimsPrincipal incomingPrincipal)
        {
            if (!incomingPrincipal.Identity.IsAuthenticated)
            {
                return incomingPrincipal;
            }

            var newPrincipal = Transform(incomingPrincipal);
            
            EstablishSession(newPrincipal);

            return newPrincipal;
        }

        ClaimsPrincipal Transform(ClaimsPrincipal incomingPrincipal)
        {
            var nameClaim = incomingPrincipal.Identities.First().FindFirst(ClaimTypes.Name);

            var claims = new List<Claim>
            {
                nameClaim,
                new Claim(ClaimTypes.Email, "foo@thinktecture.com"),
                new Claim("http://claims/time", DateTime.Now.ToLongTimeString())
            };

            var id = new ClaimsIdentity(claims, "Application");
            return new ClaimsPrincipal(id);
        }

        private void EstablishSession(ClaimsPrincipal principal)
        {
            if (HttpContext.Current != null)
            {
                var sessionToken = new SessionSecurityToken(principal);
                FederatedAuthentication.SessionAuthenticationModule.WriteSessionTokenToCookie(sessionToken);
            }
        }
    }
}