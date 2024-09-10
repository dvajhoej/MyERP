using System.Text;
using System.Threading.Tasks;

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
        private List<SalesOrderHeader> sales;
        private List<Customer> customers;

        private Database()
        {
            companies = new List<Company>();
            products = new List<Product>();
            sales = new List<SalesOrderHeader>();
            customers = new List<Customer>();
        }

        public List<Customer> Customers
        {
            get { return customers; }
        }

        public List<SalesOrderHeader> Sales
        {
            get { return sales; }
        }

        public List<Product> Products
        {
            get { return products; }
        }

        public List<Product> Produkter
        {
            get { return products; }
        }
    }
}
