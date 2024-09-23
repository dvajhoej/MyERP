using Google.Protobuf.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                Console.WriteLine("ugyldig nummer. Skriv et gyldigt tal.");
                return;
            }

            var customer = Database.Instance.GetCustomerbyID(customerID);

            if (customer == null)
            {
                Console.WriteLine("Kunde ikke fundet. Vil du skabe en ny kunde (y/n)");
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
                        Console.WriteLine("Customer creation canceled.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Sales order creation canceled.");
                    return;
                }
            }
            else
            {
                _salesOrder = new SalesOrderHeader(orderNumber: GenerateOrderNumber(), customerNumber: customer.CustomerID);
                Console.WriteLine($"Order skabt for {customer.Fullname}.");
            }

            try
            {
                Database.Instance.InsertSalesOrderHeader(_salesOrder);
                Console.WriteLine("Sale successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating sale: " + ex.Message);
            }

            this.Quit();
        }

        private int GenerateOrderNumber()
        {
            return new Random().Next(1000, 9999);
        }

     }

}
