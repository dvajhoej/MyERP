using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Kunde : Person
    {
        public int Kundenummer { get; set; }
        public DateTime SidsteKøbDato { get; set; }

        public Kunde(string fornavn, string efternavn, Adresse adresse, string email, string telefon, int kundenummer, DateTime sidsteKøbDato)
            : base(fornavn, efternavn, adresse, email, telefon)
        {
            Kundenummer = kundenummer;
            SidsteKøbDato = sidsteKøbDato;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Kundenummer: {Kundenummer}, Sidste køb: {SidsteKøbDato.ToShortDateString()}";
        }
    }
}
