using System;
using System.Collections.Generic;
using System.Linq;

namespace MyERP
{
    public partial class Database
    {
        public Salgsordrehoved HentSalgsOrdreHovedUdFraId(int ordrenummer)
        {
            return sales.FirstOrDefault(sale => sale.Ordrenummer == ordrenummer);
        }

        public List<Salgsordrehoved> salgsordrehoveder()
        {
            return new List<Salgsordrehoved>(sales);
        }

        public void InsætSalgsordrehovede(Salgsordrehoved sale)
        {
            if (sale.Ordrenummer == 0)
            {
                sales.Add(sale);
            }
        }

        public void OpdaterSalgsOrdreHoved(Salgsordrehoved updatesale)
        {
            var existingsale = HentSalgsOrdreHovedUdFraId(updatesale.Ordrenummer);
            if (existingsale != null)
            {
                int index = sales.IndexOf(existingsale);
                sales[index] = updatesale;
            }
        }
        public void SletSalgsOrdreHoved(int ordrenummer)
        {
            var sale = HentSalgsOrdreHovedUdFraId(ordrenummer);
            if (sale != null)
            {
                sales.Remove(sale);
            }
        }
    }
}
