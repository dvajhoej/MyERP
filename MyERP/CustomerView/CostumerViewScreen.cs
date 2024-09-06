using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.CustomerView
{
    public class KundeListSkærm : Screen
    {
        private ListPage<Kunde> listPage;

        public KundeListSkærm()
        {
            listPage = new ListPage<Kunde>();

            listPage.Add(new Kunde { FirstName = "Lars", LastName = "Jensen", Kundenummer = 12345, Email = "tor@tor.dk", Phone = "80123123" });

        }
        public override string Title { get; set; } = "Kunde";
        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at oprette  kunde");
            Console.WriteLine("Tryk F2 for at redigere kunde");
            Console.WriteLine("Tryk F5 for at slette   kunde");
            //listPage.AddKey(ConsoleKey.F1, CreateCostumer);
            //listPage.AddKey(ConsoleKey.F2, EditCostumer);
            //listPage.AddKey(ConsoleKey.F5, DeleteCostumer);

            listPage.AddColumn("Kunde nummer", "Kundenummer");
            listPage.AddColumn("Navn", "FuldtNavn");
            listPage.AddColumn("Telefon", "Telefon");
            listPage.AddColumn("Email", "Email");

            //listPage.AddColumn("Fornavn", "Fornavn");
            //listPage.AddColumn("Efternavn", "Efternavn");
            //listPage.AddColumn("Vej", "Streetname");
            //listPage.AddColumn("Husnummer", "ZipCode");
            //listPage.AddColumn("By", "City");
            //listPage.AddColumn("Land", "Country");
            //listPage.AddColumn("Email", "Email");
            //listPage.AddColumn("Telefon", "Telefon");


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
        private void CreateCostumer(Kunde costumer)
        {

            var NewCostumer = new Kunde();
            listPage.Add(NewCostumer);
            Display(new CostumerCreateView(NewCostumer));

        }

        //private void EditCostumer(Kunde selected)
        //{
        //    Screen.Display(new costumerEditScreen(selected));
        //}

        public void DeleteCostumer(Kunde selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Kunde '{selected.FullName}' er blevet slettet.");
            }

        }
    }
}
