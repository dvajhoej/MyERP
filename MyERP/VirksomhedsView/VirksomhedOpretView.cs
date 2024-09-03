using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class VirksomhedOpretView : Screen
    {
        public override string Title { get; set; } = "Opret Virksomhed";

        private Virksomhed _virksomhed;

        public VirksomhedOpretView(Virksomhed virksomhed)
        {
            _virksomhed = virksomhed;
        }

        protected override void Draw()
        {
            Clear();

            Form<Virksomhed> editor = new Form<Virksomhed>();

            editor.TextBox("Firmanavn", "Firmanavn");
            editor.TextBox("Vej", "Vej");
            editor.IntBox("Husnummer", "Husnummer");
            editor.IntBox("Postnummer", "Postnummer");
            editor.TextBox("By", "By");
            editor.TextBox("Land", "Land");
            editor.SelectBox("Valuta", "Valuta");
          

            editor.Edit(_virksomhed);


            Console.WriteLine($"Virksomhed {_virksomhed.Firmanavn} er blevet opdateret.");
            this.Quit();
        }
    }
}
