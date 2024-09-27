using System;
using System.Collections.Generic;
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

            editor.IntBox("Ordre Nummer", "OrderNumber");
            editor.SelectBox("Order Status", "Status", GetStatusOptions());

            editor.Edit(_salesOrder);

            if (_salesOrder.Status == SalesOrderHeader.OrderStatus.Completed)
            {
                _salesOrder.CompletionDate = DateTime.Now;
            }

            EditOrderLines();
            Database.Instance.UpdateSalesOrderHeader(_salesOrder);

            this.Quit();
        }

        private Dictionary<string, object> GetStatusOptions()
        {
            return new Dictionary<string, object>
            {
                { "Confirmed", (object)SalesOrderHeader.OrderStatus.Confirmed },
                { "Packed", (object)SalesOrderHeader.OrderStatus.Packed },
                { "Completed", (object)SalesOrderHeader.OrderStatus.Completed }
            };
        }

        private void EditOrderLines()
        {
            var orderLines = _salesOrder.GetOrderLines();
            if (orderLines.Count == 0)
            {
                Console.WriteLine("Ingen ordrelinjer tilgængelige for redigering.");
                return;
            }

            while (true)
            {
                Console.WriteLine("Aktuelle ordrelinjer:");
                for (int i = 0; i < orderLines.Count; i++)
                {
                    Console.WriteLine($"{i + 1}: {orderLines[i].Name} - Mængde: {orderLines[i].Quantity}, Pris: {orderLines[i].Price}");
                }

                Console.WriteLine("Indtast nummeret på ordrelinjen du vil redigere, eller 0 for at afslutte:");
                if (!int.TryParse(Console.ReadLine(), out int lineIndex) || lineIndex < 0 || lineIndex > orderLines.Count)
                {
                    Console.WriteLine("Ugyldigt input. Prøv igen.");
                    continue;
                }

                if (lineIndex == 0) break;

                var orderLineToEdit = orderLines[lineIndex - 1];

                Form<SalesOrderLine> orderLineEditor = new Form<SalesOrderLine>();
                orderLineEditor.TextBox("Navn", "Name");
                orderLineEditor.DoubleBox("Mængde", "Quantity");
                orderLineEditor.DoubleBox("Pris", "Price");

                orderLineToEdit.Name = orderLineToEdit.Name;
                orderLineToEdit.Quantity = orderLineToEdit.Quantity;
                orderLineToEdit.Price = orderLineToEdit.Price;

                bool success = orderLineEditor.Edit(orderLineToEdit);
                if (success)
                {
                    _salesOrder.EditOrderLine(lineIndex - 1, orderLineToEdit);
                    Console.WriteLine("Ordrelinje opdateret.");
                }
                else
                {
                    Console.WriteLine("Redigering af ordrelinje annulleret.");
                }
            }
        }

    }
}
