using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Person
    {
        public string Fornavn { get; set; }
        public string Efternavn { get; set; }
        public Adresse Adresse { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }

        public string FuldtNavn
        {
            get => $"{Fornavn} {Efternavn}";
        }
        public Person()
        {
            
        }
        public Person(string fornavn, string efternavn, Adresse adresse, string email, string telefon)
        {
            Fornavn = fornavn;
            Efternavn = efternavn;
            Adresse = adresse;
            Email = email;
            Telefon = telefon;
        }

        public override string ToString()
        {
            return $"{FuldtNavn}, {Adresse}, Email: {Email}, Telefon: {Telefon}";
        }
    }
}
