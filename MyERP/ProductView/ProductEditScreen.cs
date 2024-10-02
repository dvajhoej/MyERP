using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    // Define a class ProductEditScreen to edit a product
    public class ProductEditScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Rediger kunde";

        // Private field to store the product
        private Product _product;

        // Constructor to initialize the product
        public ProductEditScreen(Product product)
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
            editor.AddOption("Enhed", "Styk", UnitType.Stk);
            editor.AddOption("Enhed", "Pakke", UnitType.Pakker);
            editor.AddOption("Enhed", "time", UnitType.Time);
            editor.AddOption("Enhed", "Meter", UnitType.Meter);

            // Edit the product using the form
            editor.Edit(_product);

            // Quit the screen
            this.Quit();
        }
    }
}