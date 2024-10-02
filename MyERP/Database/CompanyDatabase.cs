using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Method to get a company by ID
        public Company? GetCompanyById(int companyID)
        {
            // Iterate through the list of companies
            foreach (var company in companies)
            {
                // Check if the company ID matches
                if (company.CompanyID == companyID)
                {
                    // Return the company
                    return company;
                }
            }
            // Return null if no company is found
            return null;
        }

        // Method to get all companies
        public List<Company> GetAllCompanies()
        {
            // Define the connection string
            string connectionString = DatabaseString.ConnectionString;

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get all companies
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

                // Create a new SqlCommand object
                SqlCommand command = new SqlCommand(query, connection);

                // Open the connection
                connection.Open();

                // Create a new SqlDataReader object
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Iterate through the reader
                    while (reader.Read())
                    {
                        // Create a new Company object
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

                        // Add the company to the list
                        companies.Add(company);
                    }
                }
            }

            // Return the list of companies
            return companies;
        }

        // Method to insert a company
        public void InsertCompany(Company company)
        {
            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                // Open the connection
                connection.Open();

                // Create a new SqlTransaction object
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

                    // Create a new SqlCommand object
                    using (SqlCommand addressCommand = new SqlCommand(insertAddressQuery, connection, transaction))
                    {
                        // Add parameters to the command
                        addressCommand.Parameters.AddWithValue("@Street", company.Street);
                        addressCommand.Parameters.AddWithValue("@HouseNumber", company.HouseNumber);
                        addressCommand.Parameters.AddWithValue("@ZipCode", company.ZipCode);
                        addressCommand.Parameters.AddWithValue("@City", company.City);
                        addressCommand.Parameters.AddWithValue("@Country", company.Country);

                        // Execute the command and get the inserted address ID
                        company.AddressID = (int)addressCommand.ExecuteScalar();

                        // Insert into Companies table
                        string insertCompanyQuery = "INSERT INTO Companies (" +
                                                        " name," +
                                                        " currency," +
                                                        " addressID) " +
                                                    " OUTPUT INSERTED.companyID" +
                                                    " VALUES (" +
                                                        " @CompanyName," +
                                                        " @Currency," +
                                                        " @AddressID)";

                        // Create a new SqlCommand object
                        using (SqlCommand companyCommand = new SqlCommand(insertCompanyQuery, connection, transaction))
                        {
                            // Add parameters to the command
                            companyCommand.Parameters.AddWithValue("@CompanyName", company.CompanyName);
                            companyCommand.Parameters.AddWithValue("@Currency", company.Currency.ToString());
                            companyCommand.Parameters.AddWithValue("@AddressID", company.AddressID);

                            // Execute the command and get the inserted company ID
                            company.CompanyID = (int)companyCommand.ExecuteScalar();
                        }
                    }

                    // Add the company to the list
                    Instance.companies.Add(company);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();
<<<<<<< HEAD

                    // Throw an exception with a error message
                    throw new Exception("Error while inserting customer: " + ex.Message);
=======
                    throw new Exception(ex.Message);
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                }
            }
        }

        // Method to update a company
        public void UpdateCompany(Company updatedCompany)
        {
            // Check if the company ID is valid
            if (updatedCompany.CompanyID == 0)
            {
                throw new Exception("Kunde ID ikke gyldigt.");
            }

            // Get the existing company
            var existingCompany = GetCompanyById(updatedCompany.CompanyID);

            // Check if the existing company is not null
            if (existingCompany != null)
            {
                try
                {
                    // Create a new SqlConnection object
                    using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
                    {
                        // Open the connection
                        connection.Open();

                        // Create a new SqlTransaction object
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            try
                            {
                                // Define the query to update the company
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

                                // Create a new SqlCommand object
                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@Name", updatedCompany.CompanyName);
                                    command.Parameters.AddWithValue("@Currency", updatedCompany.Currency.ToString());
                                    command.Parameters.AddWithValue("@CompanyId", updatedCompany.CompanyID);

                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@AddressID", updatedCompany.AddressID);
                                    command.Parameters.AddWithValue("@Street", updatedCompany.Street);
                                    command.Parameters.AddWithValue("@HouseNumber", updatedCompany.HouseNumber);
                                    command.Parameters.AddWithValue("@ZipCode", updatedCompany.ZipCode);
                                    command.Parameters.AddWithValue("@City", updatedCompany.City);
                                    command.Parameters.AddWithValue("@Country", updatedCompany.Country);

                                    // Execute the command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    // Check if any rows were affected
                                    if (rowsAffected > 0)
                                    {
                                        // Commit the transaction
                                        transaction.Commit();
                                    }
                                    else
                                    {
<<<<<<< HEAD
                                        // Rollback the transaction
=======
                                        throw new Exception("Ingen linjer blev opdateret.");
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                                        transaction.Rollback();

                                        // Throw an exception
                                        throw new Exception("No rows were updated.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Rollback the transaction
                                transaction.Rollback();
<<<<<<< HEAD

                                // Throw an exception
                                throw new Exception($"Error while updating company: {ex.Message}");
=======
                                throw new Exception($"{ex.Message}");
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
<<<<<<< HEAD
                    // Throw an exception with a error message
                    throw new Exception($"Error while connecting to the database: {ex.Message}");
=======
                    throw new Exception($"Der kunne ikke oprettes forbindelse til sql server");
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                }
            }
            else
            {
<<<<<<< HEAD
                // Throw an exception
                throw new Exception($"Company with ID {updatedCompany.CompanyID} not found.");
=======
                throw new Exception($"Virksomhed med ID {updatedCompany.CompanyID} blev ikke fundet.");
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
            }
        }

        // Method to delete a company by ID
        public void DeleteCompanyById(int companyID)
        {
            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
<<<<<<< HEAD
                // Open the connection
                connection.Open();

                // Create a new SqlTransaction object
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Get the company to delete
                    var company = GetCompanyById(companyID);

                    // Check if the company is not null
                    if (company != null)
                    {
                        // Define the query to delete the company
                        string deleteCompanyQuery = "DELETE FROM Companies WHERE companyID = @CompanyID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteCompanyQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@CompanyID", company.CompanyID);

                            // Execute the command
                            command.ExecuteNonQuery();
=======
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


>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                        }
                    }
                    catch (Exception ex)
                    {

<<<<<<< HEAD
                        // Define the query to delete the address
                        string deleteAddressQuery = "DELETE FROM addresses WHERE addressID = @AddressID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteAddressQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@AddressID", company.AddressID);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Remove the company from the list
                        companies.Remove(company);
=======
                        transaction.Rollback();
                        throw new Exception("Fejl under sletning: " + ex.Message);
>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                    }
                }
                catch (Exception ex)
                {
<<<<<<< HEAD
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while deleting company: " + ex.Message);
=======
                    throw new Exception("Der kunne ikke oprettes forbindelse til sql server");

>>>>>>> 012bc92e9689e5e97bc8cbcb570b1c3506e4706c
                }
            }
        }
    }
}