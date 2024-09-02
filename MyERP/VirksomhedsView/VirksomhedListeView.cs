using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
 
    public class VirksomhedVisningSkærm : Screen
    {
        public Virksomhed virksomhedsVisning { get; set; }

        public VirksomhedVisningSkærm(Virksomhed v)
        {
            virksomhed = new Virksomhed
            {
                Firmanavn = v.Firmanavn,
                Land = v.Land,
                Valuta = v.Valuta,
                Vej = "Example Road",
                Husnummer = 123,
                Postnummer = 8000,
                By = "Example City"
            };
        }

        public override string Title { get; set; } = "Virksomhed Visning";
        public Virksomhed virksomhed { get; private set; }

        protected override void Draw()
        {
            Clear();

            ListPage<Virksomhed> listPage = new();

            listPage.Add(virksomhed);

            listPage.AddColumn("Firmanavn", "Firmanavn");
            listPage.AddColumn("Vej", "Vej");
            listPage.AddColumn("Husnummer", "Husnummer");
            listPage.AddColumn("Postnummer", "Postnummer");
            listPage.AddColumn("By", "By");
            listPage.AddColumn("Land", "Land");
            listPage.AddColumn("Valuta", "Valuta");

            listPage.Select();
        }
    }
}