using TECHCOOL.UI;

namespace MyERP.productView
{
    public class ProductCreateScreen : Screen
    {
        public override string Title { get; set; } = "Opret produkt";

        private Product _product;

        public ProductCreateScreen(Product product)
        {
            _product = product;
        }

        protected override void Draw()
        {
            Clear();

            Form<Product> editor = new Form<Product>();

            editor.IntBox("Varenummer", "ProductNumber");
            editor.TextBox("Navn", "Name");
            editor.TextBox("Beskrivelse", "Description");
            editor.DoubleBox("Salgsspris", "SellingPrice");
            editor.DoubleBox("Indkøbspris", "PurchasePrice");
            editor.TextBox("Lokation", "Location");
            editor.DoubleBox("Antal på lager", "QuantityInStock");
            editor.SelectBox("Enhed", "Unit");

            editor.AddOption("Enhed", "Stk", UnitType.Stk);
            editor.AddOption("Enhed", "Pakker", UnitType.Pakker);
            editor.AddOption("Enhed", "Time", UnitType.Time);
            editor.AddOption("Enhed", "Meter", UnitType.Meter);

            editor.Edit(_product);



            //try
            //{
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error while creating product: " + ex.Message);
            //    Console.ReadLine();
            //}

            this.Quit();
        }
    }
}


