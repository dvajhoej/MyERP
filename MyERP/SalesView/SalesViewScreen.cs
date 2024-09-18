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
        }

        public override string Title { get; set; } = "Ordre Visning";
        public SalesOrderHeader salesOrderHeader { get; set; }

        protected override void Draw()
        {
            Clear();

            ListPage<SalesOrderHeader> listPage = new();

            listPage.Add(salesOrderHeader);

            listPage.AddColumn("Ordre nummer", "OrderNumber", 25);
            listPage.AddColumn("Oprettelse", "CreationDate", 25);
            listPage.AddColumn("Færdig", "CompletionDate", 25);
            listPage.AddColumn("Kunde nummer", "CustomerNumber", 25);
            listPage.AddColumn("Kunde Navn", "Fullname", 25);

            listPage.Select();

            var selected = listPage.Select();
            if (selected != null)
            {
            }
            else
            {
                Quit();
            }
        }
    }
}
