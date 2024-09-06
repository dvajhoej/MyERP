using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerViewScreen : Screen
    {
        public Customer customerDisplay { get; set; }

        public CustomerViewScreen(Customer c)
        {
            customer = new Customer
            {
                FullName = c.FullName,
                Street = c.Street,
                City = c.City,
                Country = c.Country,
                ZipCode = c.ZipCode,
                HouseNumber = c.HouseNumber,
                LastPurchaseDate = c.LastPurchaseDate,
               
            };
        }

        public override string Title { get; set; } = "Kunde Visning";
        public Customer customer { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Customer> listPage = new();

            listPage.Add(customer);

            listPage.AddColumn("Fulde navn", "Fullname");
            listPage.AddColumn("Fulde navn", "Address");
            listPage.AddColumn("Fulde navn", "LastPurchaseDate");


            listPage.Select();

            Quit();
        }
    }
}