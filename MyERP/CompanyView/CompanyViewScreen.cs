using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyViewScreen : Screen
    {
        public Company companyDisplay { get; set; }

        public CompanyViewScreen(Company c)
        {
            company = c;
            ExitOnEscape();
        }

        public override string Title { get; set; } = "Virksomheds Visning";
        public Company company { get; private set; }

        protected override void Draw()
        {
            Console.WriteLine($"Navn:        {company.CompanyName}");
            Console.WriteLine($"Address:     {company.Street} {company.HouseNumber}");
            Console.WriteLine($"Postnummer:  {company.ZipCode}");
            Console.WriteLine($"By:          {company.City}");
            Console.WriteLine($"Land:        {company.Country}");
            Console.WriteLine($"Currency:    {company.Currency}");
        }
    }
}
