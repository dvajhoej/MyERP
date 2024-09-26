using System;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesEditScreen : Screen
    {
        public override string Title { get; set; } = "Rediger Salg";

        private SalesOrderHeader _salesOrder;

        public SalesEditScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Clear();

            Form<SalesOrderHeader> editor = new Form<SalesOrderHeader>();

            editor.IntBox("Ordre Nummer", "OrderNumber");
            editor.IntBox("Kunde Nummer", "CustomerNumber");
            editor.TextBox("Fornavn", "Firstname");
            editor.TextBox("Efternavn", "Lastname");
            editor.DoubleBox("Total Pris", "TotalPrice");

            editor.Edit(_salesOrder);
            Database.Instance.UpdateSalesOrderHeader(_salesOrder);

            //this.Quit();
        }
    }
}
