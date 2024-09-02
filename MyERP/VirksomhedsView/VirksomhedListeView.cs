using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
        public class VirksomhedVisning : Virksomheder
    {
        public string Vej { get; set; }
        public int Husnummer { get; set; }
        public int Postnummer { get; set; }
        public string By { get; set; }
    }

    public class VirksomhedVisningSkærm : Screen
    {
        public VirksomhedVisning virksomhedsVisning { get; set; }

        public VirksomhedVisningSkærm(Virksomheder item)
        {
            // Assuming some details are filled with default values or pulled from another source
            virksomhedsVisning = new VirksomhedVisning
            {
                Firmanavn = item.Firmanavn,
                Land = item.Land,
                Valuta = item.Valuta,
                Vej = "Example Road",
                Husnummer = 123,
                Postnummer = 8000,
                By = "Example City"
            };
        }

        public override string Title { get; set; } = "Virksomhed Visning";

        protected override void Draw()
        {
            Clear();

            ListPage<VirksomhedVisning> listPage = new();

            listPage.Add(virksomhedsVisning);

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