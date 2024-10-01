using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerViewScreen : Screen
    {
        public Customer customerDisplay { get; set; }

        public CustomerViewScreen(Customer c)
        {
            customer = c;
            ExitOnEscape();
        }

        public override string Title { get; set; } = "Kunde Visning";
        public Customer customer { get; private set; }

        protected override void Draw()
        {
            string lastPurchaseDateDisplay;

            if (customer.LastPurchaseDate == new DateTime(1900, 1, 1))
            {
                lastPurchaseDateDisplay = "";
            }
            else
            {
                lastPurchaseDateDisplay = customer.LastPurchaseDate?.ToShortDateString();
            }

            int space = 54;
            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-53} │", "Tryk Esc for at forlade siden");
            WindowHelper.Spacer('└', '─', space, '┘');

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Kunde ID", customer.CustomerID);
            WindowHelper.Spacer('└', '─', space, '┘');

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(customer.FullName, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Address", WindowHelper.Truncate((customer.Street + customer.HouseNumber), 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Postnummer", customer.ZipCode);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "By", WindowHelper.Truncate(customer.City, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Land", WindowHelper.Truncate(customer.Country, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Sidste køb", lastPurchaseDateDisplay);
            WindowHelper.Spacer('└', '─', space, '┘');

          
        }

    }
}