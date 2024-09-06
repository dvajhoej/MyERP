using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyEditScreen : Screen
    {
        public override string Title { get; set; } = "Edit Company";

        private Company _company;

        public CompanyEditScreen(Company company)
        {
            _company = company;
        }

        protected override void Draw()
        {
            Clear();

            Form<Company> editor = new Form<Company>();

            editor.TextBox("Virksomheds Navn", "CompanyName");
            editor.TextBox("Vej", "Street");
            editor.IntBox("Hus nummer", "HouseNumber");
            editor.IntBox("Post nummer", "ZipCode");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");
            editor.SelectBox("Valuta", "Currency");
            editor.AddOption("Valuta", "DKK", Currency.DKK);
            editor.AddOption("Valuta", "SEK", Currency.SEK);
            editor.AddOption("Valuta", "USD", Currency.USD);
            editor.AddOption("Valuta", "EUR", Currency.EUR);

            editor.Edit(_company);

            Console.WriteLine($"Virksomhed: {_company.CompanyName} er blevet opdateret.");
            this.Quit();
        }
    }
}
