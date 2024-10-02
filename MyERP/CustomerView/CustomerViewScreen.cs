using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    // Define a class CustomerViewScreen that inherits from Screen
    public class CustomerViewScreen : Screen
    {
        // Public property to store the customer to be displayed
        public Customer customerDisplay { get; set; }

        // Constructor that takes a Customer object as a parameter
        public CustomerViewScreen(Customer c)
        {
            // Set the customer property
            customer = c;

            // Exit the screen when the Escape key is pressed
            ExitOnEscape();
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Kunde Visning";

        // Private property to store the customer
        public Customer customer { get; private set; }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Initialize a variable to store the last purchase date display
            string lastPurchaseDateDisplay;

            // Check if the last purchase date is the default date (1900-01-01)
            if (customer.LastPurchaseDate == new DateTime(1900, 1, 1))
            {
                // If it is, set the last purchase date display to an empty string
                lastPurchaseDateDisplay = "";
            }
            else
            {
                // If it's not, set the last purchase date display to the short date string of the last purchase date
                lastPurchaseDateDisplay = customer.LastPurchaseDate?.ToShortDateString();
            }

            // Calculate the number of spaces for the window border
            int space = 54;

            // Draw the top border of the window
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display a message to the user
            Console.WriteLine("│{0,-53} │", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Spacer('└', '─', space, '┘');

            // Draw the top border of the customer information section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the customer ID
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Kunde ID", customer.CustomerID);

            // Draw the bottom border of the customer information section
            WindowHelper.Spacer('└', '─', space, '┘');

            // Draw the top border of the customer details section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the customer details
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(customer.FullName, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Address", WindowHelper.Truncate((customer.Street + customer.HouseNumber), 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Postnummer", customer.ZipCode);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "By", WindowHelper.Truncate(customer.City, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Land", WindowHelper.Truncate(customer.Country, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Sidste køb", lastPurchaseDateDisplay);

            // Draw the bottom border of the customer details section
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}