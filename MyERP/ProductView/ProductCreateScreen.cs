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
            editor.AddOption("Enhed", "Pakker", UnitType.Pakke);
            editor.AddOption("Enhed", "time", UnitType.Meter);
            editor.AddOption("Enhed", "Meter", UnitType.Time);
          
            editor.Edit(_product);

            this.Quit();
        }
    }
}


//listPage.AddColumn("Kunde nummer", "Kundenummer");
//listPage.AddColumn("Navn", "FuldtNavn");
//listPage.AddColumn("Telefon", "Telefon");
//listPage.AddColumn("Email", "Email");

//listPage.AddColumn("Fornavn", "Fornavn");
//listPage.AddColumn("Efternavn", "Efternavn");
//listPage.AddColumn("Vej", "Streetname");
//listPage.AddColumn("Husnummer", "ZipCode");
//listPage.AddColumn("By", "City");
//listPage.AddColumn("Land", "Country");
//listPage.AddColumn("Email", "Email");
//listPage.AddColumn("Telefon", "Telefon");
