using MyERP.CompanyView;
using System.ComponentModel;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    // Define a class CustomerListScreen that inherits from Screen
    public class CustomerListScreen : Screen
    {
        // Private field to store the ListPage object
        private ListPage<Customer> listPage;

        // Constructor that initializes the ListPage object
        public CustomerListScreen()
        {
            // Create a new ListPage object with the list of customers from the database
            listPage = new ListPage<Customer>(Database.Instance.Customers);

            // Add keyboard shortcuts to the ListPage object
            listPage.AddKey(ConsoleKey.F1, CreateCustomer); // Create a new customer
            listPage.AddKey(ConsoleKey.F2, EditCustomer); // Edit a customer
            listPage.AddKey(ConsoleKey.F5, DeleteCustomer); // Delete a customer
            listPage.AddKey(ConsoleKey.Escape, Quit); // Quit the screen

            // Add columns to the ListPage object
            listPage.AddColumn("Fornavn", "FirstName");
            listPage.AddColumn("Efternavn", "LastName");
            listPage.AddColumn("Kunde nummer", "CustomerID");
            listPage.AddColumn("Street", "Street");
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Kunder";

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Calculate the number of spaces for the window border
            int spaces = 35;

            // Draw the top border of the window
            WindowHelper.Top(spaces);

            // Display instructions for the user
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette  en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette   en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Bot(spaces);

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                // Display the CustomerViewScreen for the selected customer
                Screen.Display(new CustomerViewScreen(selected));
            }
        }

        // Method to quit the screen
        void Quit(Customer _)
        {
            // Quit the screen
            Quit();
        }

        // Method to create a new customer
        private void CreateCustomer(Customer customer)
        {
            // Create a new Customer object
            var newCustomer = new Customer();

            // Display the CustomerCreateScreen to create the new customer
            Screen.Display(new CustomerCreateScreen(newCustomer));

            try
            {
                // Insert the new customer into the database
                Database.Instance.InsertCustomer(newCustomer);

                // Display a success message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{newCustomer.FullName} oprettet");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under oprettelse af kunde: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }

        // Method to edit a customer
        private void EditCustomer(Customer selected)
        {
            // Display the CustomerEditScreen to edit the customer
            Screen.Display(new CustomerEditScreen(selected));

            try
            {
                // Update the customer in the database
                Database.Instance.UpdateCustomer(selected);

                // Display a success message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{selected.FullName} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under redigering af kunde: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }

        // Method to delete a customer
        public void DeleteCustomer(Customer selected)
        {
            if (selected != null)
            {
                try
                {
                    // Delete the customer from the database
                    Database.Instance.DeleteCustomerByID(selected.CustomerID);

                    // Display a success message to the user
                    int spaces = 40;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-40}│", $"{selected.FullName} slettet");
                    Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    int spaces = 120;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af kunde:{WindowHelper.Truncate(ex.Message, 70)}");
                    Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
            }
            else
            {
                int spaces = 60;
                Console.SetCursorPosition(0, 7);
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-60}│", "Ingen kunde valgt");
                Console.WriteLine("│{0,-60}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }
    }
}