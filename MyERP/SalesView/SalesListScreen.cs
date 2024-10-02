using TECHCOOL.UI;

namespace MyERP.SalesView
{
    // Define a class SalesListScreen to display a list of sales orders
    public class SalesListScreen : Screen
    {
        // Private field to store the list page
        private ListPage<SalesOrderHeader> listPage;

        // Constructor to initialize the list page
        public SalesListScreen()
        {
            // Create a new ListPage object to display the sales orders
            listPage = new ListPage<SalesOrderHeader>(Database.Instance.SalesOrderHeaders);

            // Add keys to the list page to perform actions
            listPage.AddKey(ConsoleKey.F1, CreateOrder);
            listPage.AddKey(ConsoleKey.F2, EditOrder);
            listPage.AddKey(ConsoleKey.F5, DeleteOrder);
            listPage.AddKey(ConsoleKey.F9, PrintInvoice);
            listPage.AddKey(ConsoleKey.Escape, Quit);

            // Add columns to the list page to display the sales order details
            listPage.AddColumn("Ordre nummer", "OrderNumber", 25);
            listPage.AddColumn("Oprettelse", "CreationDate", 25);
            listPage.AddColumn("Færdig", "CompletionDate", 25);
            listPage.AddColumn("Kunde nummer", "CustomerNumber", 25);
            listPage.AddColumn("Kunde Navn", "Fullname", 25);
            listPage.AddColumn("Status", "Status", 25);
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Ordre";

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
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F9 for at oprette  en faktura");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Bot(spaces);

            // Add some blank lines to the screen
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }

            // Select a sales order from the list page
            var selected = listPage.Select();

            // If a sales order is selected, display the sales view screen
            if (selected != null)
            {
                Screen.Display(new SalesViewScreen(selected));
            }
        }

        // Method to quit the screen
        void Quit(SalesOrderHeader _)
        {
            Quit();
        }

        // Method to create a new sales order
        private void CreateOrder(SalesOrderHeader order)
        {
            // Create a new SalesOrderHeader object
            var newOrder = new SalesOrderHeader();

            // Display the sales create screen
            Screen.Display(new SalesCreateScreen(newOrder));
          
            //try
            //{
            //    // Add the new sales order to the list page

            //    // Display a message to the user
            //    Console.WriteLine("Order successfully created.");
            //    Console.ReadLine();
            //}
            //catch (Exception ex)
            //{
            //    // Display an error message to the user
            //    Console.WriteLine($"An error occurred: {ex.Message}");
            //    Console.ReadLine();
            //}
        }

        // Method to print an invoice for a sales order
        public void PrintInvoice(SalesOrderHeader selected)
        {
            try
            {
                // Create a new Invoice object
                var newInvoice = new Invoice();

                // Insert the invoice into the database
                Database.Instance.InsertInvoice(selected, newInvoice);

                // Generate the invoice
                Invoice.GenerateInvoice(selected, newInvoice);
            }
            catch (Exception ex)
            {
                // Display an error message to the user
                Console.SetCursorPosition(0, 8);
                WindowHelper.Top(70);
                Console.WriteLine("│{0,-70}│", $"Der er sket en fejl under oprettelse af faktura : {ex.Message}");
                WindowHelper.Bot(70);
                Console.ReadKey();
            }
        }

        // Method to edit a sales order
        private void EditOrder(SalesOrderHeader selected)
        {
            // Display the sales edit screen
            Screen.Display(new SalesEditScreen(selected));


            try
            {

                Database.Instance.UpdateSalesOrderHeader(selected);

                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"Ordre nr: {selected.OrderNumber} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();


            }
            catch (Exception ex)
            {
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under redigering af ordre: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }


        }

        // Method to delete a sales order
        private void DeleteOrder(SalesOrderHeader selected)
        {
            if (selected != null)
            {
                try
                {
                    // Delete the sales order from the database
                    Database.Instance.DeleteSalesOrderHeadByID(selected.OrderNumber);

                    // Remove the sales order from the list page
                    listPage.Remove(selected);

                    // Display a message to the user
                    int spaces = 70;
                    Console.SetCursorPosition(0, 8);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-70}│", $"Ordre nr {selected.OrderNumber} er blevet slettet.");
                    Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadLine();
                }
                catch (Exception ex)
                {
                    // Display an error message to the user
                    int spaces = 70;
                    Console.SetCursorPosition(0, 8);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af ordre:{WindowHelper.Truncate(ex.Message, 70)}");
                    Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadLine();
                }
            }
        }
    }
}