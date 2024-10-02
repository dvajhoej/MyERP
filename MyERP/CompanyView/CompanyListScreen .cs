using TECHCOOL.UI;

namespace MyERP.CompanyView
{
    // Define a class CompanyListScreen that inherits from Screen
    public class CompanyListScreen : Screen
    {
        // Private field to store the ListPage object
        private ListPage<Company> listPage;

        // Constructor that initializes the ListPage object
        public CompanyListScreen()
        {
            // Create a new ListPage object with the list of companies from the database
            listPage = new ListPage<Company>(Database.Instance.Companies);

            // Add keyboard shortcuts to the ListPage object
            listPage.AddKey(ConsoleKey.F1, CreateCompany); // Create a new company
            listPage.AddKey(ConsoleKey.F2, EditCompany); // Edit a company
            listPage.AddKey(ConsoleKey.F5, DeleteCompany); // Delete a company
            listPage.AddKey(ConsoleKey.Escape, Quit); // Quit the screen

            // Add columns to the ListPage object
            listPage.AddColumn("Company Name", "CompanyName", 25); // Company name column
            listPage.AddColumn("Country", "Country"); // Country column
            listPage.AddColumn("Currency", "Currency"); // Currency column
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Virksomhed";

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Calculate the number of spaces for the window border
            int spaces = 40;

            // Draw the top border of the window
            WindowHelper.Top(spaces);

            // Display instructions for the user
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at oprette en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at redigere en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk F1  for at slette en virksomhed");
            Console.WriteLine("│{0,-40}│", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Bot(spaces);

<<<<<<< HEAD
            // Add some empty lines for spacing
            for (int i = 0; i < 3; i++)
=======
            for (int i = 0; i < 4; i++)
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            {
                Console.WriteLine();
            }

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                // Display the CompanyViewScreen for the selected company
                Screen.Display(new CompanyViewScreen(selected));
            }
        }

        // Method to quit the screen
        void Quit(Company _)
        {
<<<<<<< HEAD
            // Quit the screen
=======

>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            Quit();
        }

        // Method to create a new company
        private void CreateCompany(Company company)
        {
            // Create a new Company object
            var newCompany = new Company();

            // Display the CompanyCreateScreen to create the new company
            Screen.Display(new CompanyCreateScreen(newCompany));

            try
            {
                // Insert the new company into the database
                Database.Instance.InsertCompany(newCompany);

                // Display a success message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{newCompany.CompanyName} oprettet");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                // Display an error message to the user
                Console.WriteLine("Fejl under oprettelse af virksomhed: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
=======
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under oprettelse af virksomhed: " +  WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                Console.ReadKey();
            }
        }

        // Method to edit a company
        private void EditCompany(Company selected)
        {
            // Display the CompanyEditScreen to edit the company
            Screen.Display(new CompanyEditScreen(selected));

            try
            {
                // Update the company in the database
                Database.Instance.UpdateCompany(selected);

                // Display a success message to the user
                int spaces = 40;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-40}│", $"{selected.CompanyName} redigeret");
                Console.WriteLine("│{0,-40}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
<<<<<<< HEAD
                // Display an error message to the user
                Console.WriteLine("Fejl under redigering af virksomhed: " + ex.Message);
                Console.WriteLine("Tryk på en tast for at fortsætte");
                Console.ReadKey();
            }
=======
                int spaces = 120;
                WindowHelper.Top(spaces);
                Console.WriteLine("│{0,-120}│", $"Fejl under redigering af virksomhed: " + WindowHelper.Truncate(ex.Message, 70));
                Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }


>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
        }

        // Method to delete a company
        public void DeleteCompany(Company selected)
        {
            if (selected != null)
            {
                try
                {
                    // Delete the company from the database
                    Database.Instance.DeleteCompanyById(selected.CompanyID);

<<<<<<< HEAD
                    // Display a success message to the user
=======
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
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
<<<<<<< HEAD
                    // Display an error message to the user
                    Console.WriteLine($"Fejl Under sletning af virksomhed: {ex.Message} ");
                    Console.WriteLine("Tryk på en tast for at fortsætte");
                    Console.ReadKey();
                }
            }
            else
            {
                // Display a message to the user if no company is selected
                Console.WriteLine("Ingen virksomhed valgt");
=======
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

>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            }
        }
    }
}