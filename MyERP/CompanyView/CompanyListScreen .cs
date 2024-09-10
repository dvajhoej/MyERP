using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyListScreen : Screen
    {
        private ListPage<Company> listPage;

        public CompanyListScreen()
        {
            listPage = new ListPage<Company>();
            listPage.Add(new Company { CompanyName = "Danish Import", City = "Aalborg", HouseNumber = 13, ZipCode = 9000, Country = "Denmark", Street = "Danmarksgade", Currency = Currency.DKK, ID = 0 });
            listPage.Add(new Company { CompanyName = "Swedish Import", City = "Malmö", HouseNumber = 51, ZipCode = 58200, Country = "Sweden", Street = "Sverigesvej", Currency = Currency.SEK, ID = 0 });
            listPage.Add(new Company { CompanyName = "USA Import", City = "Las Vegas", HouseNumber = 67, ZipCode = 1500, Country = "Nevada", Street = "Casinoroad", Currency = Currency.USD, ID = 0 });
            listPage.Add(new Company { CompanyName = "EURO Import", City = "Berlin", HouseNumber = 661, ZipCode = 6712, Country = "Germany", Street = "BerlinStrabe", Currency = Currency.EUR, ID = 0 });
        }

        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Press F1 to create a company");
            Console.WriteLine("Press F2 to edit a company");
            Console.WriteLine("Press F5 to delete a company");
            listPage.AddKey(ConsoleKey.F1, CreateCompany);
            listPage.AddKey(ConsoleKey.F2, EditCompany);
            listPage.AddKey(ConsoleKey.F5, DeleteCompany);

            listPage.AddColumn("Company Name", "CompanyName");
            listPage.AddColumn("Country", "Country");
            listPage.AddColumn("Currency", "Currency");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new CompanyViewScreen(selected));
            }
            else
            {
                
                Quit();
            }
        }

        private void CreateCompany(Company company)
        {
            var newCompany = new Company();
            listPage.Add(newCompany);
            Screen.Display(new CompanyCreateScreen(newCompany));

            //if (newCompany.CompanyName != null)
            //{
            //    listPage.Add(newCompany);
            //}

            // AFVENTER IMPLEMENTERING
        }

        private void EditCompany(Company selected)
        {
            Screen.Display(new CompanyEditScreen(selected));
        }

        public void DeleteCompany(Company selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Company '{selected.CompanyName}' has been deleted.");
            }
        }
    }
}
