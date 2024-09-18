using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL.UI;

namespace MyERP.SalesView
{
    public class SalesCreateScreen : Screen
    {
        public override string Title { get; set; } = "Opret Ordre";

        private SalesOrderHeader _salesOrder;
        private Person _person;

        public SalesCreateScreen(SalesOrderHeader salesOrder)
        {
            _salesOrder = salesOrder;
            _salesOrder.CreationDate = DateTime.Now;

            _person = new Person();
            _person.Address = new Address();
        }

        protected override void Draw()
        {
            Clear();

            Form<SalesOrderHeader> orderEditor = new Form<SalesOrderHeader>();
            Form<Person> personEditor = new Form<Person>();
            Form<Address> addressEditor = new Form<Address>();

            orderEditor.IntBox("Salgsordrenummer", "OrderNumber");
            orderEditor.DoubleBox("Dato (oprettet)", "CreationDate"); 
            orderEditor.IntBox("Kundenummer", "CustomerNumber");   
            orderEditor.SelectBox("Ordrestatus", "Status");

            personEditor.TextBox("Fornavn", "FirstName");
            personEditor.TextBox("Efternavn", "LastName");
            personEditor.TextBox("Email", "Email");
            personEditor.TextBox("Telefonnummer", "Phone");

            addressEditor.TextBox("Vej", "Street");
            addressEditor.TextBox("Husnummer", "HouseNumber");
            addressEditor.TextBox("Postnummer", "ZipCode");
            addressEditor.TextBox("By", "City");
            addressEditor.TextBox("Land", "Country");

            orderEditor.Edit(_salesOrder);
            personEditor.Edit(_person);
            addressEditor.Edit(_person.Address);

            orderEditor.Edit(_salesOrder);
            personEditor.Edit(_person);
            addressEditor.Edit(_person.Address);

            this.Quit();
        }
    }
}
