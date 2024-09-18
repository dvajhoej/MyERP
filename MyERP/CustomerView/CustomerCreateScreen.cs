using MyERP.CompanyView;
using System;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerCreateScreen : Screen
    {
        public override string Title { get; set; } = "Opret kunde"; 

        private Customer _customer;

        public CustomerCreateScreen(Customer customer)
        {
            _customer = customer ?? new Customer(); 
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

            try
            {
                Database.InsertCustomer(_customer); 
                Console.WriteLine("Customer successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while creating customer: " + ex.Message);
            }

            this.Quit();
        }
    }
}
