using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Produkt
    {
        public string Varenummer { get; set; }
        public string Navn { get; set; }
        public string Beskrivelse { get; set; }
        public decimal Salgspris { get; set; }
        public decimal Indkøbspris { get; set; }

        private string lokation;
        public string Lokation
        {
            get => lokation;
            set
            {
                if (value.Length != 4 || !ErTalEllerBogstav(value))
                {
                    throw new ArgumentException("Lokation skal være præcis 4 tal eller bogstaver.");
                }
                lokation = value;
            }
        }

        public decimal AntalPåLager { get; set; }
        public Enhedstype Enhed { get; set; }

        public Produkt(string varenummer, string navn, string beskrivelse, decimal salgspris, decimal indkøbspris, string lokation, decimal antalPåLager, Enhedstype enhed)
        {
            Varenummer = varenummer;
            Navn = navn;
            Beskrivelse = beskrivelse;
            Salgspris = salgspris;
            Indkøbspris = indkøbspris;
            Lokation = lokation;
            AntalPåLager = antalPåLager;
            Enhed = enhed;
        }

        public decimal BeregnFortjeneste()
        {
            return Salgspris - Indkøbspris;
        }

        public decimal BeregnAvanceProcent()
        {
            if (Indkøbspris == 0)
            {
                throw new DivideByZeroException("Indkøbspris kan ikke være 0 ved beregning af avanceprocent.");
            }
            return (BeregnFortjeneste() / Indkøbspris) * 100;
        }

        private bool ErTalEllerBogstav(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum Enhedstype
    {
        Styk,
        Timer,
        Meter
    }
}
