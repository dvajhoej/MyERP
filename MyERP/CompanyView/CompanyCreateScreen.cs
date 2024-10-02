using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyCreateScreen : Screen
    {
        public override string Title { get; set; } = "Rediger Virksomhed";

        private Company _company;

        public CompanyCreateScreen(Company company)
        {
            _company = company;
        }

        protected override void Draw()
        {
            Clear();

            Form<Company> editor = new Form<Company>();

            int spacer = 35;
            WindowHelper.Top(spacer);
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at gemme");
            WindowHelper.Bot(spacer);


            editor.TextBox("Virksomheds Navn", "CompanyName");
            editor.TextBox("Vej", "Street");
            editor.TextBox("Hus nummer", "HouseNumber");
            editor.TextBox("Post nummer", "ZipCode");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");
            editor.SelectBox("Valuta", "Currency");
            editor.AddOption("Valuta", "DKK", Currency.DKK);
            editor.AddOption("Valuta", "SEK", Currency.SEK);
            editor.AddOption("Valuta", "USD", Currency.USD);
            editor.AddOption("Valuta", "EUR", Currency.EUR);

            editor.Edit(_company);


            this.Quit();

        }
    }
}
