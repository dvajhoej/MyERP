namespace MyERP
{
    public class Customer : Person
    {
        public int CustomerID { get; set; }
        public DateTime? LastPurchaseDate { get; set; }


        public Customer(string firstName, string lastName, Address address, string email, string phone, DateTime lastPurchaseDate)
            : base(firstName, lastName, address, email, phone)
        {


            LastPurchaseDate = lastPurchaseDate;

        }

        public Customer()
        {


        }
        private Address _address = new Address();
        private Person _person = new Person();

        public int PersonID
        {
            get { return _person.PersonID; }
            set { _person.PersonID = value; }
        }
        public int AddressID
        {
            get { return _address.AddressID; }
            set { _address.AddressID = value; }
        }
        public string FullAddress
        {
            get { return Street + " " + HouseNumber + ", " + ZipCode + ", " + City + ", " + Country; }
        }
        public string Fullname
        {
            get { return FirstName + " " + LastName; }
        }

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
            return $"{base.ToString()}, Customer Number: {CustomerID}, Last Purchase: {LastPurchaseDate?.ToShortDateString()}";
        }
    }
}
