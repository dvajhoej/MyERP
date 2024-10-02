using MyERP.DBHELPER;
using System.Data.SqlClient;
using TECHCOOL.UI;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Method to get a customer by ID
        public Customer? GetCustomerbyID(int id)
        {
            // Iterate through the list of customers
            foreach (var customer in customers)
            {
                // Check if the customer ID matches
                if (customer.CustomerID == id)
                {
                    // Return the customer
                    return customer;
                }
            }
            // Return null if no customer is found
            return null;
        }

        // Method to get all customers
        public List<Customer> GetAllCustomers()
        {
            // Define the connection string
            string connectionString = DatabaseString.ConnectionString;

            // Clear the list of customers
            customers.Clear();

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get all customers
                string query = "SELECT " +
                                      "Customers.customerID, " +
                                      "Persons.firstname, " +
                                      "Persons.lastname, " +
                                      "Addresses.street, " +
                                      "Addresses.houseNumber, " +
                                      "Addresses.zipCode, " +
                                      "Addresses.city, " +
                                      "Addresses.country, " +
                                      "Persons.phone, " +
                                      "Persons.email, " +
                                      "Customers.lastPurchaseDate, " +
                                      "Persons.personID, " +
                                      "Addresses.addressID " +
                               "FROM Addresses " +
                                      "INNER JOIN Customers ON Addresses.addressID = Customers.addressID " +
                                      "INNER JOIN Persons ON Customers.personID = Persons.personID";

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
                        // Create a new Customer object
                        Customer customer = new Customer
                        {
                            CustomerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Street = reader.GetString(3),
                            HouseNumber = reader.GetString(4),
                            ZipCode = reader.GetString(5),
                            City = reader.GetString(6),
                            Country = reader.GetString(7),
                            Phone = reader.GetString(8),
                            Email = reader.GetString(9),
                            LastPurchaseDate = reader.IsDBNull(10) ? (DateTime?)null : reader.GetDateTime(10),
                            AddressID = reader.GetInt32(11),
                            PersonID = reader.GetInt32(12)
                        };

                        // Add the customer to the list
                        customers.Add(customer);
                    }
                }
            }

            // Return the list of customers
            return customers;
        }

        // Method to insert a customer
        public void InsertCustomer(Customer customer)
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
                    using (SqlCommand command = new SqlCommand(insertAddressQuery, connection, transaction))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Street", customer.Street);
                        command.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                        command.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
                        command.Parameters.AddWithValue("@City", customer.City);
                        command.Parameters.AddWithValue("@Country", customer.Country);

                        // Execute the command and get the inserted address ID
                        customer.AddressID = (int)command.ExecuteScalar();

                        // Insert into Persons table
                        string insertPersonQuery = "INSERT INTO Persons (" +
                                                         " firstname," +
                                                         " lastname," +
                                                         " phone," +
                                                         " email," +
                                                         " addressID) " +
                                                   " OUTPUT INSERTED.personID" +
                                                   " VALUES (" +
                                                         " @FirstName," +
                                                         " @LastName," +
                                                         " @Phone," +
                                                         " @Email," +
                                                         " @AddressID)";

                        // Create a new SqlCommand object
                        using (SqlCommand personCommand = new SqlCommand(insertPersonQuery, connection, transaction))
                        {
                            // Add parameters to the command
                            personCommand.Parameters.AddWithValue("@FirstName", customer.FirstName);
                            personCommand.Parameters.AddWithValue("@LastName", customer.LastName);
                            personCommand.Parameters.AddWithValue("@Phone", customer.Phone);
                            personCommand.Parameters.AddWithValue("@Email", customer.Email);
                            personCommand.Parameters.AddWithValue("@AddressID", customer.AddressID);

                            // Execute the command and get the inserted person ID
                            customer.PersonID = (int)personCommand.ExecuteScalar();

                            // Insert into Customers table
                            string insertCustomerQuery = "INSERT INTO Customers (" +
                                "personID," +
                                " addressID," +
                                " lastPurchaseDate)" +
                                " " +

                                "OUTPUT INSERTED.customerID" +
                                " VALUES (" +
                                "@PersonID," +
                                " @AddressID," +
                                " @LastPurchaseDate)";

                            // Create a new SqlCommand object
                            using (SqlCommand customerCommand = new SqlCommand(insertCustomerQuery, connection, transaction))
                            {
                                // Add parameters to the command
                                customerCommand.Parameters.AddWithValue("@PersonID", customer.PersonID);
                                customerCommand.Parameters.AddWithValue("@AddressID", customer.AddressID);
                                customerCommand.Parameters.AddWithValue("@LastPurchaseDate", "01-01-1900 00:00:00");

                                // Execute the command and get the inserted customer ID
                                customer.CustomerID = (int)customerCommand.ExecuteScalar();
                            }
                        }
                    }

                    // Add the customer to the list
                    Database.Instance.customers.Add(customer);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while inserting customer: " + ex.Message);
                }
            }
        }

        // Method to update a customer
        public void UpdateCustomer(Customer updatedCustomer)
        {
            // Check if the customer ID is valid
            if (updatedCustomer.CustomerID == 0)
            {
                throw new ArgumentException("Customer ID is invalid.");
            }

            // Get the existing customer
            var existingCustomer = GetCustomerbyID(updatedCustomer.CustomerID);

            // Check if the existing customer is not null
            if (existingCustomer != null)
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
                                // Define the query to update the customer
                                string updateQuery = "UPDATE Customers SET " +
                                                     "lastPurchaseDate = @LastPurchaseDate " +
                                                     "WHERE customerID = @CustomerID;" +
                                                     "UPDATE Persons SET " +
                                                     "firstname = @FirstName, " +
                                                     "lastname = @LastName, " +
                                                     "phone = @Phone, " +
                                                     "email = @Email " +
                                                     "WHERE personID = @PersonID;" +
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
                                    command.Parameters.AddWithValue("@CustomerID", updatedCustomer.CustomerID);

                                    // Check if the last purchase date is not null
                                    if (updatedCustomer.LastPurchaseDate != null)
                                    {
                                        command.Parameters.AddWithValue("@LastPurchaseDate", updatedCustomer.LastPurchaseDate);
                                    }
                                    else
                                    {
                                        command.Parameters.AddWithValue("@LastPurchaseDate", new DateTime(1900, 1, 1));
                                    }

                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@PersonID", updatedCustomer.PersonID);
                                    command.Parameters.AddWithValue("@FirstName", updatedCustomer.FirstName);
                                    command.Parameters.AddWithValue("@LastName", updatedCustomer.LastName);
                                    command.Parameters.AddWithValue("@Phone", updatedCustomer.Phone);
                                    command.Parameters.AddWithValue("@Email", updatedCustomer.Email);

                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@AddressID", updatedCustomer.AddressID);
                                    command.Parameters.AddWithValue("@Street", updatedCustomer.Street);
                                    command.Parameters.AddWithValue("@HouseNumber", updatedCustomer.HouseNumber);
                                    command.Parameters.AddWithValue("@ZipCode", updatedCustomer.ZipCode);
                                    command.Parameters.AddWithValue("@City", updatedCustomer.City);
                                    command.Parameters.AddWithValue("@Country", updatedCustomer.Country);

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
                                        // Rollback the transaction
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

                                // Throw an exception
                                throw new Exception($"Error while updating customer: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Throw an exception with a error message
                    throw new Exception($"Error while connecting to the database: {ex.Message}");
                }
            }
            else
            {
                // Throw an exception
                throw new Exception($"Customer with ID {updatedCustomer.CustomerID} not found.");
            }
        }

        // Method to delete a customer by ID
        public void DeleteCustomerByID(int CostumerID)
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
                    // Get the customer to delete
                    var customer = GetCustomerbyID(CostumerID);

                    // Check if the customer is not null
                    if (customer != null)
                    {
                        // Define the query to delete the customer
                        string deleteCustomerQuery = "DELETE FROM Customers WHERE customerID = @customerID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteCustomerQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@customerID", customer.CustomerID);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Define the query to delete the person
                        string deletePersonQuery = "DELETE FROM Persons WHERE personID = @personID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deletePersonQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@personID", customer.PersonID);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Define the query to delete the address
                        string deleteAddressQuery = "DELETE FROM Addresses WHERE addressID = @addressID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteAddressQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@addressID", customer.AddressID);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();

                        // Remove the customer from the list
                        customers.Remove(customer);
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while deleting customer: " + ex.Message);
                }
            }
        }
    }
}