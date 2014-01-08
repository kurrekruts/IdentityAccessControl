using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsAuthorizationDemo
{
    class AuthorizationManager : ClaimsAuthorizationManager
    {
        public override bool CheckAccess(AuthorizationContext context)
        {
            var resource = context.Resource.First().Value;
            var action = context.Action.First().Value;

            if (resource == "castle" && action == "show")
            {
                return context.Principal.HasClaim("http://myclaims/hascastle", "true");
            }

            return false;
        }
    }
}
