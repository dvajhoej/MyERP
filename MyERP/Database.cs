using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
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


        private Database()
        {
            virksomheder = new List<Virksomhed>();
            produkter = new List<Produkt>();


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
