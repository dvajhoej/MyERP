using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.KundeView
{
    public class CostumerCreateView : Screen
    {
        public override string Title { get; set; } = "Opret kunde";

        private Kunde _kunde;

        public CostumerCreateView(Kunde kunde)
        {
            _kunde = kunde;
        }

        protected override void Draw()
        {
            Clear();

            Form<Kunde> editor = new Form<Kunde>();

            editor.TextBox("Fornavn", "Fornavn");
            editor.TextBox("Efternavn", "Efternavn");
            editor.TextBox("Telefon", "Telefon");
            editor.TextBox("Email", "Email");
            editor.TextBox("Vej", "Streetname");
            editor.IntBox("Husnummer", "Husnummer");
            editor.IntBox("Postnummer", "Postnummer");
            editor.TextBox("By", "City");
            editor.TextBox("Land", "Country");
            editor.TextBox("Telefon", "Telefon");

        }
    }
}


//listPage.AddColumn("Kunde nummer", "Kundenummer");
//listPage.AddColumn("Navn", "FuldtNavn");
//listPage.AddColumn("Telefon", "Telefon");
//listPage.AddColumn("Email", "Email");

//listPage.AddColumn("Fornavn", "Fornavn");
//listPage.AddColumn("Efternavn", "Efternavn");
//listPage.AddColumn("Vej", "Streetname");
//listPage.AddColumn("Husnummer", "ZipCode");
//listPage.AddColumn("By", "City");
//listPage.AddColumn("Land", "Country");
//listPage.AddColumn("Email", "Email");
//listPage.AddColumn("Telefon", "Telefon");
