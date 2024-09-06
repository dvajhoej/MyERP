using System;
using System.Collections.Generic;
using System.Linq;

namespace MyERP
{
    public partial class Database
    {
        public Produkt HentProduktUdFraId(int varenummer)
        {
            return produkter.FirstOrDefault(prot => prot.Varenummer == varenummer);
        }

        public List<Produkt> HentAlleProducter()
        {
            return new List<Produkt>(produkter);
        }

        public void InsætProduct(Produkt produkt)
        {
            if (produkt.Varenummer == 0)
            {
                produkter.Add(produkt);
            }
        }

        public void OpdaterProduct(Produkt updateproduct)
        {
            if (updateproduct.Varenummer != 0)
            {
                var existingproduct = HentVirksomhedUdFraId(updateproduct.Varenummer);
                if (existingproduct != null)
                {
                    int index = virksomheder.IndexOf(existingproduct);
                    produkter[index] = updateproduct;
                }
            }
        }

        public void SletProductUdFraId(int varenummer)
        {
            var product = HentProduktUdFraId(varenummer);
            if (product != null)
            {
                produkter.Remove(product);
            }
        }
    }
}
