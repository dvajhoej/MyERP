using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MyERP
{
    public class Company : Address
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Currency Currency { get; set; }


        public Company(string companyName, Currency currency)
        {
            CompanyName = companyName;
          
            Currency = currency;
        }

        public Company()
        {
            
        }
    }
}
// Fejl på commit navngivning, Dette er # D2