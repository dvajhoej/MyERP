using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesViewScreen : Screen
    {
        public SalesOrderHeader SalesOrderDisplay { get; set; }

        public SalesViewScreen(SalesOrderHeader soh)
        {
            salesOrderHeader = soh;
            ExitOnEscape();
        }

        public override string Title { get; set; } = "Ordre Visning";
        public SalesOrderHeader salesOrderHeader { get; set; }

        protected override void Draw()
        {

            Console.WriteLine($"Ordre nummer:   {salesOrderHeader.OrderNumber}");
            Console.WriteLine($"Oprettelse:     {salesOrderHeader.CreationDate}");
            Console.WriteLine($"Færdig:         {salesOrderHeader.CompletionDate}");
            Console.WriteLine($"Kunde nummer:   {salesOrderHeader.CustomerNumber}");
            Console.WriteLine($"Kunde navn:     {salesOrderHeader.Fullname}");
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("Ordre Linjer:");
            Console.WriteLine("----------------------------------------");

            foreach (var orderLine in salesOrderHeader.GetOrderLines())
            {
                Console.WriteLine($"Produkt Navn:    {orderLine.Name}");
                Console.WriteLine($"Antal:           {orderLine.Quantity}");
                Console.WriteLine($"Pris pr. enhed:  {orderLine.Price:C}");
                Console.WriteLine($"Linje pris:      {orderLine.Amount:C}");
                Console.WriteLine("----------------------------------------");
            }

            Console.WriteLine("========================================");
            Console.WriteLine($"Samlet pris:     {salesOrderHeader.OrderAmount:C}");
            Console.WriteLine("========================================");

        }
    }
}
