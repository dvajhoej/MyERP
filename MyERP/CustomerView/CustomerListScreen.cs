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
            listPage = new ListPage<Customer>();
            listPage.Add(Database.Instance.Customers);
            listPage.AddKey(ConsoleKey.F1, CreateCustomer);
            listPage.AddKey(ConsoleKey.F2, EditCustomer);
            //listPage.AddKey(ConsoleKey.F5, DeleteCustomer);
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

            Console.WriteLine("Tryk F1 for at oprette  en kunde");
            Console.WriteLine("Tryk F2 for at redigere en kunde");
            Console.WriteLine("Tryk F5 for at slette   en kunde");
            

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


        private void SearchCustomer(Customer customer)
        {
            string search;
            do
            {
                CleanLine06andselect();

                Console.Write("Enter Customer name or ID: ");
                search = Console.ReadLine().ToLower();
            } while (string.IsNullOrEmpty(search));


            var filtered = Database.Instance.Customers
                .Where(c => c.FirstName.ToLower().Contains(search) ||
                            c.FullName.ToLower().Contains(search) ||
                            c.LastName.ToLower().Contains(search) ||
                            c.CustomerID.ToString().Contains(search))
                .ToList();

            if (filtered.Any())
            {
                Clear();
                listPage.Clear();
                listPage.Add(filtered);
                Draw();

            }
            else
            {
                
                CleanLine06andselect();
                Console.WriteLine("No matching customer found.");
                Console.WriteLine("Press a key to continue.");

                Console.ReadKey();
            }
        }
        private void CleanLine06andselect()
        {
            Console.SetCursorPosition(0, 6);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 6);
        }


        private void CreateCustomer(Customer customer)
        {
            var newCustomer = new Customer();
            listPage.Add(newCustomer);
            Screen.Display(new CustomerCreateScreen(newCustomer));
        }

        private void EditCustomer(Customer selected)
        {
            Screen.Display(new CustomerEditScreen(selected));
        }


        //public void DeleteCustomer(Customer selected)
        //{
        //    if (selected != null)
        //    {
        //        try
        //        {
        //            Database.Instance.DeleteCustomerByID(selected.CustomerID);
        //            listPage.Remove(selected);
        //            Database.Instance.Customers.Remove(selected);
        //            listPage.
                   
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine($"Error: {ex.Message}");
        //        }
        //        finally
        //        {
        //            Console.SetCursorPosition(0, 5);
        //            Console.WriteLine($"Company '{selected.FullName}' has been deleted.");
        //            Console.WriteLine("Press any key.");
        //            Console.ReadKey();
        //        }
                
        //    }
        //    else
        //    {
        //        Console.WriteLine("Ingen kunde valgt.");
        //    }
        //}
    }
}

