using MyERP.CompanyViews;
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

        private List<Company> companies;

        private Database()
        {
            companies = new List<Company>();
        }

        public List<Company> Virksomheder
        {
            get { return companies; }
        }
    }
}
