using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    // Define a class CompanyCreateScreen that inherits from Screen
    public class CompanyCreateScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Rediger Virksomhed";

        // Private field to store the Company object
        private Company _company;

        // Constructor that takes a Company object as a parameter
        public CompanyCreateScreen(Company company)
        {
            _company = company;
        }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Clear the screen
            Clear();

            // Create a new Form object to edit the Company object
            Form<Company> editor = new Form<Company>();

<<<<<<< HEAD
            // Add text boxes to the form for editing company details
=======
            int spacer = 35;
            WindowHelper.Top(spacer);
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at gemme");
            WindowHelper.Bot(spacer);


>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            editor.TextBox("Virksomheds Navn", "CompanyName");
            editor.TextBox("Vej", "Street");
            editor.TextBox("Hus nummer", "HouseNumber");
            editor.TextBox("Post nummer", "ZipCode");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");

            // Add a select box to the form for choosing the currency
            editor.SelectBox("Valuta", "Currency");

            // Add options to the select box
            editor.AddOption("Valuta", "DKK", Currency.DKK);
            editor.AddOption("Valuta", "SEK", Currency.SEK);
            editor.AddOption("Valuta", "USD", Currency.USD);
            editor.AddOption("Valuta", "EUR", Currency.EUR);

            // Edit the Company object using the form
            editor.Edit(_company);

<<<<<<< HEAD
            // Quit the screen
=======

>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            this.Quit();
        }
    }
}