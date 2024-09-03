using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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


        private Database()
        {
            virksomheder = new List<Virksomhed>();
            produkter = new List<Produkt>();
            sales = new List<Salgsordrehoved>();


        }
        public List<Salgsordrehoved> Sales
        {
            get { return Sales; }
        }
        public List<Virksomhed> Virksomheder
        {
            get { return virksomheder; }
        }
        public List<Produkt> Produkter
        {
            get { return produkter; }


        }
    }
}
