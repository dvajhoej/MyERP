using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    // Define a class CompanyViewScreen that inherits from Screen
    public class CompanyViewScreen : Screen
    {
        // Public property to store the company to be displayed
        public Company companyDisplay { get; set; }

        // Constructor that takes a Company object as a parameter
        public CompanyViewScreen(Company c)
        {
            // Set the company property
            company = c;

            // Exit the screen when the Escape key is pressed
            ExitOnEscape();
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Virksomheds Visning";

        // Private property to store the company
        public Company company { get; private set; }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Calculate the number of spaces for the window border
            int space = 54;

            // Draw the top border of the window
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display a message to the user
            Console.WriteLine("│{0,-53} │", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Spacer('└', '─', space, '┘');

            // Draw the top border of the company information section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the company ID
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Virksomheds ID", company.CompanyID);

            // Draw the bottom border of the company information section
            WindowHelper.Spacer('└', '─', space, '┘');

            // Draw the top border of the company details section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the company details
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(company.CompanyName, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Address", WindowHelper.Truncate((company.Street + company.HouseNumber), 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Postnummer", company.ZipCode);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "By", WindowHelper.Truncate(company.City, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Land", WindowHelper.Truncate(company.Country, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Currency", company.Currency);

            // Draw the bottom border of the company details section
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}