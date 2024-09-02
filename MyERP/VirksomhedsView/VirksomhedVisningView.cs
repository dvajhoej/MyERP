using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class Virksomheder
    {
        public string Firmanavn { get; set; }
        public string Land { get; set; }
        public Currency Valuta { get; set; }
    }

    public class VirksomhedListeSkærm : Screen
    {
        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {
            Clear();

            ListPage<Virksomheder> listPage = new();

            listPage.Add(new Virksomheder { Firmanavn = "Dansk Import", Land = "Danmark", Valuta = Currency.DKK });
            listPage.Add(new Virksomheder { Firmanavn = "Svensk Import", Land = "Sverige", Valuta = Currency.SEK });
            listPage.Add(new Virksomheder { Firmanavn = "US Import", Land = "USA", Valuta = Currency.USD });
            listPage.Add(new Virksomheder { Firmanavn = "EU Import", Land = "Tyskland", Valuta = Currency.EUR });

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
