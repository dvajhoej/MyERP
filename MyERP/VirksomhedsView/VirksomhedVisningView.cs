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

            listPage.Add(new Virksomhed { Firmanavn = "Dansk Import", Land = "Danmark", Valuta = Currency.DKK });
            listPage.Add(new Virksomhed { Firmanavn = "Svensk Import", Land = "Sverige", Valuta = Currency.SEK });
            listPage.Add(new Virksomhed { Firmanavn = "US Import", Land = "USA", Valuta = Currency.USD });
            listPage.Add(new Virksomhed { Firmanavn = "EU Import", Land = "Tyskland", Valuta = Currency.EUR });

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
