using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesListScreen : Screen
    {
        private ListPage<SalesOrderHeader> listPage;

        public SalesListScreen()
        {
            listPage = new ListPage<SalesOrderHeader>(Database.Instance.SalesOrderHeaders);
            listPage.AddKey(ConsoleKey.F1, CreateOrder);
            listPage.AddKey(ConsoleKey.F2, EditOrder);
            listPage.AddKey(ConsoleKey.F5, DeleteOrder);
            listPage.AddKey(ConsoleKey.F9, PrintInvoice);
            listPage.AddKey(ConsoleKey.Escape, Quit);
            listPage.AddColumn("Ordre nummer", "OrderNumber", 25);
            listPage.AddColumn("Oprettelse", "CreationDate", 25);
            listPage.AddColumn("Færdig", "CompletionDate", 25);
            listPage.AddColumn("Kunde nummer", "CustomerNumber", 25);
            listPage.AddColumn("Kunde Navn", "Fullname", 25);
            listPage.AddColumn("Status", "Status", 25);


        }



        public override string Title { get; set; } = "Ordre";

        protected override void Draw()
        {
            Clear();
            int spaces = 35;

            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette en ordre");
            Console.WriteLine("│{0,-35}│", "Tryk F9 for at oprette  en faktura");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");
            WindowHelper.Bot(spaces);

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }



            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new SalesViewScreen(selected));
            }


        }
        void Quit(SalesOrderHeader _)
        {
            Quit();
        }
        private void CreateOrder(SalesOrderHeader order)
        {
            var newOrder = new SalesOrderHeader();
            Screen.Display(new SalesCreateScreen(newOrder));

            try
            {
                listPage.Add(newOrder);

                Console.WriteLine("Order successfully created.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadLine();
            }
        }
        public void PrintInvoice(SalesOrderHeader selected)
        {
            try
            {
                var newInvoice = new Invoice();
                Database.Instance.InsertInvoice(selected, newInvoice);
                Invoice.GenerateInvoice(selected, newInvoice);
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0,8);
                WindowHelper.Top(70);
                Console.WriteLine("│{0,-70}│", $"Der er sket en fejl under oprettelse af faktura : {ex.Message}");
                WindowHelper.Bot(70);
                Console.ReadLine();
            }



        }



        private void EditOrder(SalesOrderHeader selected)
        {
            Screen.Display(new SalesEditScreen(selected));


        }

        private void DeleteOrder(SalesOrderHeader selected)
        {
            if (selected != null)
            {
                try
                {
                    Database.Instance.DeleteSalesOrderHeadByID(selected.OrderNumber);
                    listPage.Remove(selected);

                    int spaces = 70;
                    Console.SetCursorPosition(0, 8);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-70}│", $"Ordre nr {selected.OrderNumber} er blevet slettet.");
                    Console.WriteLine("│{0,-70}│", "Tryk på en tast for at fortsætte");

                    WindowHelper.Bot(spaces);
                    Console.ReadLine();


                    //Console.WriteLine($"Ordre nr '{selected.OrderNumber}' er blevet slettet.");
                }
                catch (Exception ex)
                {

                    int spaces = 70;
                    Console.SetCursorPosition(0, 8);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af ordre:{WindowHelper.Truncate(ex.Message, 70)}");
                    Console.WriteLine("│{0,-120}│", "Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadLine();
                }

            }
        }
    }
}
