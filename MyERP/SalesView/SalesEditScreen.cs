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
            foreach (var orderLine in _salesOrder.GetOrderLines())
            {
                Console.WriteLine($"Editing Order Line for: {orderLine.Name} - Current Quantity: {orderLine.Quantity}");

                List<Product> products = Database.Instance.Products;
                var productOptions = new Dictionary<string, object>();

                foreach (var product in products)
                {
                    string displayName = $"{product.Name} (ID: {product.ProductID})";
                    if (!productOptions.ContainsKey(displayName))
                    {
                        productOptions.Add(displayName, product.ProductID);
                    }
                }

                Form<SalesOrderLine> orderLineForm = new Form<SalesOrderLine>();

                orderLineForm.SelectBox("Vælg nyt produkt", "ProductID", productOptions);

                orderLineForm.DoubleBox("Antal", "Quantity");

                bool success = orderLineForm.Edit(orderLine);

                if (success)
                {
                    int selectedProductID = orderLine.ProductID;

                    var selectedProduct = Database.Instance.GetProductById(selectedProductID);
                    if (selectedProduct != null)
                    {
                        orderLine.ProductID = selectedProductID;
                        orderLine.Name = selectedProduct.Name;
                        orderLine.Price = selectedProduct.SellingPrice;
                        Console.WriteLine($"Order line updated to: {orderLine.Quantity} x {orderLine.Name}");
                    }
                    else
                    {
                        Console.WriteLine("Invalid product selection.");
                    }
                }
                else
                {
                    Console.WriteLine("Order line edit cancelled.");
                }
            }

            Console.WriteLine($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }

    }
}
