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
            EditOrderLines();
            Form<SalesOrderHeader> editor = new Form<SalesOrderHeader>();

            editor.IntBox("Ordre Nummer", "OrderNumber");
            editor.SelectBox("Order Status", "Status", GetStatusOptions());

            editor.Edit(_salesOrder);

            if (_salesOrder.Status == SalesOrderHeader.OrderStatus.Færdig)
            {
                _salesOrder.CompletionDate = DateTime.Now;
            }

            
            Database.Instance.UpdateSalesOrderHeader(_salesOrder);

            this.Quit();
        }

        private Dictionary<string, object> GetStatusOptions()
        {
            return new Dictionary<string, object>
            {
                { "Bekræftet", (object)SalesOrderHeader.OrderStatus.Bekræftet },
                { "Pakket", (object)SalesOrderHeader.OrderStatus.Pakket },
                { "Færdig", (object)SalesOrderHeader.OrderStatus.Færdig }
            };
        }

        private void EditOrderLines()
        {
            foreach (var orderLine in Database.Instance.SalesOrderLines)
            {
                if (orderLine.SalesOrderHeadID == _salesOrder.OrderNumber)
                {

                    Console.WriteLine($"Editing Order Line for: {orderLine.Name} - Current Quantity: {orderLine.Quantity}");

                    var productOptions = new Dictionary<string, object>();

                    foreach (var product in Database.Instance.Products)
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
                            Database.Instance.UpdateSalesOrderLines(orderLine);
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
            }
            Console.WriteLine($"Samlet beløb: {_salesOrder.OrderAmount} DKK");
        }

    }
}
