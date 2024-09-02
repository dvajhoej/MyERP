using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.View
{
    public class EditSkærm(Virksomhed virksomhed) : Screen
    {
        public override string Title { get; set; } = "Rediger Virksomhed";

        protected override void Draw()
        {
            Clear();

            Form<Virksomhed> editor = new Form<Virksomhed>();

            editor.TextBox("Firmanavn", "Navn");
            editor.TextBox("Vej", "Vej");
            editor.TextBox("Husnummer", "Husnummer");
            editor.TextBox("Postnummer", "Postnummer");
            editor.TextBox("By", "By");
            editor.TextBox("Land", "Land");
            editor.TextBox("Valuta", "Valuta");

            editor.Edit(virksomhed);

            Clear();
            Console.WriteLine($"Virksomhed {virksomhed.Navn} er blevet opdateret.");
        }
    }
}
