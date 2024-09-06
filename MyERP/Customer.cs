using System;

namespace MyERP
{
    public class Customer : Person
    {
        public int CustomerID { get; private set; }
        public DateTime LastPurchaseDate { get; set; }
        static int counter = 80000;

        public Customer(string firstName, string lastName, Address address, string email, string phone, DateTime lastPurchaseDate)
            : base(firstName, lastName, address, email, phone)
        {
            counter++;
            CustomerID = counter;
            LastPurchaseDate = lastPurchaseDate;
      
        }

        public Customer()
        {
            counter++;
            CustomerID = counter;
        }

        private Address _address = new Address();
        private Address _houseNumber = new Address();
        private Address _zipCode = new Address();
        private Address _Country = new Address();

        public string City
        {
            get { return _address.City; }
            set { _address.City = value; }
        }
        public string Country
        {
            get { return _address.Country; }
            set { _address.Country = value; }
        }
        public string ZipCode
        {
            get { return _address.ZipCode; }
            set { _address.ZipCode = value; }
        }
        public string Street
        {
            get { return _address.Street; }  
            set { _address.Street = value; }
        }

        public string HouseNumber
        {
            get { return _address.HouseNumber; }
            set { _address.HouseNumber = value; }
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Customer Number: {CustomerID}, Last Purchase: {LastPurchaseDate.ToShortDateString()}";
        }
    }
}
