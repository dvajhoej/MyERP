using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class VirksomhedListeSkærm : Screen
    {
        private ListPage<Virksomhed> listPage;

        public VirksomhedListeSkærm()
        {
            listPage = new ListPage<Virksomhed>();
            listPage.Add(new Virksomhed { Firmanavn = "Dansk Import", By = "Aalborg", Husnummer = 13, Postnummer = 9000, Land = "Danmark", Vej = "Danmarksgade", Valuta = Currency.DKK, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "Svensk Import", By = "Malmø", Husnummer = 51, Postnummer = 58200, Land = "Sverige", Vej = "Sverigesvej", Valuta = Currency.SEK, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "USA Import", By = "Las Vegas", Husnummer = 67, Postnummer = 1500, Land = "Nevada", Vej = "Casinoroad", Valuta = Currency.USD, ID = 0 });
            listPage.Add(new Virksomhed { Firmanavn = "EURO Import", By = "Berlin", Husnummer = 661, Postnummer = 6712, Land = "Tyskland", Vej = "BerlinStrabe", Valuta = Currency.EUR, ID = 0 });

        }
        public override string Title { get; set; } = "Virksomhed";
        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette  virksomhed");
            Console.WriteLine("Tryk F2 for at redigere virksomhed");
            Console.WriteLine("Tryk F5 for at slette   virksomhed");
            listPage.AddKey(ConsoleKey.F1, Opretvirksomhed);
            listPage.AddKey(ConsoleKey.F2, RedigerVirksomhed);
            listPage.AddKey(ConsoleKey.F5, SletVirksomhed);

            listPage.AddColumn("Firmanavn", "Firmanavn");
            listPage.AddColumn("Land", "Land");
            listPage.AddColumn("Valuta", "Valuta");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new VirksomhedVisningSkærm(selected));
            }
            else
            {
                Quit();
            }
            
        }

        private void Opretvirksomhed(Virksomhed virksomhed)
        {

            var newVirksomhed = new Virksomhed();
            listPage.Add(newVirksomhed);
            Screen.Display(new VirksomhedOpretView(newVirksomhed));

        }

        private void RedigerVirksomhed(Virksomhed selected)
        {
            Screen.Display(new VirksomhedRedigeringView(selected));
        }

        public void SletVirksomhed(Virksomhed selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Virksomhed '{selected.Firmanavn}' er blevet slettet.");
            }

        }
    }
}
