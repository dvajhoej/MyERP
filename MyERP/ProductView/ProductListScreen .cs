using TECHCOOL.UI;
using System;

namespace MyERP.ProductView
{
    public class ProductListScreen : Screen
    {
        private ListPage<Product> listPage;

        public ProductListScreen()
        {
            listPage = new ListPage<Product>();

            listPage.Add(new Product { ProductNumber = 01, Name = "Reb", Description = "Et langt reb", SellingPrice = 100.00m, PurchasePrice = 99.00m, Location = "223A", QuantityInStock = 55, Unit = UnitType.Meter });
            listPage.Add(new Product { ProductNumber = 02, Name = "Dæk", Description = "Et lækkert dæk", SellingPrice = 100.00m, PurchasePrice = 30.00m, Location = "283E", QuantityInStock = 10, Unit = UnitType.Styk });
            listPage.Add(new Product { ProductNumber = 03, Name = "Dukke", Description = "En dukke for piger", SellingPrice = 100.00m, PurchasePrice = 80.00m, Location = "648N", QuantityInStock = 20, Unit = UnitType.Styk });
            listPage.Add(new Product { ProductNumber = 04, Name = "Gummiand", Description = "Den bedste ting i verden", SellingPrice = 200.00m, PurchasePrice = 175.00m, Location = "42AA", QuantityInStock = 1, Unit = UnitType.Styk });
        }

        public override string Title { get; set; } = "Produkter";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at skabe et produkt");
            Console.WriteLine("Tryk F2 for at tilpasse et produkt");
            Console.WriteLine("Tryk F5 for at slette et produkt");

            //listPage.AddKey(ConsoleKey.F1, CreateProduct);
            //listPage.AddKey(ConsoleKey.F2, EditProduct);
            listPage.AddKey(ConsoleKey.F5, DeleteProduct);

            listPage.AddColumn("Varenummer", "ProductNumber");
            listPage.AddColumn("Navn", "Name");
            listPage.AddColumn("Antal På Lager", "QuantityInStock");
            listPage.AddColumn("Indkøbspris", "PurchasePrice");
            listPage.AddColumn("Salgspris", "SellingPrice");
            listPage.AddColumn("Avance (%)", "MarginPercentage");


            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new ProductViewScreen(selected));
            }
            else
            {
                Quit();
            }
        }

        //private void CreateProduct(Product product)
        //{
        //    var newProduct = new Product();
        //    listPage.Add(newProduct);
        //    Screen.Display(new ProductCreateScreen(newProduct));
        //}

        //private void EditProduct(Product selected)
        //{
        //    Screen.Display(new ProductEditScreen(selected));
        //}

        private void DeleteProduct(Product selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Produktet '{selected.Name}' er blevet slettet.");
            }
        }
    }
}
