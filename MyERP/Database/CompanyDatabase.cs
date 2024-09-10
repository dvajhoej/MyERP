using MyERP.DBHELPER;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECHCOOL;

namespace MyERP
{
    public partial class Database
    {
        // Fetch a company by ID
        public Company GetCompanyById(int companyID)
        {
            return companies.FirstOrDefault(company => company.CompanyID == companyID);
        }


//        SELECT companies.name, Addresses.street, Addresses.houseNumber, Addresses.zipCode, Addresses.city, Addresses.country, companies.currency
//FROM            Addresses INNER JOIN
//                         companies ON Addresses.addressID = companies.addressID
        // Fetch all companies
        public List<Company> GetAllCompanies()
        {
            string connectionString = DatabaseString.ConnectionString;
            List<Company> companies = new List<Company>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT" +
                    " Companies.name," +
                    " Addresses.street," +
                    " Addresses.houseNumber," +
                    " Addresses.zipCode," +
                    " Addresses.city, " +
                    " Addresses.country," +
                    " Companies.currency" +
                    " FROM" +
                    " Addresses" +
                    " INNER JOIN companies ON Addresses.addressID = companies.addressID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Company company = new Company
                        {
                            CompanyName = reader.GetString(0),
                            Street = reader.GetString(1),
                            HouseNumber = reader.GetString(2),
                            ZipCode = reader.GetString(3),
                            City = reader.GetString(4),
                            Country = reader.GetString(5),
                            Currency = (Currency)Enum.Parse(typeof(Currency), reader.GetString(6)),
                                             
                        };
                        companies.Add(company);
                    }
                }
            }

            return companies;
        }

        // Insert a company
        public void InsertCompany(Company company)
        {
            if (company.CompanyID == 0)
            {
                companies.Add(company);
            }
        }

        // Update an existing company
        public void UpdateCompany(Company updatedCompany)
        {
            if (updatedCompany.CompanyID != 0)
            {
                var existingCompany = GetCompanyById(updatedCompany.CompanyID);
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
