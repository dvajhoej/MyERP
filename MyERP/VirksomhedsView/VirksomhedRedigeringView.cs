using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class VirksomhedRedigeringView : Screen
    {
        public override string Title { get; set; } = "Rediger Virksomhed";

        private Virksomheder _virksomhed;

        public VirksomhedRedigeringView(Virksomheder virksomhed)
        {
            _virksomhed = virksomhed;
        }

        protected override void Draw()
        {
            Clear();

            Form<Virksomheder> editor = new Form<Virksomheder>();

            editor.TextBox("Firmanavn", "Firmanavn");
            editor.TextBox("Vej", "Vej");
            editor.TextBox("Husnummer", "Husnummer");
            editor.TextBox("Postnummer", "Postnummer");
            editor.TextBox("By", "By");
            editor.TextBox("Land", "Land");
            editor.TextBox("Valuta", "Valuta");

            editor.Edit(_virksomhed);

            Clear();
            Console.WriteLine($"Virksomhed {_virksomhed.Firmanavn} er blevet opdateret.");
        }
    }
}
