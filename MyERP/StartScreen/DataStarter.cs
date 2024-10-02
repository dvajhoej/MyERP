namespace MyERP.StartScreen
{
    // Define a class DataStarter to start the data loading process
    class DataStarter
    {
        // Public static method to start the data loading process
        public static void DataStart()
        {
            // Display a loader animation to the user
            WindowHelper.Loader();

            // Wait for the user to press a key
            Console.ReadKey();

            // Initialize a variable to control the retry loop
            bool retry = true;

            // Loop until the data loading process is successful or the user cancels
            while (retry)
            {
                try
                {
                    // Get the data from the database
                    WindowHelper.getdata();

                    // Load the invoices from the database
                    Database.Instance.GetAllInvoices();

                    // Load the customers from the database
                    Database.Instance.GetAllCustomers();

                    // Load the companies from the database
                    Database.Instance.GetAllCompanies();

                    // Load the products from the database
                    Database.Instance.GetAllProducts();

                    // Load the sales order headers from the database
                    Database.Instance.GetAllSalesOrderHeaders();

                    // Load the sales order lines from the database
                    Database.Instance.GetAllSalesOrderLines();

                    // Set the retry variable to false to exit the loop
                    retry = false;
                }
                catch
                {
                    // Display an error message to the user
                    int spaces = 70;
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-70}│", "Der kan ikke oprettes forbindelse til database");
                    Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte eller 'ESC' for at afslutte.");
                    WindowHelper.Bot(spaces);

                    // Get the key pressed by the user
                    var key = Console.ReadKey(true).Key;

                    // Check if the user pressed the Escape key
                    if (key == ConsoleKey.Escape)
                    {
                        // Set the retry variable to false to exit the loop
                        retry = false;
                    }
                }
            }
        }
    }
}