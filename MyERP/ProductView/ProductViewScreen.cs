using MyERP.CustomerView;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    public class ProductViewScreen : Screen
    {
        public Product ProductDisplay { get; set; }

        public ProductViewScreen(Product p)
        {
            product = p;
            ExitOnEscape();
        }
        public override string Title { get; set; } = "Produkt Visning for";

        public Product product { get; private set; }

        protected override void Draw()
        {
            Clear();


            Console.WriteLine($"Varenummer:        {product.ProductNumber}");
            Console.WriteLine($"Navn:              {product.Name}");
            Console.WriteLine($"Beskrivelse:       {product.Description}");
            Console.WriteLine($"Salgspris:         {product.SellingPrice}");
            Console.WriteLine($"Indkøbspris:       {product.PurchasePrice}");
            Console.WriteLine($"Lokation:          {product.Location}");
            Console.WriteLine($"Antal på lager:    {product.QuantityInStock}");
            Console.WriteLine($"Enhed:             {product.Unit}");
            Console.WriteLine($"Avance (%):        {product.MarginPercentage}");
            Console.WriteLine($"Avance (Kr):       {product.Profit}");

        }
    }
}
