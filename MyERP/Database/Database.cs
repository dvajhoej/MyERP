using Google.Protobuf.Collections;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{

    public partial class Database
    {
        public static Database? instance;

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
        private List<Person> persons;
        private List<SalesOrderHeader> salesOrderHeaders;
        private List<SalesOrderLine> salesOrderLines;
        private List<Customer> customers;
        private List<Invoice> invoices;

        public Database()
        {
            persons = new List<Person>();
            companies = new List<Company>();
            products = new List<Product>();
            salesOrderHeaders = new List<SalesOrderHeader>();
            customers = new List<Customer>();
            salesOrderLines = new List<SalesOrderLine>();
            invoices = new List<Invoice>();
            GetAllInvoices();
            GetAllCustomers();
            GetAllCompanies();
            GetAllProducts();
            GetAllSalesOrderHeaders();
            GetAllSalesOrderLines();
        }


        public List<Person> Persons
        {
            get { return persons; }
        }

        public List<Invoice> Invoices
        {
            get { return invoices; }
        }

        public List<SalesOrderLine> SalesOrderLines
        {
            get { return salesOrderLines; }
        }            

        public List<Company> Companies
        {
            get { return companies; }
        }

        public List<Customer> Customers
        {
            get { return customers; }
        }

        public List<SalesOrderHeader> SalesOrderHeaders
        {
            get { return salesOrderHeaders; }
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
