using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace MyERP
{
    public class Virksomhed
    {
        public int ID { get; set; }
        public string Firmanavn { get; set; }
        public string Vej { get; set; }
        public int Husnummer { get; set; }
        public int Postnummer { get; set; }
        public string By { get; set; }
        public string Land { get; set; }
        public Currency Valuta { get; set; }

        public Virksomhed(int id, string firmanavn, string vej, int husnummer, int postnummer, string by, string land, Currency valuta)
        {
            ID = id;
            Firmanavn = firmanavn;
            Vej = vej;
            Husnummer = husnummer;
            Postnummer = postnummer;
            By = by;
            Land = land;
            Valuta = valuta;
        }

        public Virksomhed()
        {

        }

    }
}
