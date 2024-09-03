using MyERP.CompanyViews;
using System;
using System.Collections.Generic;

namespace MyERP
{
    public partial class Database
    {
        private static Database instance;

        public static Database Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Database();
                }
                return instance;
            }
        }

        private List<Company> companies;
        private List<Product> products;
        private List<SalesOrderHeader> salesOrders;

        private Database()
        {
            companies = new List<Company>();
            products = new List<Product>();
            salesOrders = new List<SalesOrderHeader>();
        }

        public List<SalesOrderHeader> SalesOrders
        {
            get { return salesOrders; }
        }

        public List<Company> Companies
        {
            get { return companies; }
        }

        public List<Product> Products
        {
            get { return products; }
        }
    }
}
