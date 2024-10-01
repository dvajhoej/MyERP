using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyListScreen : Screen
    {
        private ListPage<Company> listPage;

        public CompanyListScreen()
        {
            listPage = new ListPage<Company>(Database.Instance.Companies);
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


            int spaces = 40;

            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at oprette en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at redigere en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at slette en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk Esc for at forlade siden");

            WindowHelper.Bot(spaces);

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine();
            }


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
            Screen.Display(new CompanyCreateScreen(newCompany));

            try
            {
                Database.Instance.InsertCompany(newCompany);
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{newCompany.CompanyName} oprettet");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl under oprettelse af virksomhed: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
                Console.ReadKey();
            }        
        }

        private void EditCompany(Company selected)
        {
            Screen.Display(new CompanyEditScreen(selected));

            try
            {
                Database.Instance.UpdateCompany(selected);
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{selected.CompanyName} opdateret");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fejl under redigering af virksomhed: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
                Console.ReadKey();
            }
       

        }

        public void DeleteCompany(Company selected)
        {
            if (selected != null)
            {
                try
                {
                    Database.Instance.DeleteCompanyById(selected.CompanyID);
                    
                    int spaces = 40;
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-40}│", $"{selected.CompanyName} slettet");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fejl Under sletning af virksomhed: {ex.Message} ");
                    Console.WriteLine("Tryk på en tast for at fortsætte");
                    Console.ReadKey();
                }         

            }
            else
            {
                Console.WriteLine("Ingen virksomhed valgt");
            }
        }
    }
}
