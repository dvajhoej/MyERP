using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Address(string street, string houseNumber, string zipCode, string city, string country)
        {
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }
        public Address()
        {
            
        }
        public override string ToString()
        {
            return $"{Street} {HouseNumber}, {ZipCode} {City}, {Country}";
        }

    }
}



