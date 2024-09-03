using TECHCOOL.UI;

namespace MyERP.CompanyViews
{
    public class CompanyListScreen : Screen
    {
        private ListPage<Company> listPage;

        public CompanyListScreen()
        {
            listPage = new ListPage<Company>();

            InitializeData();
        }

        public override string Title { get; set; } = "Company";

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

            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new CompanyViewScreen(selected));
            }

            Quit();
        }

        private void InitializeData()
        {
            listPage.Add(new Company(0, "Danish Import", "Danmarksgade", 13, 9000, "Aalborg", "Denmark", Currency.DKK));
            listPage.Add(new Company(1, "Swedish Import", "Sverigesvej", 51, 58200, "Malmö", "Sweden", Currency.SEK));
            listPage.Add(new Company(2, "USA Import", "Casinoroad", 67, 1500, "Las Vegas", "USA", Currency.USD));
            listPage.Add(new Company(3, "EURO Import", "BerlinStrabe", 661, 6712, "Berlin", "Germany", Currency.EUR));
        }

        private void CreateCompany(Company company)
        {
            var newCompany = new Company();
            listPage.Add(newCompany);
            Screen.Display(new CompanyCreateView(newCompany));
        }

        private void EditCompany(Company selected)
        {
            Screen.Display(new CompanyEditView(selected));
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
