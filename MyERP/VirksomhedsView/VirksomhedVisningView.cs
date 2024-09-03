using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class VirksomhedListeSkærm : Screen
    {
        private ListPage<Virksomhed> listPage;

        public VirksomhedListeSkærm()
        {
            listPage = new ListPage<Virksomhed>();

            InitializeData();
        }

        public override string Title { get; set; } = "Virksomhed";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette virksomhed");
            Console.WriteLine("Tryk F2 for at redigere virksomhed");
            Console.WriteLine("Tryk F5 for at slette virksomhed");

            listPage.AddKey(ConsoleKey.F1, Opretvirksomhed);
            listPage.AddKey(ConsoleKey.F2, RedigerVirksomhed);
            listPage.AddKey(ConsoleKey.F5, SletVirksomhed);

            listPage.AddColumn("Firmanavn", "Firmanavn");
            listPage.AddColumn("Land", "Land");
            listPage.AddColumn("Valuta", "Valuta");

            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new VirksomhedVisningSkærm(selected));
            }

            Quit();
        }

        private void InitializeData()
        {
            listPage.Add(new Virksomhed(0, "Dansk Import", "Danmarksgade", 13, 9000, "Aalborg", "Danmark", Currency.DKK));
            listPage.Add(new Virksomhed(1, "Svensk Import", "Sverigesvej", 51, 58200, "Malmø", "Sverige", Currency.SEK));
            listPage.Add(new Virksomhed(2, "USA Import", "Casinoroad", 67, 1500, "Las Vegas", "USA", Currency.USD));
            listPage.Add(new Virksomhed(3, "EURO Import", "BerlinStrabe", 661, 6712, "Berlin", "Tyskland", Currency.EUR));
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
