using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InheritanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var id = new GenericIdentity("bob");
           
        }

    }

    class CorpIdentity : ClaimsIdentity
    {
        public CorpIdentity(string name, string reportsTo, string office)
        {
            AddClaim(new Claim(ClaimTypes.Name, name));
            AddClaim(new Claim("reportsto", reportsTo));
            AddClaim(new Claim("office", office));
        }


        public string ReportsTo 
        { 
            get 
            {
                return FindFirst("reportsto").Value;
            } 
        }
        public string Office { get; set; }

    }
}
