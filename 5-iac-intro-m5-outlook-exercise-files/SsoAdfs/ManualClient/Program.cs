using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Identity45;
using Thinktecture.IdentityModel45;


namespace ManualClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var token = RequestToken();
            CallService(token);
        }

        private static SecurityToken RequestToken()
        {
            var factory = new WSTrustChannelFactory(
                new WindowsWSTrustBinding(SecurityMode.Transport),
                new EndpointAddress("https://adfs.leastprivilege.vm/adfs/services/trust/13/windowstransport"));
            factory.TrustVersion = TrustVersion.WSTrust13;

            var channel = factory.CreateChannel();

            var rst = new RequestSecurityToken
            {
                RequestType = RequestTypes.Issue,
                KeyType = KeyTypes.Bearer,
                AppliesTo = new EndpointReference("https://adfs.leastprivilege.vm/adfsapp/")
            };

            return channel.Issue(rst);
        }

        private static void CallService(SecurityToken token)
        {
            var binding = new WS2007FederationHttpBinding(WSFederationHttpSecurityMode.TransportWithMessageCredential);
            binding.Security.Message.EstablishSecurityContext = false;
            binding.Security.Message.IssuedKeyType = SecurityKeyType.BearerKey;

            var factory = new ChannelFactory<IClaimsService>(
                binding,
                new EndpointAddress("https://adfs.leastprivilege.vm/adfsapp/service.svc"));
            factory.Credentials.SupportInteractive = false;

            var channel = factory.CreateChannelWithIssuedToken(token);
            channel.GetClaims().ToList().ForEach(c => Console.WriteLine(c.Value));
        }
    }
}
