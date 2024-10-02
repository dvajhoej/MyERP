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

            for (int i = 0; i < 4; i++)
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
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under oprettelse af virksomhed: " +  WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
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
                Console.WriteLine("│{0,-40}│", $"{selected.CompanyName} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under redigering af virksomhed: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
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
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-40}│", $"{selected.CompanyName} slettet");
                    Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    int spaces = 120;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af virksomhed:{WindowHelper.Truncate(ex.Message, 70)}");
                    Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }

            }
            else
            {
                int spaces = 60;
                Console.SetCursorPosition(0, 7);
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-60}│", "Ingen virksomhed valgt");
                Console.WriteLine("│{0,-60}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();

            }
        }
    }
}
