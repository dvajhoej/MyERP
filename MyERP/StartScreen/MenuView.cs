using MyERP.KundeView;
using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.StartScreen
{
    public class MyMenuScreen : Screen
    {
        public override string Title { get; set; } = "LNE Security A/S";
        private VirksomhedListeSkærm virk = new VirksomhedListeSkærm();
        private SalesListSkærm sale = new SalesListSkærm();
        private KundeListSkærm kunde = new KundeListSkærm();

        protected override void Draw()
        {
            
            Menu menu = new Menu();
            menu.Add(virk);
            menu.Add(sale);
            menu.Add(kunde);
            menu.Start(this);

           

        }
    }
}