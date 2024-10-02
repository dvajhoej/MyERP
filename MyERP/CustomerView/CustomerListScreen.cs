using MyERP.CompanyView;
using System.ComponentModel;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerListScreen : Screen
    {

        private ListPage<Customer> listPage;

        public CustomerListScreen()
        {
            listPage = new ListPage<Customer>(Database.Instance.Customers);
            listPage.AddKey(ConsoleKey.F1, CreateCustomer);
            listPage.AddKey(ConsoleKey.F2, EditCustomer);
            listPage.AddKey(ConsoleKey.F5, DeleteCustomer);
            listPage.AddKey(ConsoleKey.Escape, Quit);
            listPage.AddColumn("Fornavn", "FirstName");
            listPage.AddColumn("Efternavn", "LastName");
            listPage.AddColumn("Kunde nummer", "CustomerID");
            listPage.AddColumn("Street", "Street");

        }

        public override string Title { get; set; } = "Kunder";

        protected override void Draw()
        {
            Clear();


            int spaces = 35;

            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette  en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette   en kunde");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");

            WindowHelper.Bot(spaces);

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }

            

            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new CustomerViewScreen(selected));
            }
         

        }

        void Quit(Customer _)
        {
            Quit();
        }


        private void CreateCustomer(Customer customer)
        {
            var newCustomer = new Customer();
            Screen.Display(new CustomerCreateScreen(newCustomer));


            try
            {
                Database.Instance.InsertCustomer(newCustomer);

                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{newCustomer.FullName} oprettet");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 70;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-70}│", $"Fejl under oprettelse af kunde: " + ex.Message);
                Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }

        private void EditCustomer(Customer selected)
        {
            Screen.Display(new CustomerEditScreen(selected));


            try
            {
                Database.Instance.UpdateCustomer(selected);
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{selected.FullName} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 70;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-70}│", $"Fejl under redigering af kunde: " + ex.Message);
                Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }


        public void DeleteCustomer(Customer selected)
        {
            if (selected != null)
            {
                try
                {
                    Database.Instance.DeleteCustomerByID(selected.CustomerID);

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

