﻿using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    // Define a class CompanyEditScreen that inherits from Screen
    public class CompanyEditScreen : Screen
    {
        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Rediger virksomhed";

        // Private field to store the Company object
        private Company _company;

        // Constructor that takes a Company object as a parameter
        public CompanyEditScreen(Company company)
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

            // Add text boxes to the form for editing company details
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

            // Quit the screen
            this.Quit();
        }
    }
}