using MyERP.CompanyView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public partial class Database
    {
        // Fetch a company by ID
        public Company GetCompanyById(int id)
        {
            return companies.FirstOrDefault(company => company.ID == id);
        }

        // Fetch all companies
        public List<Company> GetAllCompanies()
        {
            return new List<Company>(companies);
        }

        // Insert a company
        public void InsertCompany(Company company)
        {
            if (company.ID == 0)
            {
                companies.Add(company);
            }
        }

        // Update an existing company
        public void UpdateCompany(Company updatedCompany)
        {
            if (updatedCompany.ID != 0)
            {
                var existingCompany = GetCompanyById(updatedCompany.ID);
                if (existingCompany != null)
                {
                    int index = companies.IndexOf(existingCompany);
                    companies[index] = updatedCompany;
                }
            }
        }

        // Delete a company by ID
        public void DeleteCompanyById(int id)
        {
            var company = GetCompanyById(id);
            if (company != null)
            {
                companies.Remove(company);
            }
        }
    }
}
