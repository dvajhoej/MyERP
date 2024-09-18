using System.ComponentModel;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerListScreen : Screen
    {

        private ListPage<Customer> listPage;

        public CustomerListScreen()
        {
            listPage = new ListPage<Customer>();
            listPage.Add(Database.Instance.Customers);

        }

        public override string Title { get; set; } = "Kunder";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette  en kunde");
            Console.WriteLine("Tryk F2 for at redigere en kunde");
            Console.WriteLine("Tryk F5 for at slette   en kunde");
            listPage.AddKey(ConsoleKey.F1, CreateCustomer);
            listPage.AddKey(ConsoleKey.F2, EditCustomer);
            listPage.AddKey(ConsoleKey.F5, DeleteCustomer);
            listPage.AddKey(ConsoleKey.Escape, Quit);
            listPage.AddColumn("Fornavn", "FirstName");
            listPage.AddColumn("Efternavn", "LastName");
            listPage.AddColumn("Kunde nummer", "CustomerID");
            listPage.AddColumn("Street", "Street");

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
            listPage.Add(newCustomer);
            Screen.Display(new CustomerCreateScreen(newCustomer));
        }

        private void EditCustomer(Customer selected)
        {
            Screen.Display(new CustomerEditScreen(selected, Database.Instance));
        }


        public void DeleteCustomer(Customer selected)
        {
            if (selected != null)
            {

                listPage.Remove(selected);
                Database.Instance.DeleteCustomerByID(selected.CustomerID);
                Console.WriteLine($"'{selected.FullName}' er blevet slettet.");
            }
            else
            {
                Console.WriteLine("Ingen kunde valgt.");
            }
        }
    }
}

