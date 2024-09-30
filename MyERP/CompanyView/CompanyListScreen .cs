using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    public class CompanyListScreen : Screen
    {
        private ListPage<Company> listPage;

        public CompanyListScreen()
        {
            //listPage = new ListPage<Company>(Database.Instance.Companies);

            listPage = new ListPage<Company>();
            listPage.Add(Database.Instance.Companies);
            listPage.AddKey(ConsoleKey.F1, CreateCompany);
            listPage.AddKey(ConsoleKey.F2, EditCompany);
            listPage.AddKey(ConsoleKey.F3, SearchCompany);
            listPage.AddKey(ConsoleKey.F5, DeleteCompany);
            listPage.AddKey(ConsoleKey.Escape, Quit);
            listPage.AddColumn("Company Name", "CompanyName", 25);
            listPage.AddColumn("Country", "Country");
            listPage.AddColumn("Currency", "Currency");
        }

        //private void SearchCompany(Company company)
        //{
        //    Screen.Display(new CompanyFilterListScreen());
        //}

        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {

            Console.WriteLine("Press F1 to create a company");
            Console.WriteLine("Press F2 to edit a company");
            Console.WriteLine("Press F3 to search for company");
            Console.WriteLine("Press F5 to delete a company");
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
            //listPage.Clear();
            //listPage.Add(Database.Instance.Companies);

            Quit();
        }


        private void SearchCompany(Company company)
        {
            string search;
            do
            {
                CleanLine06andselect();

                Console.Write("Enter Company name or ID: ");
                search = Console.ReadLine().ToLower();
            } while (string.IsNullOrEmpty(search));

            var filtered = Database.Instance.Companies
                .Where(c => c.CompanyName.ToLower().Contains(search) ||
                            c.CompanyID.ToString().Contains(search))
                .ToList();

            if (filtered.Any())
            {
                Clear();
                listPage.Clear();
                listPage.Add(filtered);
                //Draw();

                var selected = listPage.Select();
                if (selected != null)
                {
                    Screen.Display(new CompanyViewScreen(selected));
                }
                else
                {
             

                }

            }
            else
            {
                CleanLine06andselect();
                Console.WriteLine("No matching companies found.");
                Console.WriteLine("Press a key to continue.");

                Console.ReadKey();
            }
        }

        public void CleanLine06andselect()
        {
            Console.SetCursorPosition(0, 6);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, 6);
        }




        private void CreateCompany(Company company)
        {
            var newCompany = new Company();
            listPage.Add(newCompany);
            Screen.Display(new CompanyCreateScreen(newCompany));
        }

        private void EditCompany(Company selected)
        {
            Screen.Display(new CompanyEditScreen(selected));
        }

        public void DeleteCompany(Company selected)
        {
            if (selected != null)
            {
                try
                {
                    Database.Instance.DeleteCompanyById(selected.CompanyID);
                    listPage.Remove(selected);
                    //Database.Instance.Companies.Remove(selected);
                    //listPage.Clear();
                    //listPage.Add(Database.Instance.Companies);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error has occured: {ex.Message} ");
                }
                finally
                {
                    Console.SetCursorPosition(0, 5);
                    Console.WriteLine($"Company '{selected.CompanyName}' has been deleted.");
                    Console.WriteLine("Press any key.");

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
