namespace MyERP.StartScreen
{
    class DataStarter
    {

        public static void DataStart()
        {

            WindowHelper.Loader();
            Console.ReadKey();
            bool retry = true;
            while (retry)
            {

                try
                {
                    

                    WindowHelper.getdata();

                    Database.Instance.GetAllInvoices();
                    Database.Instance.GetAllCustomers();
                    Database.Instance.GetAllCompanies();
                    Database.Instance.GetAllProducts();
                    Database.Instance.GetAllSalesOrderHeaders();
                    Database.Instance.GetAllSalesOrderLines();

                    retry = false;
                }
                catch
                {
                    int spaces = 70;
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-70}│", "Der kan ikke oprettes forbindelse til database");
                    Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte eller 'ESC' for at afslutte.");
                    WindowHelper.Bot(spaces);

                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.Escape)
                    {
                        retry = false;
                    }
                }
            }
        }
    }
}

