using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Address
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Address(string street, string houseNumber, string postalCode, string city, string country)
        {
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            Country = country;
        }

        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {PostalCode} {City}, {Country}";
        }

    }
}
