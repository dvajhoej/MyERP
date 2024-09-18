using MyERP.CustomerView;
using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;
using MyERP.SalesView;
using MyERP.ProductView;

namespace MyERP.StartScreen
{
    public class MyMenuScreen : Menu
    {
        //public override string Title { get; set; } = "LNE Security A/S";
        private CompanyListScreen companyListScreen = new CompanyListScreen();
        private SalesListScreen salesListScreen = new SalesListScreen();
        private CustomerListScreen customerListScreen = new CustomerListScreen();
        private ProductListScreen productListScreen = new ProductListScreen();

        public MyMenuScreen()
        {
            Add(companyListScreen);
            Add(salesListScreen);
            Add(customerListScreen);
            Add(productListScreen);
        }
        protected override void Draw()
        {
            base.Draw();


        }
    }
}
