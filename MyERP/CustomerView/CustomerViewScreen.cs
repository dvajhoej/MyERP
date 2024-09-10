using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerViewScreen : Screen
    {
        public Customer customerDisplay { get; set; }

        public CustomerViewScreen(Customer c)
        {
            customer = c;
        }

        public override string Title { get; set; } = "Kunde Visning";
        public Customer customer { get; private set; }

        protected override void Draw()
        {
            Console.WriteLine($"Fuldenavn : {customer.Fullname}");
            Console.WriteLine($"Address : {customer.FullAddress}");
            Console.WriteLine($"Sidste køb : {customer.LastPurchaseDate.ToShortDateString()}");

        }
    }
}