using MyERP.productView;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    public class ProductListScreen : Screen
    {
        private ListPage<Product> listPage;

        public ProductListScreen()
        {
            listPage = new ListPage<Product>();
            listPage.Add(Database.Instance.Products);
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

            Console.WriteLine("Tryk F1 for at skabe et produkt");
            Console.WriteLine("Tryk F2 for at tilpasse et produkt");
            Console.WriteLine("Tryk F5 for at slette et produkt");




            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new ProductViewScreen(selected));
            }

        }
        void Quit(Product _)
        {
            Quit();
        }
        private void CreateProduct(Product product)
        {
            var newProduct = new Product();
            listPage.Add(newProduct) ;
            Screen.Display(new ProductCreateScreen(newProduct));
        }

        private void EditProduct(Product selected)
        {
            Screen.Display(new ProductEditScreen(selected));
        }

        private void DeleteProduct(Product selected)
        {
            if (selected != null)
            {
                Database.Instance.DeleteProductById(selected.ProductID);
                listPage.Remove(selected);
                Console.WriteLine($"Produktet '{selected.Name}' er blevet slettet.");
            }
        }
    }
}
