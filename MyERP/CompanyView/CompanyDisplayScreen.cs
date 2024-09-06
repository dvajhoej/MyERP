using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyDisplayScreen : Screen
    {
        public Company companyDisplay { get; set; }

        public CompanyDisplayScreen(Company c)
        {
            company = new Company
            {
                CompanyName = c.CompanyName,
                Country = c.Country,
                Currency = c.Currency,
                Street = c.Street,
                HouseNumber = c.HouseNumber,
                PostalCode = c.PostalCode,
                City = c.City,
            };
        }

        public override string Title { get; set; } = "Company Display";
        public Company company { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Company> listPage = new();

            listPage.Add(company);

            listPage.AddColumn("Company Name", "CompanyName");
            listPage.AddColumn("Street", "Street");
            listPage.AddColumn("House Number", "HouseNumber");
            listPage.AddColumn("Postal Code", "PostalCode");
            listPage.AddColumn("City", "City");
            listPage.AddColumn("Country", "Country");
            listPage.AddColumn("Currency", "Currency");

            listPage.Select();

            var selected = listPage.Select();
            if (selected != null)
            {
            }
            else
            {
                Quit();
            }
        }
    }
}
