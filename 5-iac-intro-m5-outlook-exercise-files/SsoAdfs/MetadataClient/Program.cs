using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetadataClient.ServiceReference;

namespace MetadataClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var proxy = new ClaimsServiceContractClient();
            proxy.GetClaims().ToList().ForEach(c=> Console.WriteLine(c.Value));
        }
    }
}
