using System.Text;
using System.Threading.Tasks;

using System.Text;
using System.Threading.Tasks;

using System.Text;
using System.Threading.Tasks;

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

        private List<Virksomhed> virksomheder;
        private List<Produkt> produkter;
        private List<Salgsordrehoved> sales;
        private List<Kunde> kunder;


        private Database()
        {
            virksomheder = new List<Virksomhed>();
            produkter = new List<Produkt>();
            sales = new List<Salgsordrehoved>();
            kunder = new List<Kunde>();


        }
        public List<Kunde> Kunder
        {
            get { return Kunder; }
        }

        public List<Salgsordrehoved> Sales
        {
            get { return Sales; }
        }

        public List<Product> Products
        {
            get { return products; }
        }

        public List<Produkt> Produkter
        {
            get { return produkter; }
        }
    }
}
