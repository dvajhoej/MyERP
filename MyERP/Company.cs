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
    // Define a class Company to represent a company
    public class Company : Address
    {
        // Public properties to store the company details
        public int CompanyID { get; set; }
        public string CompanyName { get; set; }
        public Currency Currency { get; set; }

        // Constructor to initialize the company details
        public Company(string companyName, Currency currency)
        {
            // Set the company name
            CompanyName = companyName;

            // Set the currency
            Currency = currency;
        }

        // Default constructor
        public Company()
        {
            
        }
    }
}