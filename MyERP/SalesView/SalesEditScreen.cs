using System;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesEditScreen : Screen
    {
        public override string Title { get; set; } = "Rediger Order";

        private SalesOrderHeader _salesOrder;

        public SalesEditScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
        }

        protected override void Draw()
        {
            Clear();

            Form<SalesOrderHeader> editor = new Form<SalesOrderHeader>();

            Dictionary<string, object> statusOptions = new Dictionary<string, object>
            {
                { "Confirmed", (object)SalesOrderHeader.OrderStatus.Confirmed },
                { "Packed", (object)SalesOrderHeader.OrderStatus.Packed },
                { "Completed", (object)SalesOrderHeader.OrderStatus.Completed }
            };

            editor.IntBox("Ordre Nummer", "OrderNumber");
            editor.SelectBox("Order Status", "Status", statusOptions);
            editor.TextBox("Completion Date (yyyy-MM-dd)", "CompletionDate");

            editor.Edit(_salesOrder);
            Database.Instance.UpdateSalesOrderHeader(_salesOrder);

            //this.Quit();
        }
    }
}
