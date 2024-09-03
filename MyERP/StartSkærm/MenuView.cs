using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.StartSkærm
{
    public class MyMenuScreen : Screen
    {
        public override string Title { get; set; } = "LNE Security A/S";
        protected override void Draw()
        {
            Menu menu = new Menu();
            menu.Add(new VirksomhedListeSkærm());
            
            menu.Start(this);
        }
    }
}