using MyERP.CompanyView;
using MyERP.CustomerView;
using MyERP.ProductView;
using MyERP.SalesView;
using TECHCOOL.UI;

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

    }
}
