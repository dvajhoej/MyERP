using TECHCOOL.UI;

namespace MyERP.VirksomhedsView
{
    public class SalesListView : Screen
    {
        private ListPage<Salgsordrehoved> listPage;

        public SalesListView()
        {
            listPage = new ListPage<Salgsordrehoved>();
            listPage.Add(new Salgsordrehoved {
                Ordrenummer = 0,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 123,
                Tilstand = OrdreTilstand.Færdig,     
             });

            listPage.Add(new Salgsordrehoved
            {
                Ordrenummer = 1,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 451,
                Tilstand = OrdreTilstand.Færdig,
            });

            listPage.Add(new Salgsordrehoved
            {
                Ordrenummer = 2,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 717,
                Tilstand = OrdreTilstand.Færdig,
                
            });

            listPage.Add(new Salgsordrehoved
            {
                Ordrenummer = 3,
                Oprettelsestidspunkt = new DateTime(2024, 9, 1, 10, 30, 0),
                Gennemførelsestidspunkt = new DateTime(2024, 9, 3, 15, 45, 0),
                Kundenummer = 182,
                Tilstand = OrdreTilstand.Færdig,
                ord
                
            });
        }
        public override string Title { get; set; } = "Salgsordrehoveder";
        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette virksomhed");
            Console.WriteLine("Tryk F2 for at redigere virksomhed");
            Console.WriteLine("Tryk F5 for at slette virksomhed");
            //listPage.AddKey(ConsoleKey.F1, Opretvirksomhed);
            //listPage.AddKey(ConsoleKey.F2, RedigerVirksomhed);
            //listPage.AddKey(ConsoleKey.F5, SletVirksomhed);

            listPage.AddColumn("Ordrenummer", "Ordrenummer");
            listPage.AddColumn("Oprettelsestidspunkt", "Oprettelsestidspunkt");
            listPage.AddColumn("Kundenummer", "Kundenummer");
            listPage.AddColumn("Navn", "Navn")
            listPage.AddColumn("Tilstand", "Tilstand");
            //listPage.AddColumn("Beløb", "Beløb");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                //Screen.Display(new VirksomhedVisningSkærm(selected));
            }

            Quit();
        }

        //private void Opretvirksomhed(Salgsordrehoved virksomhed)
        //{

        //    var newVirksomhed = new Salgsordrehoved();
        //    listPage.Add(newVirksomhed);
        //    Screen.Display(new VirksomhedOpretView(newVirksomhed));

        //}

        //private void RedigerVirksomhed(Salgsordrehoved selected)
        //{
        //    Screen.Display(new VirksomhedRedigeringView(selected));
        //}

        //public void SletVirksomhed(Virksomhed selected)
        //{
        //    if (selected != null)
        //    {
        //        listPage.Remove(selected);
        //        Console.WriteLine($"Virksomhed '{selected.Firmanavn}' er blevet slettet.");
        //    }

        //}
    }
}
