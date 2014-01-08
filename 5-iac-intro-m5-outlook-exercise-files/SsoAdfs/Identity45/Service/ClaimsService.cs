using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Claims;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace Identity45
{
    [ServiceContract(Name = "ClaimsServiceContract", Namespace = "urn:tt")]
    public interface IClaimsService
    {
        [OperationContract(Name = "GetClaims", Action = "GetClaims", ReplyAction = "GetClaimsReply")]
        List<ViewClaim> GetClaims();
    }

    public class ViewClaim
    {
        public string ClaimType { get; set; }
        public string Value { get; set; }
        public string Issuer { get; set; }
        public string OriginalIssuer { get; set; }
    }

    [ServiceBehavior(Name = "ClaimsService", Namespace = "urn:tt")]
    public class ClaimsService : IClaimsService
    {
        public List<ViewClaim> GetClaims()
        {
            var id = Thread.CurrentPrincipal.Identity as ClaimsIdentity;
            //var id = OperationContext.Current.ClaimsPrincipal;

            if (id == null)
            {
                throw new SecurityException();
            }

            return new List<ViewClaim>(
                from claim in id.Claims
                select new ViewClaim
                {
                    ClaimType = claim.Type,
                    Value = claim.Value,
                    Issuer = claim.Issuer,
                    OriginalIssuer = claim.OriginalIssuer,
                });
        }
    }
}