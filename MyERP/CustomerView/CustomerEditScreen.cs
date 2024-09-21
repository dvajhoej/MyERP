using System;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerEditScreen : Screen
    {
        public override string Title { get; set; } = "Rediger kunde";

        private Customer _customer;

        public CustomerEditScreen(Customer customer)
        {
            _customer = customer;
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
            Database.Instance.UpdateCustomer(_customer);
            this.Quit();
        }
    }
}
