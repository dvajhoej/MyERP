using Google.Protobuf.WellKnownTypes;
using System.Globalization;

namespace MyERP
{
    // Define a class Customer to represent a customer
    public class Customer : Person
    {
        // Public properties to store the customer details
        public int CustomerID { get; set; }
        public DateTime? LastPurchaseDate { get; set; }

        // Constructor to initialize the customer details
        public Customer(string firstName, string lastName, string email, string phone, DateTime lastPurchaseDate)
            : base(firstName, lastName, email, phone)
        {
            // Set the last purchase date
            LastPurchaseDate = lastPurchaseDate;
        }

        // Default constructor
        public Customer()
        {
            
        }

        // Override the ToString method to provide a string representation of the customer
        public override string ToString()
        {
            string lastPurchase = LastPurchaseDate.HasValue
        ? LastPurchaseDate.Value.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
        : "";
            // Return a string that includes the customer's details and the last purchase date
            return $"{base.ToString()}, Customer Number: {CustomerID}, Last Purchase: {lastPurchase}";
        }

    }
}