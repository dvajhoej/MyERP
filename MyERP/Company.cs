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
        public int ID { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Currency Currency { get; set; }

        public Company(int id, string companyName, string street, int houseNumber, int postalCode, string city, string country, Currency currency)
        {
            ID = id;
            CompanyName = companyName;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
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