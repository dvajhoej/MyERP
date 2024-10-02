using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesCreateScreen : Screen
    {
        int spaces;

        public override string Title { get; set; } = "Opret Ordre";

        private SalesOrderHeader _salesOrder;

        public SalesCreateScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        protected override void Draw()
        {
            spaces = 35;
            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Skriv exit for at gå tilbage");
            WindowHelper.Bot(spaces);

            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Skriv et kunde nummer:");
            WindowHelper.Bot(spaces);
            Console.SetCursorPosition(25, 5);

            string readline = Console.ReadLine();
            if (readline == "exit")
            {
                this.Quit();
                return;
            }

            if (!int.TryParse(readline, out int customerID))
            {
                ShowError("Ugyldig nummer. Skriv et gyldigt tal.");
                return;
            }

            // Get the customer details from the database
            var customer = Database.Instance.GetCustomerbyID(customerID);
            Customer cust;

            // Check if the customer exists
            if (customer == null)
            {
                cust = CreateNewCustomer();
                if (cust == null)
                {
                    // User canceled customer creation
                    ShowError("Kunde oprettelse annulleret.");
                    return;
                }
            }
            else
            {
                cust = customer;
                ShowMessage($"Ordre skabt for {customer.FullName}");
            }

            // Continue with creating the sales order
            try
            {
                _salesOrder.CustomerNumber = cust.CustomerID;
                _salesOrder.Firstname = cust.FirstName;
                _salesOrder.Lastname = cust.LastName;
                _salesOrder.Email = cust.Email;
                _salesOrder.Phone = cust.Phone;

                _salesOrder.CreationDate = DateTime.Now;
                Database.Instance.InsertSalesOrderHeader(_salesOrder);

                int orderID = _salesOrder.OrderNumber;
                AddOrderLines(orderID);
            }
            catch (Exception ex)
            {
                throw new Exception("Fejl under oprettelse af ordre: " + ex.Message);
            }

            this.Quit();
        }

        private Customer CreateNewCustomer()
        {
            // Prompt the user to create a new customer
            spaces = 60;
            Console.Clear();
            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-60}│", "Kunde kunne ikke findes. Vil du skabe en ny kunde? (y/n): ");
            WindowHelper.Bot(spaces);
            Console.SetCursorPosition(59, 1);

            var input = Console.ReadKey().KeyChar;
            if (input != 'y' && input != 'Y')
            {
                return null;  // User does not want to create a new customer
            }

            // Create a new customer
            Customer newCustomer = new Customer();
            Form<Customer> customerEditor = new Form<Customer>();

            // Add fields for customer details
            Console.Clear();
            int spacer = 35;
            WindowHelper.Top(spacer);
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at gemme");
            WindowHelper.Bot(spacer);

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

            if (!success) return null;  // User canceled customer creation

            newCustomer.LastPurchaseDate = DateTime.Now;
            Database.Instance.InsertCustomer(newCustomer);

            ShowMessage($"Ordre skabt for {newCustomer.FullName}");
            return newCustomer;
        }

        private void AddOrderLines(int orderId)
        {
            List<Product> products = Database.Instance.Products;

            if (products.Count == 0)
            {
                ShowError("Ingen produkter tilgængelige for valg.");
                return;
            }

            double total = 0;

            while (true)
            {
                Clear();
                spaces = 73;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-73}│", "Tilgængelige produkter:");
                Console.WriteLine("│{0,-73}│", "");
                Console.WriteLine("│{0,-17}│{1,-27}│{2,-17}│{3,-9}│", "Produkt ID", "Produkt", "Pris", "Valuta");

                foreach (var product in products)
                {
                    Console.WriteLine("│ {0,-15} │ {1,-25} │ {2,-15} │ {3,-7} │", product.ProductID, product.Name, product.SellingPrice, "DKK");
                }

                WindowHelper.Bot(spaces);

                spaces = 80;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-80}│", "Skriv produkt ID for at tilføje til ordren (eller 'q' for at afslutte): ");
                WindowHelper.Bot(spaces);
                Console.SetCursorPosition(74, 12);

                var input = Console.ReadLine();
                if (input.ToLower() == "q") break;

                if (!int.TryParse(input, out int productId) || !products.Exists(p => p.ProductID == productId))
                {
                    ShowError("Ugyldigt produkt ID. Prøv igen.");
                    continue;
                }

                Product selectedProduct = products.Find(p => p.ProductID == productId);
                Console.WriteLine($"Skriv mængde for {selectedProduct.Name}: ");
                if (!double.TryParse(Console.ReadLine(), out double quantity) || quantity <= 0)
                {
                    ShowError("Ugyldig mængde. Annullerer tilføjelse af produkt.");
                    continue;
                }

                SalesOrderLine orderLine = new SalesOrderLine
                {
                    ProductID = selectedProduct.ProductID,
                    Name = selectedProduct.Name,
                    Price = selectedProduct.SellingPrice,
                    Quantity = quantity,
                    SalesOrderHeadID = orderId,
                };

                total += selectedProduct.SellingPrice * quantity;
                Database.Instance.InsertSalesOrderline(orderLine);

                ShowMessage($"Tilføjede {quantity} x {selectedProduct.Name} til ordren.");
            }

            _salesOrder.OrderAmount = total;
            ShowMessage($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }

        private void ShowError(string message)
        {
            int spacer = 45;
            WindowHelper.Top(spacer);
            Console.WriteLine($"│{message,-45}│");
            Console.WriteLine("│{0,-45}│", "Tryk på en tast for at fortsætte");
            WindowHelper.Bot(spacer);
            Console.ReadKey();
            Clear();
        }

        private void ShowMessage(string message)
        {
            int spacer = 45;
            WindowHelper.Top(spacer);
            Console.WriteLine($"│{message,-45}│");
            Console.WriteLine("│{0,-45}│", "Tryk på en tast for at fortsætte");
            WindowHelper.Bot(spacer);
            Console.ReadKey();
        }
    }
}
