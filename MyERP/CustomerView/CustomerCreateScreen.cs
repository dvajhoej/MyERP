using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class CustomerCreateScreen : Screen
    {
        public override string Title { get; set; } = "Opret kunde";

        private Customer _customer;

        public CustomerCreateScreen(Customer kunde)
        {
            _customer = kunde;
        }

        protected override void Draw()
        {
            Clear();

            Form<Customer> editor = new Form<Customer>();

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
