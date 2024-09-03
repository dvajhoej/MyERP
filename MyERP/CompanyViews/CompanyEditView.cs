using TECHCOOL.UI;

namespace MyERP.CompanyViews
{
    public class CompanyEditView : Screen
    {
        public override string Title { get; set; } = "Edit Company";

        private Company _company;

        public CompanyEditView(Company company)
        {
            _company = company;
        }

        protected override void Draw()
        {
            Clear();

            Form<Company> editor = new Form<Company>();

            editor.TextBox("Company Name", "Company Name");
            editor.TextBox("Street", "Street");
            editor.IntBox("House Number", "House Number");
            editor.IntBox("Postal Code", "Postal Code");
            editor.TextBox("City", "City");
            editor.TextBox("Country", "Country");
            editor.SelectBox("Currency", "Currency");
            editor.AddOption("Currency", "DKK", Currency.DKK);
            editor.AddOption("Currency", "SEK", Currency.SEK);
            editor.AddOption("Currency", "USD", Currency.USD);
            editor.AddOption("Currency", "EUR", Currency.EUR);

            editor.Edit(_company);

            Console.WriteLine($"Company {_company.CompanyName} has been updated.");
            this.Quit();
        }
    }
}
