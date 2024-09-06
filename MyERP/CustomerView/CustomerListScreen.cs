using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerListScreen : Screen
    {
        private ListPage<Customer> listPage;

        public CustomerListScreen()
        {
            listPage = new ListPage<Customer>();
            listPage.Add(new Customer { FirstName = "Peter", LastName = "Larsen", Street = "TEST" });
            listPage.Add(new Customer { FirstName = "Jens", LastName = "Thorsen", Street = "TEST" });
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

            listPage.AddColumn("Fornavn", "FirstName");
            listPage.AddColumn("Efternavn", "LastName");
            listPage.AddColumn("Kunde nummer", "CustomerID");
            listPage.AddColumn("Street", "Street");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                //Screen.Display(new CustomerListScreen(selected));
            }
            else
            {
                Quit();
            }
        }
        private void CreateCustomer(Customer customer)
        {
            var newCustomer = new Customer();
            listPage.Add(newCustomer);
            Screen.Display(new CustomerCreateScreen(newCustomer));

            //if (newCompany.CompanyName != null)
            //{
            //    listPage.Add(newCompany);
            //}

            // AFVENTER IMPLEMENTERING
        }

        private void EditCustomer(Customer selected)
        {
            Screen.Display(new CustomerEditScreen(selected));
        }

        public void DeleteCustomer(Customer selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Company '{selected.FullName}' has been deleted.");
            }
        }
    }
}

