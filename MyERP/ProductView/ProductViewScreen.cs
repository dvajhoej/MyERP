using MyERP.CustomerView;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    // Define a class ProductViewScreen to display a product
    public class ProductViewScreen : Screen
    {
        // Public property to store the product to display
        public Product ProductDisplay { get; set; }

        // Constructor to initialize the product to display
        public ProductViewScreen(Product p)
        {
            product = p;
            ExitOnEscape();
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Produkt Visning";

        // Private field to store the product to display
        public Product product { get; private set; }

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

            // Draw the top border of the product details section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the product details
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Varenummer", product.ProductNumber);
            WindowHelper.Spacer('│', '─', space, '│');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(product.Name, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Beskrivelse", WindowHelper.Truncate(product.Description, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Salgspris", product.SellingPrice);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Indkøbspris", product.PurchasePrice);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Lokation", product.Location);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Antal på lager", product.QuantityInStock);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Enhed", product.Unit);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Avance (%)", product.MarginPercentage);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Avance (Kr)", product.Profit);

            // Draw the bottom border of the product details section
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}