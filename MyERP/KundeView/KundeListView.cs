using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.KundeView
{
    public class KundeListSkærm : Screen
    {
        private ListPage<Kunde> listPage;

        public KundeListSkærm()
        {
            listPage = new ListPage<Kunde>();
            listPage.Add(new Kunde { Fornavn = "Peter", Efternavn = "Larsen" });
            listPage.Add(new Kunde { Fornavn = "Jens", Efternavn = "Thorsen" });


        }
        public override string Title { get; set; } = "Kunde";
        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette  kunde");
            Console.WriteLine("Tryk F2 for at redigere kunde");
            Console.WriteLine("Tryk F5 for at slette   kunde");
            //listPage.AddKey(ConsoleKey.F1, Opretvirksomhed);
            //listPage.AddKey(ConsoleKey.F2, RedigerVirksomhed);
            //listPage.AddKey(ConsoleKey.F5, SletVirksomhed);

            listPage.AddColumn("Fornavn", "Fornavn");
            listPage.AddColumn("Efternavn", "Efternavn");
            //listPage.AddColumn("Valuta", "Valuta");

            // Show the list and get the selected item
            var selected = listPage.Select();
            if (selected != null)
            {
                //Screen.Display(new VirksomhedVisningSkærm(selected));
            }
            else
            {
                Quit();

            }

        }
    }
}
