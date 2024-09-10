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
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }


        public Company(int companyID, string companyName, string street, string houseNumber, string zipCode, string city, string country, Currency currency)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            City = city;
            Country = country;
            Currency = currency;
        }

        public Company()
        {

        }
    }
}
// Fejl på commit navngivning, Dette er # D2