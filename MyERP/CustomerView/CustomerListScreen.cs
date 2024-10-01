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

            for (int i = 0; i < 3; i++)
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
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl under oprettelse af kunde: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
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
                Console.WriteLine("│{0,-40}│", $"{selected.FullName} opdateret");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl under redigering af kunde: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
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
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl Under sletning af kunde: {ex.Message} ");
                    Console.WriteLine("Tryk på en tast for at fortsætte");
                    Console.ReadKey();
                }
          

            }
            else
            {
                Console.WriteLine("Ingen kunde valgt.");
            }
        }
    }
}

