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
        public override string Title { get; set; } = "Produkt Visning";

        public Product product { get; private set; }

        protected override void Draw()
        {

            int space = 54;
            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-53} │", "Tryk Esc for at forlade siden");
            WindowHelper.Spacer('└', '─', space, '┘');
            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Varenummer", product.ProductNumber);
            WindowHelper.Spacer('│', '─', space, '│');
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Navn", WindowHelper.Truncate(product.Name, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Beskrivelse", WindowHelper.Truncate(product.Description, 35));
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Salgspris", product.SellingPrice);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Indkøbspris", product.PurchasePrice);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Lokation", product.Location);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Antal på lager", product.QuantityInStock);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Enhed", product.Unit);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Avance (%)", product.MarginPercentage);
            Console.WriteLine("│{0,-15} │ {1,-35} │", "Avance (Kr)", product.Profit);
            WindowHelper.Spacer('└', '─', space, '┘');




        }
    }
}
