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
                    //instance.customers = instance.GetAllCustomers();

                }
                return instance;
            }
        }

        private List<Company> companies;
        private List<Product> products;
        private List<Person> persons;
        private List<SalesOrderHeader> sales;
        private List<SalesOrderLine> lines;
        private List<Customer> customers;
        private List<Invoice> invoices;

        public Database()
        {
            persons = new List<Person>();
            companies = new List<Company>();
            products = new List<Product>();
            sales = new List<SalesOrderHeader>();
            customers = new List<Customer>();
            lines = new List<SalesOrderLine>();
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

        public List<SalesOrderLine> Lines
        {
            get { return lines; }
        }            

        public List<Company> Companies
        {
            get { return companies; }
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
