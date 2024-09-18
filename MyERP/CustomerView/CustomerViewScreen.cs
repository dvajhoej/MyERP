﻿using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerViewScreen : Screen
    {
        public Customer customerDisplay { get; set; }

        public CustomerViewScreen(Customer c)
        {
            customer = c;
            ExitOnEscape();
        }

        public override string Title { get; set; } = "Kunde Visning";
        public Customer customer { get; private set; }

        protected override void Draw()
        {
            string lastPurchaseDateDisplay;

            if (customer.LastPurchaseDate == new DateTime(1900, 1, 1))
            {
                lastPurchaseDateDisplay = "";
            }
            else
            {
                lastPurchaseDateDisplay = customer.LastPurchaseDate?.ToShortDateString();
            }

            Console.WriteLine($"Navn:        {customer.Fullname}");
            Console.WriteLine($"Address:     {customer.Street} {customer.HouseNumber}");
            Console.WriteLine($"Postnummer:  {customer.ZipCode}");
            Console.WriteLine($"By:          {customer.City}");
            Console.WriteLine($"Land:        {customer.Country}");
            Console.WriteLine($"Sidste køb:  {lastPurchaseDateDisplay}");
        }

    }
}