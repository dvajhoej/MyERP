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

        private Database()
        {
            virksomheder = new List<Virksomhed>();
        }

        public List<Virksomhed> Virksomheder
        {
            get { return virksomheder; }
        }
    }
}
