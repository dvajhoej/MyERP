using MyERP.VirksomhedsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public partial class Database
    {
        public Virksomhed HentVirksomhedUdFraId(int id)
        {
            return virksomheder.FirstOrDefault(company => company.ID == id);
        }

        public List<Virksomhed> HentAlleVirksomheder()
        {
            return new List<Virksomhed>(virksomheder);
        }

        public void IndsætVirksomhed(Virksomhed virksomhed)
        {
            if (virksomhed.ID == 0)
            {
                virksomheder.Add(virksomhed);
            }
        }

        public void OpdaterVirksomhed(Virksomhed updatedvirksomhed)
        {
            if (updatedvirksomhed.ID != 0)
            {
                var existingCompany = HentVirksomhedUdFraId(updatedvirksomhed.ID);
                if (existingCompany != null)
                {
                    int index = virksomheder.IndexOf(existingCompany);
                    virksomheder[index] = updatedvirksomhed;
                }
            }
        }

        public void SletVirksomhedUdFraId(int id)
        {
            var virksomhed = HentVirksomhedUdFraId(id);
            if (virksomhed != null)
            {
                virksomheder.Remove(virksomhed);
            }
        }
    }
}
