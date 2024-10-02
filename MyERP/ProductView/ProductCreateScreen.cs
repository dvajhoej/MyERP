using TECHCOOL.UI;

namespace MyERP.productView
{
    // Define a class ProductCreateScreen to create a product
    public class ProductCreateScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Opret produkt";

        // Private field to store the product
        private Product _product;

        // Constructor to initialize the product
        public ProductCreateScreen(Product product)
        {
            _product = product;
        }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Create a new Form object to edit the product
            Form<Product> editor = new Form<Product>();

            // Calculate the number of spaces for the window border
            int spacer = 35;

            // Draw the top border of the window
            WindowHelper.Top(spacer);

            // Display a message to the user
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at gemme");

            // Draw the bottom border of the window
            WindowHelper.Bot(spacer);

            // Add fields to the form to edit the product
            editor.IntBox("Varenummer", "ProductNumber");
            editor.TextBox("Navn", "Name");
            editor.TextBox("Beskrivelse", "Description");
            editor.DoubleBox("Salgsspris", "SellingPrice");
            editor.DoubleBox("Indkøbspris", "PurchasePrice");
            editor.TextBox("Lokation", "Location");
            editor.DoubleBox("Antal på lager", "QuantityInStock");
            editor.SelectBox("Enhed", "Unit");

            // Add options to the select box for the unit
            editor.AddOption("Enhed", "Stk", UnitType.Stk);
            editor.AddOption("Enhed", "Pakker", UnitType.Pakker);
            editor.AddOption("Enhed", "Time", UnitType.Time);
            editor.AddOption("Enhed", "Meter", UnitType.Meter);

            // Edit the product using the form
            editor.Edit(_product);

            // Quit the screen
            this.Quit();
        }
    }
}