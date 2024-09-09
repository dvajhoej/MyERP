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

            // Creating Salgsordrehoved objects with associated Salgsordrelinje objects
            var order1 = new SalesOrderHeader
            {
                OrderNumber = 0,
                CreationDate = new DateTime(2024, 9, 1, 10, 30, 0),
                CompletionDate = new DateTime(2024, 9, 3, 15, 45, 0),
                CustomerNumber = 123,
                Status = OrderStatus.Completed
            };

            // Adding Salgsordrelinje (order lines) to the order
            order1.AddOrderLine(new SalesOrderLine
            {
                ProductNumber = 101,
                Name = "Product A",
                Quantity = 2,
                Price = 100 // Price per item
            });

            order1.AddOrderLine(new SalesOrderLine
            {
                ProductNumber = 102,
                Name = "Product B",
                Quantity = 1,
                Price = 50,
                
            });

            listPage.Add(order1);

            var order2 = new SalesOrderHeader
            {
                OrderNumber = 1,
                CreationDate = new DateTime(2024, 9, 1, 10, 30, 0),
                CompletionDate = new DateTime(2024, 9, 3, 15, 45, 0),
                CustomerNumber = 451,
                Status = OrderStatus.Completed
            };

            order2.AddOrderLine(new SalesOrderLine
            {
                ProductNumber = 103,
                Name = "Product C",
                Quantity = 3,
                Price = 200
            });

            listPage.Add(order2);
        }

        public override string Title { get; set; } = "Ordre";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Press F1 to create an order");
            Console.WriteLine("Press F2 to edit an order");
            Console.WriteLine("Press F5 to delete an order");

            //listPage.AddKey(ConsoleKey.F1, CreateOrder);
            //listPage.AddKey(ConsoleKey.F2, EditOrder);
            //listPage.AddKey(ConsoleKey.F5, DeleteOrder);

            listPage.AddColumn("Ordre nummer", "OrderNumber", 25);
            listPage.AddColumn("Oprettelse", "CreationDate", 25);
            listPage.AddColumn("Færdig", "CompletionDate", 25);
            listPage.AddColumn("Kunde nummer", "CustomerNumber", 25);
            listPage.AddColumn("Kunde Navn", "Fullname", 25);
            //listPage.AddColumn("Status", "Status", 25);

            // Display the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                //Screen.Display(new OrderViewScreen(selected));
            }

            Quit();
        }

        private void CreateProduct(SalesOrderHeader order)
        {
            var newOrder = new SalesOrderHeader();
            listPage.Add(newOrder);
            //Screen.Display(new OrderCreateScreen(newOrder));
        }

        private void EditProduct(SalesOrderHeader selected)
        {
            //Screen.Display(new OrdertEditScreen(selected));
        }

        private void DeleteProduct(SalesOrderHeader selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Ordre nr '{selected.OrderNumber}' er blevet slettet.");
            }
        }
    }
}
