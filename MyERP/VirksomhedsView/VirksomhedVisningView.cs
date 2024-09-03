using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{

    public class VirksomhedListeSkærm : Screen
    {
        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {
            Clear();

            ListPage<Virksomhed> listPage = new();

            listPage.Add(new Virksomhed { Firmanavn = "Dansk Import", By = "Aalborg", Husnummer = 13, Postnummer = 9000, Land = "Danmark", Vej = "Danmarksgade", Valuta = Currency.DKK, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "Svensk Import", By = "Malmø", Husnummer = 51, Postnummer = 58200, Land = "Sverige", Vej = "Sverigesvej", Valuta = Currency.SEK, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "USA Import", By = "Las Vegas", Husnummer = 67, Postnummer = 1500, Land = "Nevada", Vej = "Casinoroad", Valuta = Currency.USD, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "EURO Import", By = "Berlin", Husnummer = 661, Postnummer = 6712, Land = "Tyskland", Vej = "BerlinStrabe", Valuta = Currency.EUR, ID = 0 });

            listPage.AddColumn("Firmanavn", "Firmanavn");
            listPage.AddColumn("Land", "Land");
            listPage.AddColumn("Valuta", "Valuta");

            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new VirksomhedVisningSkærm(selected));
            }
            else 
            {
            }
            this.Quit();
        }
    }


}
