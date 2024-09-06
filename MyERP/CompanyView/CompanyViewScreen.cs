using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyViewScreen : Screen
    {
        public Company companyDisplay { get; set; }

        public CompanyViewScreen(Company c)
        {
            company = new Company
            {
                CompanyName = c.CompanyName,
                Country = c.Country,
                Currency = c.Currency,
                Street = c.Street,
                HouseNumber = c.HouseNumber,
                Zipcode = c.Zipcode,
                City = c.City,
            };
        }

        public override string Title { get; set; } = "Virksomheds Visning";
        public Company company { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Company> listPage = new();

            listPage.Add(company);

            listPage.AddColumn("Company Name", "CompanyName");
            listPage.AddColumn("Street", "Street");
            listPage.AddColumn("House Number", "HouseNumber");
            listPage.AddColumn("Postal Code", "ZipCode");
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
