using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Salgsordrelinje
    {
        public int Varenummer { get; set; }
        public string Navn { get; set; }
        public decimal Antal { get; set; }
        public decimal Pris { get; set; }

        public decimal Beløb
        {
            get
            {
                return Antal * Pris;
            }
        }

        public Salgsordrelinje(int varenum, string navn, decimal antal, decimal pris)
        {
            Varenummer = varenum;
            Navn = navn;
            Antal = antal;
            Pris = pris;
        }
    }
}
