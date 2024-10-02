using MyERP.productView;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    public class ProductListScreen : Screen
    {
        private ListPage<Product> listPage;

        public ProductListScreen()
        {
            listPage = new ListPage<Product>(Database.Instance.Products);
            listPage.AddKey(ConsoleKey.F1, CreateProduct);
            listPage.AddKey(ConsoleKey.F2, EditProduct);
            listPage.AddKey(ConsoleKey.F5, DeleteProduct);
            listPage.AddKey(ConsoleKey.Escape, Quit);

            listPage.AddColumn("Varenummer", "ProductNumber");
            listPage.AddColumn("Navn", "Name");
            listPage.AddColumn("Antal På Lager", "QuantityInStock");
            listPage.AddColumn("Enhed", "Unit");
            listPage.AddColumn("Indkøbspris", "PurchasePrice");
            listPage.AddColumn("Salgspris", "SellingPrice");
            listPage.AddColumn("Avance (%)", "MarginPercentage");
        }

        public override string Title { get; set; } = "Produkter";

        protected override void Draw()
        {
            Clear();

            int spaces = 35;
            WindowHelper.Top(spaces);
            Console.WriteLine("│{0,-35}│", "Tryk F1 for at oprette  en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk F2 for at redigere en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk F5 for at slette   en produkt");
            Console.WriteLine("│{0,-35}│", "Tryk Esc for at forlade siden");

            WindowHelper.Bot(spaces);

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();
            }




            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new ProductViewScreen(selected));
            }
            else { }

        }
        void Quit(Product _)
        {
            Quit();
        }
        private void CreateProduct(Product product)
        {
            var newProduct = new Product();
            Screen.Display(new ProductCreateScreen(newProduct));

            try
            {
                Database.Instance.InsertProduct(newProduct);
                listPage.Add(newProduct);
                Console.WriteLine("Product successfully created.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it or show an error message)
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.ReadLine();

            }
        }


        private void EditProduct(Product selected)
        {
            Screen.Display(new ProductEditScreen(selected));
        }

        private void DeleteProduct(Product selected)
        {
            if (selected != null)
            {
                try
                {
                    Database.Instance.DeleteCustomerByID(selected.ProductID);

                    int spaces = 40;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-40}│", $"{selected.Name} slettet");
                    Console.WriteLine("│{0,-40}│", $"Tryk på en tast for at fortsætte");
                    WindowHelper.Bot(spaces);
                    Console.ReadKey();
                }
                catch (Exception ex)
                {
                    int spaces = 120;
                    Console.SetCursorPosition(0, 7);
                    WindowHelper.Top(spaces);
                    Console.WriteLine("│{0,-120}│", $"Fejl Under sletning af produkt:{WindowHelper.Truncate(ex.Message, 70)}");
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
                Console.WriteLine("│{0,-60}│", "Ingen produkt valgt");
                Console.WriteLine("│{0,-60}│", "Tryk på en tast for at fortsætte");
                WindowHelper.Bot(spaces);
                Console.ReadKey();
            }
        }
    }
}
