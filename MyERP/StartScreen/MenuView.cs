using MyERP.CompanyView;
using MyERP.CustomerView;
using MyERP.ProductView;
using MyERP.SalesView;
using TECHCOOL.UI;

namespace MyERP.StartScreen
{
    // Define a class MyMenuScreen to display the main menu
    public class MyMenuScreen : Menu
    {
        // Private fields to store the screens for each menu item
        private CompanyListScreen companyListScreen = new CompanyListScreen();
        private SalesListScreen salesListScreen = new SalesListScreen();
        private CustomerListScreen customerListScreen = new CustomerListScreen();
        private ProductListScreen productListScreen = new ProductListScreen();

        // Constructor to initialize the menu
        public MyMenuScreen()
        {
            // Add the screens to the menu
            Add(companyListScreen);
            Add(salesListScreen);
            Add(customerListScreen);
            Add(productListScreen);
        }
    }
}