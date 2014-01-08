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

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CallService();
        }

        private static void CallService()
        {
            var binding = new WS2007HttpBinding(SecurityMode.TransportWithMessageCredential);
            binding.Security.Message.EstablishSecurityContext = false;

            var factory = new ChannelFactory<IClaimsService>(
                binding,
                new EndpointAddress("https://adfs.leastprivilege.vm/claimsapp/service.svc"));

            factory.CreateChannel().GetClaims().ToList().ForEach(c => Console.WriteLine(c.Value));
        }
    }
}
