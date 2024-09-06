using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Customer : Person
    {
        public int CustomerNumber { get; set; }
        public DateTime LastPurchaseDate { get; set; }

        public Customer(string firstName, string lastName, Address address, string email, string phone, int customerNumber, DateTime lastPurchaseDate)
            : base(firstName, lastName, address, email, phone)
        {
            CustomerNumber = customerNumber;
            LastPurchaseDate = lastPurchaseDate;
        }

        public Customer()
        {

        }

        public override string ToString()
        {
            return $"{base.ToString()}, Customer Number: {CustomerNumber}, Last Purchase: {LastPurchaseDate.ToShortDateString()}";
        }
    }
}
