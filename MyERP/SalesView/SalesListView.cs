using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class SalesListSkærm : Screen
    {
        private ListPage<Salgsordrehoved> listPage;

        public SalesListSkærm()
        {

            listPage = new ListPage<Salgsordrehoved>();

            // Creating Salgsordrehoved objects with associated Salgsordrelinje objects
            var ordre1 = new Salgsordrehoved
            {
                Ordrenummer = 0,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 123,
                Tilstand = OrdreTilstand.Færdig
            };

            // Add Salgsordrelinje to the order
            ordre1.TilføjOrdrelinje(new Salgsordrelinje
            {
                Varenummer = 101,
                Navn = "Product A",
                Antal = 2,
                Pris = 100m // Price per item
            });

            ordre1.TilføjOrdrelinje(new Salgsordrelinje
            {
                Varenummer = 102,
                Navn = "Product B",
                Antal = 1,
                Pris = 50m
            });

            listPage.Add(ordre1);

            var ordre2 = new Salgsordrehoved
            {
                Ordrenummer = 1,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 451,
                Tilstand = OrdreTilstand.Færdig
            };

            ordre2.TilføjOrdrelinje(new Salgsordrelinje
            {
                Varenummer = 103,
                Navn = "Product C",
                Antal = 3,
                Pris = 200m
            });

            listPage.Add(ordre2);
            
        }
        public override string Title { get; set; } = "Salgsordrehoveder";
        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette  ordre");
            Console.WriteLine("Tryk F2 for at redigere ordre");
            Console.WriteLine("Tryk F5 for at slette   ordre");
            //listPage.AddKey(ConsoleKey.F1, Opretvirksomhed);
            //listPage.AddKey(ConsoleKey.F2, RedigerVirksomhed);
            //listPage.AddKey(ConsoleKey.F5, SletVirksomhed);

            listPage.AddColumn("Ordrenummer", "Ordrenummer", 25);
            listPage.AddColumn("Oprettelsestidspunkt", "Oprettelsestidspunkt", 25);
            listPage.AddColumn("Gennemførelsestidspunkt", "Gennemførelsestidspunkt", 25);
            listPage.AddColumn("Kundenummer", "Kundenummer", 25);
            listPage.AddColumn("Tilstand", "Tilstand", 25);
/*            listPage.AddColumn("Navn", "FørsteVareNavn")*/;
            //listPage.AddColumn("Pris", "Pris");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                //Screen.Display(new VirksomhedVisningSkærm(selected));
            }

            Quit();
        }


    }
}
