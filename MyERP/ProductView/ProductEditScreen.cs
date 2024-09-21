using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.ProductView
{
    public class ProductEditScreen : Screen
    {
        public override string Title { get; set; } = "Rediger kunde";

        private Product _product;

        public ProductEditScreen(Product product)
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
            editor.AddOption("Enhed", "Styk", UnitType.Stk);
            editor.AddOption("Enhed", "Pakke", UnitType.Pakker);
            editor.AddOption("Enhed", "time", UnitType.Time);
            editor.AddOption("Enhed", "Meter", UnitType.Meter);

            editor.Edit(_product);
            Database.Instance.UpdateProduct(_product);


            this.Quit();
        }
    }
}