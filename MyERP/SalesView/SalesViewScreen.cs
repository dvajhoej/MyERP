using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    // Define a class SalesViewScreen to display a sales order
    public class SalesViewScreen : Screen
    {
        // Public property to store the sales order to display
        public SalesOrderHeader SalesOrderDisplay { get; set; }

        // Constructor to initialize the sales order to display
        public SalesViewScreen(SalesOrderHeader soh)
        {
            salesOrderHeader = soh;
            ExitOnEscape();
        }

        // Override the Title property to set the title of the screen
        public override string Title { get; set; } = "Ordre Visning";

        // Private field to store the sales order to display
        public SalesOrderHeader salesOrderHeader { get; set; }

        // Override the Draw method to define the layout of the screen
        protected override void Draw()
        {
            // Calculate the number of spaces for the window border
            int space = 83;

            // Draw the top border of the window
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display a message to the user
            Console.WriteLine("│{0,-82} │", "Tryk Esc for at forlade siden");

            // Draw the bottom border of the window
            WindowHelper.Spacer('└', '─', space, '┘');

            // Draw the top border of the sales order details section
            WindowHelper.Spacer('┌', '─', space, '┐');

            // Display the sales order details
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Ordre nummer", salesOrderHeader.OrderNumber);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Oprettelse", salesOrderHeader.CreationDate);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Færdig", salesOrderHeader.CompletionDate);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Kunde nummer", salesOrderHeader.CustomerNumber);
            Console.WriteLine("│{0,-16} │ {1,-63} │", "Kunde navn", WindowHelper.Truncate(salesOrderHeader.Fullname, 64));

            // Draw a separator line
            WindowHelper.Spacer('├', '─', space, '┤');

            // Display the order line details
            Console.WriteLine("│{0,-10} | {1,-20} | {2,-15} | {3,-10} | {4,-15}│", "Ordre linje", "Produkt Navn", "Pris pr. enhed", "Antal", "Linje pris");

            // Draw a separator line
            WindowHelper.Spacer('├', '─', space, '┤');

            // Initialize a variable to store the total amount
            double total = 0;

            // Initialize a variable to store the order line number
            int i = 1;

            // Iterate through the order lines
            foreach (var orderLine in Database.Instance.SalesOrderLines)
            {
                // Check if the order line belongs to the current sales order
                if (orderLine.SalesOrderHeadID == salesOrderHeader.OrderNumber)
                {
                    // Display the order line details
                    Console.WriteLine("│{0,-11} | {1,-20} | {2,-15:C} | {3,-10} | {4,-14:C} │", i, WindowHelper.Truncate(orderLine.Name, 20), orderLine.Price, orderLine.Quantity, orderLine.Amount);

                    // Draw a separator line
                    WindowHelper.Spacer('├', '-', space, '│');

                    // Add the order line amount to the total amount
                    total += orderLine.Amount;

                    // Increment the order line number
                    i++;
                }
            }

            // Display the total amount
            Console.WriteLine("│{0,-15} │ {1,64:C} │", "Samlet total", total);

            // Draw the bottom border of the sales order details section
            WindowHelper.Spacer('└', '─', space, '┘');
        }
    }
}