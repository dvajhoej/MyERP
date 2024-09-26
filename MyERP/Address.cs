using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
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

        public string FullAddress
        {
            get
            {
                List<string> addressParts = new List<string>();

                if (!string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(HouseNumber))
                {
                    addressParts.Add($"{Street} {HouseNumber}".Trim());
                }
                else if (!string.IsNullOrWhiteSpace(Street))
                {
                    addressParts.Add(Street.Trim());
                }
                else if (!string.IsNullOrWhiteSpace(HouseNumber))
                {
                    addressParts.Add(HouseNumber.Trim());
                }

                if (!string.IsNullOrWhiteSpace(ZipCode))
                {
                    addressParts.Add(ZipCode.Trim());
                }

                if (!string.IsNullOrWhiteSpace(City))
                {
                    addressParts.Add(City.Trim());
                }

                if (!string.IsNullOrWhiteSpace(Country))
                {
                    addressParts.Add(Country.Trim());
                }

                return addressParts.Count > 0 ? string.Join(", ", addressParts) : string.Empty;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    ClearAddress();
                    return;
                }

                var addressParts = value.Split(',').Select(part => part.Trim()).ToArray();


                switch (addressParts.Length)
                {
                    case 5:
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        ZipCode = addressParts[2];
                        City = addressParts[3];
                        Country = addressParts[4];
                        break;
                    case 4:
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        ZipCode = addressParts[2];
                        City = addressParts[3];
                        break;
                    case 3:
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        City = addressParts[2];
                        break;
                    case 2:
                        Street = addressParts[0];
                        City = addressParts[1];
                        break;
                    default:
                        ClearAddress();
                        break;
                }
            }
        }

        // Method to clear all address fields
        private void ClearAddress()
        {
            Street = string.Empty;
            HouseNumber = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
            Country = string.Empty;
        }



        public Address()
        {
            
        }
     

    }
}



