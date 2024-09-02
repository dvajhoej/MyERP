using MyERP;
using TECHCOOL.UI;

namespace MyERP.View
{
    public class Virksomhed
    {
        public string Firmanavn { get; set; }
        public string Land { get; set; }
        public Currency Valuta { get; set; }
    }

    public class VirksomhedSkærm : Screen
    {
        public override string Title { get; set; } = "Liste over virksomheder";

        protected override void Draw()
        {
            Clear();

            ListPage<Virksomhed> listPage = new();

            listPage.Add(new Virksomhed { Firmanavn = "Dansk Import", Land = "Danmark", Valuta = Currency.DKK });
            listPage.Add(new Virksomhed { Firmanavn = "Svensk Import", Land = "Sverige", Valuta = Currency.SEK });
            listPage.Add(new Virksomhed { Firmanavn = "US Import", Land = "USA", Valuta = Currency.USD });
            listPage.Add(new Virksomhed { Firmanavn = "EU Import", Land = "tyskland", Valuta = Currency.EUR });

            listPage.AddColumn("Firmanavn", "Firmanavn");
            listPage.AddColumn("Land", "Land");
            listPage.AddColumn("Valuta", "Valuta");

            var selected = listPage.Select();
            if (selected != null)
            {

            }
            else
            {

            }
        }
    }
}