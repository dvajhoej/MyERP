using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    // Define a class SalesCreateScreen to create a sales order
    public class SalesCreateScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Opret Ordre";

        // Private field to store the sales order
        private SalesOrderHeader _salesOrder;

        // Constructor to initialize the sales order
        public SalesCreateScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();
            int spaces = 35;
            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Skriv et kunde nummer:");
            WindowHelper.Bot(spaces);
            Console.SetCursorPosition(25, 2);
            if (!int.TryParse(Console.ReadLine(), out int customerID))
            {
                // Display an error message if the input is not a valid integer
                Console.WriteLine("Ugyldig nummer. Skriv et gyldigt tal.");
                return;
            }

            // Get the customer details from the database
            var customer = Database.Instance.GetCustomerbyID(customerID);

            // Check if the customer exists
            if (customer == null)
            {
                // Prompt the user to create a new customer
                Console.WriteLine("Kunde kunne ikke findes. Vil du skabe en ny kunde? (y/n)");
                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                // Check if the user wants to create a new customer
                if (input == 'y' || input == 'Y')
                {
                    // Create a new Customer object
                    Customer newCustomer = new Customer();

                    // Create a new Form object to edit the customer details
                    Form<Customer> customerEditor = new Form<Customer>();

                    // Define the layout of the form
                    int spacer = 35;
                    WindowHelper.Top(spacer);
                    Console.WriteLine("│{0,-35}│", "Tryk Esc for at gemme");
                    WindowHelper.Bot(spacer);

                    // Add fields to the form to edit the customer details
                    customerEditor.TextBox("Fornavn", "FirstName");
                    customerEditor.TextBox("Efternavn", "LastName");
                    customerEditor.TextBox("Telefon", "Phone");
                    customerEditor.TextBox("Email", "Email");
                    customerEditor.TextBox("Vej", "Street");
                    customerEditor.TextBox("Husnummer", "HouseNumber");
                    customerEditor.TextBox("Postnummer", "ZipCode");
                    customerEditor.TextBox("By", "City");
                    customerEditor.TextBox("Land", "Country");

                    // Edit the customer details using the form
                    bool success = customerEditor.Edit(newCustomer);

                    // Check if the customer details were edited successfully
                    if (success)
                    {
                        // Set the customer ID and last purchase date
                        //newCustomer.CustomerID = customerID;
                        newCustomer.LastPurchaseDate = DateTime.Now;

                        // Display a message to the user
                        Console.WriteLine($"Ordre skabt for {newCustomer.FullName}.");

                        // Insert the new customer into the database
                        Database.Instance.InsertCustomer(newCustomer);

                        // Set the customer number of the sales order
                        _salesOrder.CustomerNumber = Database.Instance.GetCustomerbyID(newCustomer.CustomerID).CustomerID;
                    }
                    else
                    {
                        // Display an error message if the customer details were not edited successfully
                        Console.WriteLine("Kunde oprettelse annulleret.");
                        return;
                    }
                }
                else
                {
                    // Display an error message if the user does not want to create a new customer
                    Console.WriteLine("Ordre oprettelse annulleret.");
                    return;
                }
            }
            else
            {
                // Set the creation date of the sales order
                _salesOrder.CreationDate = DateTime.Now;

                // Display a message to the user
                Console.WriteLine($"Ordre skabt for {customer.FullName}.");
            }

            try
            {
                // Insert the sales order into the database
                Database.Instance.InsertSalesOrderHeader(_salesOrder);

                // Get the order ID
                int orderID = _salesOrder.OrderNumber;

                // Add order lines to the sales order
                AddOrderLines(orderID);
            }
            catch (Exception ex)
            {
                // Display an error message if an exception occurs
                Console.WriteLine("Fejl under oprettelse af order: " + ex.Message);
            }

            // Quit the screen
            this.Quit();
        }

        // Method to add order lines to the sales order
        private void AddOrderLines(int orderId)
        {
            // Get the list of products from the database
            List<Product> products = Database.Instance.Products;

            // Check if there are any products
            if (products.Count == 0)
            {
                // Display an error message if there are no products
                Console.WriteLine("Ingen produkter tilgængelige for valg.");
                return;
            }

            // Loop until the user wants to stop adding order lines
            while (true)
            {
                // Display the list of products
                Console.WriteLine("Tilgængelige produkter:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, {product.Name} - Pris: {product.SellingPrice} DKK");
                }

                // Prompt the user to enter a product ID
                Console.Write("Skriv produkt ID for at tilføje til ordren (eller 'q' for at afslutte): ");
                var input = Console.ReadLine();

                // Check if the user wants to stop adding order lines
                if (input.ToLower() == "q") break;

                // Check if the input is a valid integer
                if (!int.TryParse(input, out int productId) || !products.Exists(p => p.ProductID == productId))
                {
                    // Display an error message if the input is not a valid integer
                    Console.WriteLine("Ugyldigt produkt ID. Prøv igen.");
                    continue;
                }

                // Get the selected product
                Product selectedProduct = products.Find(p => p.ProductID == productId);

                // Prompt the user to enter a quantity
                Console.Write($"Skriv mængde for {selectedProduct.Name}: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                {
                    // Display an error message if the input is not a valid quantity
                    Console.WriteLine("Ugyldig mængde. Annullerer tilføjelse af produkt.");
                    continue;
                }

                // Create a new SalesOrderLine object
                SalesOrderLine orderLine = new SalesOrderLine
                {
                    ProductID = selectedProduct.ProductID,
                    Name = selectedProduct.Name,
                    Price = selectedProduct.SellingPrice,
                    Quantity = quantity,
                    SalesOrderHeadID = orderId,
                };

                // Insert the order line into the database
                Database.Instance.InsertSalesOrderline(orderLine);

                // Clear the screen
                Clear();

                // Display a message to the user
                Console.WriteLine($"Tilføjede {quantity} x {selectedProduct.Name} til ordren.");
            }

            // Display the total amount of the sales order
            Console.WriteLine($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }
    }
}