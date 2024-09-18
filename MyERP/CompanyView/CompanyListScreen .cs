using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyListScreen : Screen
    {
        private ListPage<Company> listPage;

        public CompanyListScreen()
        {
            listPage = new ListPage<Company>();
            List<Company> companies = Database.Instance.GetAllCompanies();
            listPage.Add(companies);
            listPage.AddKey(ConsoleKey.F1, CreateCompany);
            listPage.AddKey(ConsoleKey.F2, EditCompany);
            listPage.AddKey(ConsoleKey.F5, DeleteCompany);
            listPage.AddKey(ConsoleKey.Escape, Quit);
            listPage.AddColumn("Company Name", "CompanyName", 25);
            listPage.AddColumn("Country", "Country");
            listPage.AddColumn("Currency", "Currency");
        }

        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Press F1 to create a company");
            Console.WriteLine("Press F2 to edit a company");
            Console.WriteLine("Press F5 to delete a company");
          

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new CompanyViewScreen(selected));
            }
       
        }
        void Quit(Company _)
        {
            Quit();
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
