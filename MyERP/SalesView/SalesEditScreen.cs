using System;
using System.Collections.Generic;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    // Define a class SalesEditScreen to edit a sales order
    public class SalesEditScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Rediger Order";

        // Private field to store the sales order
        private SalesOrderHeader _salesOrder;

        // Constructor to initialize the sales order
        public SalesEditScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Calculate the number of spaces for the window border
            int spaces = 45;

            // Draw the top border of the window
            WindowHelper.Top(spaces);

            // Display messages to the user
            Console.WriteLine("│{0,-45}│", "Tryk F1 for at ændre status på valgte ordre");
            Console.WriteLine("│{0,-45}│", "Tryk F2 for at ændre varer på ordren");

            // Draw the bottom border of the window
            WindowHelper.Bot(spaces);

            // Get the key pressed by the user
            var key = Console.ReadKey();

            // Check if the user wants to change the status of the order
            if (key.Key == ConsoleKey.F1)
            {
                // Check if the order number is valid
                if (_salesOrder.OrderNumber != 0)
                {
                    // Create a new Form object to edit the order status
                    Form<SalesOrderHeader> editor = new Form<SalesOrderHeader>();

                    // Display the order number
                    Console.WriteLine($"Ordre nummer:       {_salesOrder.OrderNumber}");

                    // Add a select box to the form to edit the order status
                    editor.SelectBox("Order Status", "Status", GetStatusOptions());

                    // Edit the order status using the form
                    editor.Edit(_salesOrder);

                    // Check if the order status is set to "Færdig"
                    if (_salesOrder.Status == SalesOrderHeader.OrderStatus.Færdig)
                    {
                        // Set the completion date of the order
                        _salesOrder.CompletionDate = DateTime.Now;
                    }

                    // Update the order in the database
                    Database.Instance.UpdateSalesOrderHeader(_salesOrder);
                }
                else
                {
                    // Throw an exception if the order number is not valid
                    throw new Exception("Du kan ikke ændre status på denne ordre");
                }
            }
            // Check if the user wants to edit the order lines
            else if (key.Key == ConsoleKey.F2)
            {
                // Edit the order lines
                EditOrderLines();
            }

            // Quit the screen
            this.Quit();
        }

        // Method to get the status options for the order
        private Dictionary<string, object> GetStatusOptions()
        {
            // Create a dictionary to store the status options
            return new Dictionary<string, object>
            {
                { "Bekræftet", (object)SalesOrderHeader.OrderStatus.Bekræftet },
                { "Pakket", (object)SalesOrderHeader.OrderStatus.Pakket },
                { "Færdig", (object)SalesOrderHeader.OrderStatus.Færdig }
            };
        }

        // Method to edit the order lines
        private void EditOrderLines()
        {
            // Iterate through the order lines
            foreach (var orderLine in Database.Instance.SalesOrderLines)
            {
                // Check if the order line belongs to the current order
                if (orderLine.SalesOrderHeadID == _salesOrder.OrderNumber)
                {
                    // Display the order line details
                    Console.WriteLine($"Editing Order Line for: {orderLine.Name} - Current Quantity: {orderLine.Quantity}");

                    // Create a dictionary to store the product options
                    var productOptions = new Dictionary<string, object>();

                    // Iterate through the products
                    foreach (var product in Database.Instance.Products)
                    {
                        // Create a display name for the product
                        string displayName = $"{product.Name} (ID: {product.ProductID})";

                        // Add the product to the product options
                        if (!productOptions.ContainsKey(displayName))
                        {
                            productOptions.Add(displayName, product.ProductID);
                        }
                    }

                    // Create a new Form object to edit the order line
                    Form<SalesOrderLine> orderLineForm = new Form<SalesOrderLine>();

                    // Add a select box to the form to edit the product
                    orderLineForm.SelectBox("Vælg n yt produkt", "ProductID", productOptions);

                    // Add a double box to the form to edit the quantity
                    orderLineForm.DoubleBox("Antal", "Quantity");

                    // Edit the order line using the form
                    bool success = orderLineForm.Edit(orderLine);

                    // Check if the order line was edited successfully
                    if (success)
                    {
                        // Get the selected product ID
                        int selectedProductID = orderLine.ProductID;

                        // Get the selected product
                        var selectedProduct = Database.Instance.GetProductById(selectedProductID);

                        // Check if the selected product is valid
                        if (selectedProduct != null)
                        {
                            // Update the order line details
                            orderLine.ProductID = selectedProductID;
                            orderLine.Name = selectedProduct.Name;
                            orderLine.Price = selectedProduct.SellingPrice;

                            // Display a message to the user
                            Console.WriteLine($"Order line updated to: {orderLine.Quantity} x {orderLine.Name}");
                        }
                        else
                        {
                            // Display an error message if the selected product is not valid
                            Console.WriteLine("Invalid product selection.");
                        }
                    }
                    else
                    {
                        // Display a message to the user if the order line edit was cancelled
                        Console.WriteLine("Order line edit cancelled.");
                    }
                }
            }

            // Display the total amount of the order
            Console.WriteLine($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }
    }
}