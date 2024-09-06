using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerListScreen : Screen
    {
        private ListPage<Customer> listPage;

        public CustomerListScreen()
        {
            listPage = new ListPage<Customer>();
            listPage.Add(new Customer { FirstName = "Peter", LastName = "Larsen" });
            listPage.Add(new Customer { FirstName = "Jens", LastName = "Thorsen" });
        }

        public override string Title { get; set; } = "Customer";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Press F1 to create a customer");
            Console.WriteLine("Press F2 to edit a customer");
            Console.WriteLine("Press F5 to delete a customer");

            // listPage.AddKey(ConsoleKey.F1, CreateCustomer);
            // listPage.AddKey(ConsoleKey.F2, EditCustomer);
            // listPage.AddKey(ConsoleKey.F5, DeleteCustomer);

            listPage.AddColumn("First Name", "FirstName");
            listPage.AddColumn("Last Name", "LastName");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                // Screen.Display(new CustomerViewScreen(selected));
            }
            else
            {
                Quit();
            }
        }
    }
}
