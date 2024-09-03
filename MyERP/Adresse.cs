using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Adresse
    {
        public string Vej { get; set; }
        public string Husnummer { get; set; }
        public string Postnummer { get; set; }
        public string By { get; set; }
        public string Land { get; set; }

        public Adresse(string vej, string husnummer, string postnummer, string by, string land)
        {
            Vej = vej;
            Husnummer = husnummer;
            Postnummer = postnummer;
            By = by;
            Land = land;
        }

        public override string ToString()
        {
            return $"{Vej} {Husnummer}, {Postnummer} {By}, {Land}";
        }

    }
}
