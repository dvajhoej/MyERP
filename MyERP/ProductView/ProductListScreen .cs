using MyERP.productView;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    // Define a class ProductListScreen to display a list of products
    public class ProductListScreen : Screen
    {
        // Private field to store the list page
        private ListPage<Product> listPage;

        // Constructor to initialize the list page
        public ProductListScreen()
        {
            // Create a new ListPage object to display the products
            listPage = new ListPage<Product>(Database.Instance.Products);

            // Add keys to the list page to perform actions
            listPage.AddKey(ConsoleKey.F1, CreateProduct);
            listPage.AddKey(ConsoleKey.F2, EditProduct);
            listPage.AddKey(ConsoleKey.F5, DeleteProduct);
            listPage.AddKey(ConsoleKey.Escape, Quit);

            // Add columns to the list page to display the product details
            listPage.AddColumn("Varenummer", "ProductNumber");
            listPage.AddColumn("Navn", "Name");
            listPage.AddColumn("Antal På Lager", "QuantityInStock");
            listPage.AddColumn("Enhed", "Unit");
            listPage.AddColumn("Indkøbspris", "PurchasePrice");
            listPage.AddColumn("Salgspris", "SellingPrice");
            listPage.AddColumn("Avance (%)", "MarginPercentage");
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Produkter";

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Calculate the number of spaces for the window border
            int spaces = 35;

            // Draw the top border of the window
            WindowHelper.Top(spaces);

            // Display messages to the user
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette  en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette   en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Bot(spaces);

            // Add some blank lines to the screen
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }

            // Select a product from the list page
            var selected = listPage.Select();

            // If a product is selected, display the product view screen
            if (selected != null)
            {
                Screen.Display(new ProductViewScreen(selected));
            }
            else { }
        }

        // Method to quit the screen
        void Quit(Product _)
        {
            Quit();
        }

        // Method to create a new product
        private void CreateProduct(Product product)
        {
            // Create a new Product object
            var newProduct = new Product();

            // Display the product create screen
            Screen.Display(new ProductCreateScreen(newProduct));

            try
            {
                // Insert the new product into the database
                Database.Instance.InsertProduct(newProduct);

                // Display a message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{newProduct.Name} oprettet");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // Display an error message to the user
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under oprettelse af produkt: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }

        // Method to edit a product
        private void EditProduct(Product selected)
        {
            // Display the product edit screen
            Screen.Display(new ProductEditScreen(selected));

            try
            {
                // Update the product in the database
                Database.Instance.UpdateProduct(selected);

                // Display a message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{selected.Name} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // Display an error message to the user
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under redigering af produkt: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }

        // Method to delete a product
        private void DeleteProduct(Product selected)
        {
            // Check if a product is selected
            if (selected != null)
            {
                try
                {
                    // Delete the product from the database
                    Database.Instance.DeleteProductById(selected.ProductID);

                    // Display a message to the user
                    int spaces = 40;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-40}│", $"{selected.Name} slettet");
                    Console.WriteLine("│{0,-40}│", $"Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    // Display an error message to the user
                    int spaces = 120;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af produkt:{WindowHelper.Truncate(ex.Message, 70)}");
                    Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
            }
            else
            {
                // Display a message to the user if no product is selected
                int spaces = 60;
                Console.SetCursorPosition(0, 7);
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-60}│", "Ingen produkt valgt");
                Console.WriteLine("│{0,-60}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }
    }
}