using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    public partial class Database
    {
        public Company? GetCompanyById(int companyID)
        {
            foreach (var company in companies)
            {
                if (company.CompanyID == companyID)
                {
                    return company;
                }

            }
            return null;
        }



        public List<Company> GetAllCompanies()
        {
            string connectionString = DatabaseString.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT" +
                                    " Companies.name," +
                                    " Addresses.street," +
                                    " Addresses.houseNumber," +
                                    " Addresses.zipCode," +
                                    " Addresses.city, " +
                                    " Addresses.country," +
                                    " Companies.currency," +
                                    " Companies.companyID," +
                                    " Addresses.addressID" +
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
                            CompanyID = reader.GetInt32(7),
                            AddressID = reader.GetInt32(8),

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
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into Addresses table
                    string insertAddressQuery = "INSERT INTO Addresses (" +
                                                   " street," +
                                                   " houseNumber," +
                                                   " zipCode," +
                                                   " city," +
                                                   " country) " +

                                               " OUTPUT INSERTED.addressID" +
                                               " VALUES (" +
                                                   " @Street," +
                                                   " @HouseNumber," +
                                                   " @ZipCode," +
                                                   " @City," +
                                                   " @Country)";

                    using (SqlCommand addressCommand = new SqlCommand(insertAddressQuery, connection, transaction))
                    {
                        addressCommand.Parameters.AddWithValue("@Street", company.Street);
                        addressCommand.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                        addressCommand.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                        addressCommand.Parameters.AddWithValue("@City", company.City);
                        addressCommand.Parameters.AddWithValue("@Country", company.Country);

                        // Retrieve the inserted addressID
                        company.AddressID = (int)addressCommand.ExecuteScalar();

                        // Insert into Persons table
                        string insertCompanyQuery = "INSERT INTO Companies (" +
                                                        " name," +
                                                        " currency," +
                                                        " addressID) " +
                                                    " OUTPUT INSERTED.companyID" +
                                                    " VALUES (" +
                                                        " @CompanyName," +
                                                        " @Currency," +
                                                        " @AddressID)";

                        using (SqlCommand companyCommand = new SqlCommand(insertCompanyQuery, connection, transaction))
                        {
                            companyCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                            companyCommand.Parameters.AddWithValue("@Currency", company.Currency.ToString());
                            companyCommand.Parameters.AddWithValue("@AddressID", company.AddressID);

                            company.CompanyID = (int)companyCommand.ExecuteScalar();

                        }
                    }



                    Instance.companies.Add(company);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception(ex.Message);
                }
            }
        }

        // Update an existing company
        public void UpdateCompany(Company updatedCompany)
        {
            if (updatedCompany.CompanyID == 0)
            {
                throw new Exception("Kunde ID ikke gyldigt.");
            }

            var existingCompany = GetCompanyById(updatedCompany.CompanyID);
            if (existingCompany != null)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                string updateQuery = "UPDATE Companies SET " +
                                                         "name = @Name, " +
                                                         "currency = @Currency " +
                                                     "WHERE companyID = @CompanyID;" +
                                                     "UPDATE Addresses SET " +
                                                         "street = @Street, " +
                                                         "houseNumber = @HouseNumber, " +
                                                         "zipCode = @ZipCode, " +
                                                         "city = @City, " +
                                                        "country = @Country " +
                                                     "WHERE addressID = @AddressID;";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Update Companies table
                                    command.Parameters.AddWithValue("@Name", updatedCompany.CompanyName);
                                    command.Parameters.AddWithValue("@Currency", updatedCompany.Currency.ToString());
                                    command.Parameters.AddWithValue("@CompanyId", updatedCompany.CompanyID);

                                    // Update Addresses table
                                    command.Parameters.AddWithValue("@AddressID", updatedCompany.AddressID);
                                    command.Parameters.AddWithValue("@Street", updatedCompany.Street);
                                    command.Parameters.AddWithValue("@HouseNumber", updatedCompany.HouseNumber);
                                    command.Parameters.AddWithValue("@ZipCode", updatedCompany.ZipCode);
                                    command.Parameters.AddWithValue("@City", updatedCompany.City);
                                    command.Parameters.AddWithValue("@Country", updatedCompany.Country);

                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                    }
                                    else
                                    {
                                        throw new Exception("Ingen linjer blev opdateret.");
                                        transaction.Rollback();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                throw new Exception($"{ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Der kunne ikke oprettes forbindelse til sql server");
                }
            }
            else
            {
                throw new Exception($"Virksomhed med ID {updatedCompany.CompanyID} blev ikke fundet.");
            }
        }

        // Delete a company by ID
        public void DeleteCompanyById(int companyID)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {

                        var company = GetCompanyById(companyID);
                        if (company != null)
                        {
                            string deleteCompanyQuery = "DELETE FROM Companies WHERE companyID = @CompanyID";
                            using (SqlCommand command = new SqlCommand(deleteCompanyQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@CompanyID", company.CompanyID);
                                command.ExecuteNonQuery();
                            }

                            string deleteAddressQuery = "DELETE FROM addresses WHERE addressID = @AddressID";
                            using (SqlCommand command = new SqlCommand(@deleteAddressQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@AddressID", company.AddressID);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            companies.Remove(company);


                        }
                    }
                    catch (Exception ex)
                    {

                        transaction.Rollback();
                        throw new Exception("Fejl under sletning: " + ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Der kunne ikke oprettes forbindelse til sql server");

                }
            }
        }
    }
}

