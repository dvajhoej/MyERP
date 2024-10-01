using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

            int space = 83;

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-82} │", "Tryk Esc for at forlade siden");
            WindowHelper.Spacer('└', '─', space, '┘');

            WindowHelper.Spacer('┌', '─', space, '┐');
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Ordre nummer", salesOrderHeader.OrderNumber);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Oprettelse", salesOrderHeader.CreationDate);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Færdig", salesOrderHeader.CompletionDate);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Kunde nummer", salesOrderHeader.CustomerNumber);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Kunde navn", WindowHelper.Truncate(salesOrderHeader.Fullname, 64));
            //WindowHelper.Spacer('├', '─', space, '┤');
            //Console.WriteLine("│{0,-15} │ {1,-64} │", "Ordre Linjer", "");

            WindowHelper.Spacer('├', '─', space, '┤');
            Console.WriteLine("│{0,-10} | {1,-20} | {2,-15} | {3,-10} | {4,-15}│", "Ordre linje", "Produkt Navn", "Pris pr. enhed", "Antal", "Linje pris");
            WindowHelper.Spacer('├', '─', space, '┤');


            double total = 0;
            int i = 1;
            foreach (var orderLine in Database.Instance.SalesOrderLines)
            {
                if (orderLine.SalesOrderHeadID == salesOrderHeader.OrderNumber)
                {
                   Console.WriteLine("│{0,-11} | {1,-20} | {2,-15:C} | {3,-10} | {4,-14:C} │", i, WindowHelper.Truncate(orderLine.Name, 20), orderLine.Price, orderLine.Quantity, orderLine.Amount);
                    WindowHelper.Spacer('├', '-', space, '│');

                    total += orderLine.Amount;
                    i++;
                }
            }
            Console.WriteLine("│{0,-15} │ {1,64:C} │", "Samlet total",  total);
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}
