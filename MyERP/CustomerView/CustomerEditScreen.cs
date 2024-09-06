using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerEditScreen : Screen
    {
        public override string Title { get; set; } = "Rediger kunde";

        private Customer _customer;

        public CustomerEditScreen(Customer kunde)
        {
            _customer = kunde;
        }

        protected override void Draw()
        {
            Clear();

            Form<Customer> editor = new Form<Customer>();



            editor.TextBox("Fornavn", "FirstName");
            editor.TextBox("Efternavn", "LastName");
            editor.TextBox("Telefon", "Phone");
            editor.TextBox("Email", "Email");
            editor.TextBox("Vej", "Street");
            editor.TextBox("Husnummer", "HouseNumber");
            editor.TextBox("Postnummer", "ZipCode");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");

            editor.Edit(_customer);


            this.Quit();
        }
    }
}