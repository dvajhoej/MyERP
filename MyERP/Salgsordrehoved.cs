using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public enum OrdreTilstand
    {
        Ingen,
        Oprettet,
        Bekræftet,
        Pakket,
        Færdig
    }
   
    public class Salgsordrehoved
    {
        public int Ordrenummer { get; set; }
        public DateTime Oprettelsestidspunkt { get; set; }
        public DateTime? Gennemførelsestidspunkt { get; set; }
        public int Kundenummer { get; set; }
        public OrdreTilstand Tilstand { get; set; } = OrdreTilstand.Ingen;
        private List<Salgsordrelinje> Ordrelinjer { get; set; } = new List<Salgsordrelinje>();

        public decimal Ordrebeløb
        {
            get
            {
                return Ordrelinjer.Sum(linje => linje.Beløb);
            }
        }

        public Salgsordrehoved()
        {
            
        }
        public Salgsordrehoved(int ordrenummer, int kundenummer)
        {
            Ordrenummer = ordrenummer;
            Kundenummer = kundenummer;
            Oprettelsestidspunkt = DateTime.Now;
            Tilstand = OrdreTilstand.Oprettet;
        }

        //public string FørsteVareNavn
        //{
        //    get
        //    {
        //        return Ordrelinjer.FirstOrDefault()?.Navn ?? "Ingen varelinjer";
        //    }
        //}
        public void TilføjOrdrelinje(Salgsordrelinje ordrelinje)
        {
            Ordrelinjer.Add(ordrelinje);
        }

        public IReadOnlyList<Salgsordrelinje> HentOrdrelinjer()
        {
            return Ordrelinjer.AsReadOnly();
        }
    }
}
