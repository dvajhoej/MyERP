using MyERP.CompanyView;
using System;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    // Define a class CustomerCreateScreen that inherits from Screen
    public class CustomerCreateScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Opret kunde"; // Danish for "Create customer"

        // Private field to store the Customer object
        private Customer _customer;

        // Constructor that takes a Customer object as a parameter
        public CustomerCreateScreen(Customer customer)
        {
            // Set the _customer field to the provided customer, or a new Customer object if null
            _customer = customer ?? new Customer();
        }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Create a new Form object to edit the Customer object
            Form<Customer> editor = new Form<Customer>();

            // Add text boxes to the form for editing customer details
            editor.TextBox("Fornavn", "FirstName");
            editor.TextBox("Efternavn", "LastName");
            editor.TextBox("Telefon", "Phone");
            editor.TextBox("Email", "Email");
            editor.TextBox("Vej", "Street");
            editor.TextBox("Husnummer", "HouseNumber");
            editor.TextBox("Postnummer", "ZipCode");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");

            // Edit the Customer object using the form
            editor.Edit(_customer);

            // Quit the screen
            this.Quit();
        }
    }
}