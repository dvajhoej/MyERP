namespace MyERP
{
    public class Customer : Person
    {
        public int CustomerID { get; set; }
        public DateTime? LastPurchaseDate { get; set; }


        public Customer(string firstName, string lastName, string email, string phone, DateTime lastPurchaseDate)
            : base(firstName, lastName, email, phone)
        {
            LastPurchaseDate = lastPurchaseDate;
        }

        public Customer()
        {

        }

        public override string ToString()
        {
            return $"{base.ToString()}, Customer Number: {CustomerID}, Last Purchase: {LastPurchaseDate?.ToShortDateString()}";
        }
    }
}
