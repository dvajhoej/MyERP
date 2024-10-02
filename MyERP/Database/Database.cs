using Google.Protobuf.Collections;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Private static field to store the instance of the Database class
        public static Database? instance;

        // Public static property to get the instance of the Database class
        public static Database Instance
        {
            get
            {
                // If the instance is null, create a new instance
                if (instance == null)
                {
                    instance = new Database();
                }
                // Return the instance
                return instance;
            }
        }

        // Private fields to store lists of data
        private List<Company> companies;
        private List<Product> products;
        private List<Person> persons;
        private List<SalesOrderHeader> salesOrderHeaders;
        private List<SalesOrderLine> salesOrderLines;
        private List<Customer> customers;
        private List<Invoice> invoices;

        // Constructor to initialize the lists of data
        public Database()
        {
            // Initialize the lists of data
            persons = new List<Person>();
            companies = new List<Company>();
            products = new List<Product>();
            salesOrderHeaders = new List<SalesOrderHeader>();
            customers = new List<Customer>();
            salesOrderLines = new List<SalesOrderLine>();
            invoices = new List<Invoice>();
        }

        // Public properties to get the lists of data
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

    }
}