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

            Form<SalesOrderHeader> editor = new Form<SalesOrderHeader>();

            var customerDictionary = GetCustomerDictionary();
            var selectBoxOptions = customerDictionary.ToDictionary(
                kvp => kvp.Key,
                kvp => (object)kvp.Value 
            );

            editor.SelectBox("Vælg kunde", "CustomerNumber", selectBoxOptions);

            editor.Edit(_salesOrder);

            this.Quit();
        }

        private Dictionary<string, Customer> GetCustomerDictionary()
        {
            var customerDictionary = new Dictionary<string, Customer>();

            var customers = Database.Instance.Customers.ToList();
            foreach (var customer in customers)
            {
                var displayName = customer.Fullname;
                customerDictionary[displayName] = customer;
            }

            return customerDictionary;
        }
    }
}
