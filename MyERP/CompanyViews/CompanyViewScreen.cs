using TECHCOOL.UI;

namespace MyERP.CompanyViews
{
    public class CompanyViewScreen : Screen
    {
        public Company CompanyView { get; set; }

        public CompanyViewScreen(Company c)
        {
            Company = new Company
            {
                CompanyName = c.CompanyName,
                Country = c.Country,
                Currency = c.Currency,
                Street = c.Street,
                HouseNumber = c.HouseNumber,
                PostalCode = c.PostalCode,
                City = c.City
            };
        }

        public override string Title { get; set; } = "Company View";
        public Company Company { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Company> listPage = new();

            listPage.Add(Company);

            listPage.AddColumn("Company Name", "Company Name");
            listPage.AddColumn("Street", "Street");
            listPage.AddColumn("House Number", "House Number");
            listPage.AddColumn("Postal Code", "Postal Code");
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
