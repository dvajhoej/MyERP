using TECHCOOL.UI;

namespace MyERP.ProductView
{
    public class ProductViewScreen : Screen
    {
        public Product ProductDisplay { get; set; }

        public ProductViewScreen(Product p)
        {
            product = new Product
            {
                ProductNumber = p.ProductNumber,
                Name = p.Name,
                Description = p.Description,
                SellingPrice = p.SellingPrice,
                PurchasePrice = p.PurchasePrice,
                Location = p.Location,
                QuantityInStock = p.QuantityInStock,
                Unit = p.Unit
            };
        }

        public override string Title { get; set; } = "Produkt Visning";
        public Product product { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Product> listPage = new();

            listPage.Add(product);

            listPage.AddColumn("Varenummer", "ProductNumber");
            listPage.AddColumn("Navn", "Name");
            listPage.AddColumn("Beskrivelse", "Description");
            listPage.AddColumn("Salgsspris", "SellingPrice");
            listPage.AddColumn("Indkøbspris", "PurchasePrice");
            listPage.AddColumn("Lokation", "Location");
            listPage.AddColumn("Antal på lager", "QuantityInStock");
            listPage.AddColumn("Enhed", "Unit");
            listPage.AddColumn("Avance (%)", "MarginPercentage");
            listPage.AddColumn("Avance (kr)", "Profit");

            listPage.Select();

            var selected = listPage.Select();
            if (selected != null)
            {
            }
            else
            {
                Quit();
            }
        }
    }
}
