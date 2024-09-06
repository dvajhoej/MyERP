using MyERP.CustomerView;
using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;
using MyERP.SalesView;

namespace MyERP.StartScreen
{
    public class MyMenuScreen : Screen
    {
        public override string Title { get; set; } = "LNE Security A/S";
        private CompanyListScreen companyListScreen = new CompanyListScreen();
        private SalesListScreen salesListScreen = new SalesListScreen();
        private CustomerListScreen customerListScreen = new CustomerListScreen();

        protected override void Draw()
        {
            Menu menu = new Menu();
            menu.Add(companyListScreen);
            menu.Add(salesListScreen);
            menu.Add(customerListScreen);
            menu.Start(this);
        }
    }
}
