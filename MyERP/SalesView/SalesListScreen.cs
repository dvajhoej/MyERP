using MyERP.productView;
using MyERP.ProductView;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesListScreen : Screen
    {
        private ListPage<SalesOrderHeader> listPage;

        public SalesListScreen()
        {
            listPage = new ListPage<SalesOrderHeader>();
            listPage.Add(Database.Instance.Sales);

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

        }

     

        public override string Title { get; set; } = "Ordre";

        protected override void Draw()
        {
            Clear();
            Console.WriteLine("Press F1 to create an order");
            Console.WriteLine("Press F2 to edit an order");
            Console.WriteLine("Press F5 to delete an order");
            Console.WriteLine("Press F9 to to generata invoice");


            //listPage.AddColumn("Status", "Status", 25);

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
            var newInvoice = new Invoice();
            Database.instance.InsertInvoice(selected, newInvoice);
            Invoice.GenerateInvoice(selected, newInvoice);
        }
        private void EditOrder(SalesOrderHeader selected)
        {
            Screen.Display(new SalesEditScreen(selected));
        }

        private void DeleteOrder(SalesOrderHeader selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Ordre nr '{selected.OrderNumber}' er blevet slettet.");
            }
        }
    }
}
