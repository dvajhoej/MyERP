using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesCreateScreen : Screen
    {
        public override string Title { get; set; } = "Opret Ordre";

        private SalesOrderHeader _salesOrder;

        public SalesCreateScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Skriv et kunde nummer:");
            if (!int.TryParse(Console.ReadLine(), out int customerID))
            {
                Console.WriteLine("Ugyldig nummer. Skriv et gyldigt tal.");
                return;
            }

            var customer = Database.Instance.GetCustomerbyID(customerID);

            if (customer == null)
            {
                Console.WriteLine("Kunde kunne ikke findes. Vil du skabe en ny kunde? (y/n)");
                var input = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (input == 'y' || input == 'Y')
                {
                    Customer newCustomer = new Customer();
                    Form<Customer> customerEditor = new Form<Customer>();

                    customerEditor.TextBox("Fornavn", "FirstName");
                    customerEditor.TextBox("Efternavn", "LastName");
                    customerEditor.TextBox("Telefon", "Phone");
                    customerEditor.TextBox("Email", "Email");
                    customerEditor.TextBox("Vej", "Street");
                    customerEditor.TextBox("Husnummer", "HouseNumber");
                    customerEditor.TextBox("Postnummer", "ZipCode");
                    customerEditor.TextBox("By", "City");
                    customerEditor.TextBox("Land", "Country");

                    bool success = customerEditor.Edit(newCustomer);

                    if (success)
                    {
                        Database.Instance.InsertCustomer(newCustomer);
                        _salesOrder = new SalesOrderHeader(orderNumber: GenerateOrderNumber(), customerNumber: newCustomer.CustomerID);
                    }
                    else
                    {
                        Console.WriteLine("Kundeoprettelse annulleret.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Ordreoprettelse annulleret.");
                    return;
                }
            }
            else
            {
                _salesOrder = new SalesOrderHeader(orderNumber: GenerateOrderNumber(), customerNumber: customer.CustomerID);
                Console.WriteLine($"Ordre skabt for {customer.Fullname}.");
            }

            AddOrderLines();

            try
            {
                // Database.Instance.InsertSalesOrderHeader(_salesOrder);
                Console.WriteLine("Order blev oprettet.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl under oprettelse af order: " + ex.Message);
            }

            this.Quit();
        }

        private int GenerateOrderNumber()
        {
            return new Random().Next(1000, 9999);
        }

        private void AddOrderLines()
        {
            List<Product> products = Database.Instance.GetAllProducts();

            if (products.Count == 0)
            {
                Console.WriteLine("Ingen produkter tilgængelige for valg.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Tilgængelige produkter:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, {product.Name} - Pris: {product.SellingPrice} DKK");
                }

                Console.Write("Skriv produkt ID for at tilføje til ordren (eller 'q' for at afslutte): ");
                var input = Console.ReadLine();

                if (input.ToLower() == "q") break;

                if (!int.TryParse(input, out int productId) || !products.Exists(p => p.ProductID == productId))
                {
                    Console.WriteLine("Ugyldigt produkt ID. Prøv igen.");
                    continue;
                }

                Product selectedProduct = products.Find(p => p.ProductID == productId);

                Console.Write($"Skriv mængde for {selectedProduct.Name}: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                {
                    Console.WriteLine("Ugyldig mængde. Annullerer tilføjelse af produkt.");
                    continue;
                }

                SalesOrderLine orderLine = new SalesOrderLine
                {
                    ProductID = selectedProduct.ProductID,
                    Name = selectedProduct.Name,
                    Price = selectedProduct.SellingPrice,
                    Quantity = quantity
                };

                _salesOrder.AddOrderLine(orderLine);
                Console.WriteLine($"Tilføjede {quantity} x {selectedProduct.Name} til ordren.");
            }

            Console.WriteLine($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }
    }
}
