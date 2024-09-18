using MyERP.DBHELPER;
using System.Data;
using System.Data.SqlClient;

namespace MyERP
{
    public partial class Database
    {
        //List<Customer> customers = Instance.GetAllCustomers();


        public Customer GetCustomerbyID(int id)
        {

            foreach (var customer in customers)
            {
                if (customer.CustomerID == id)
                {
                    return customer;
                }
            }
            return null;
        }

        public List<Customer> GetAllCustomers()
        {
            string connectionString = DatabaseString.ConnectionString;
            customers.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
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

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
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
                            LastPurchaseDate = reader.GetDateTime(10),
                            AddressID = reader.GetInt32(11),
                            PersonID = reader.GetInt32(12)
                        };

                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public static void InsertCustomer(Customer customer)
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

                    using (SqlCommand command = new SqlCommand(insertAddressQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Street", customer.Street);
                        command.Parameters.AddWithValue("@HouseNumber", customer.HouseNumber);
                        command.Parameters.AddWithValue("@ZipCode", customer.ZipCode);
                        command.Parameters.AddWithValue("@City", customer.City);
                        command.Parameters.AddWithValue("@Country", customer.Country);

                        // Retrieve the inserted addressID
                        int addressID = (int)command.ExecuteScalar();

                        // Insert into Persons table
                        string insertPersonQuery = "INSERT INTO Persons (firstname, lastname, phone, email, addressID) " +
                                                   "OUTPUT INSERTED.personID VALUES (@FirstName, @LastName, @Phone, @Email, @AddressID)";

                        using (SqlCommand personCommand = new SqlCommand(insertPersonQuery, connection, transaction))
                        {
                            personCommand.Parameters.AddWithValue("@FirstName", customer.FirstName);
                            personCommand.Parameters.AddWithValue("@LastName", customer.LastName);
                            personCommand.Parameters.AddWithValue("@Phone", customer.Phone);
                            personCommand.Parameters.AddWithValue("@Email", customer.Email);
                            personCommand.Parameters.AddWithValue("@AddressID", addressID);

                            // Retrieve the inserted personID
                            int personID = (int)personCommand.ExecuteScalar();

                            // Insert into Customers table
                            string insertCustomerQuery = "INSERT INTO Customers (personID, addressID, lastPurchaseDate) " +
                                                         "OUTPUT INSERTED.customerID VALUES (@PersonID, @AddressID, @LastPurchaseDate)";

                            using (SqlCommand customerCommand = new SqlCommand(insertCustomerQuery, connection, transaction))
                            {
                                customerCommand.Parameters.AddWithValue("@PersonID", personID);
                                customerCommand.Parameters.AddWithValue("@AddressID", addressID);
                                customerCommand.Parameters.AddWithValue("@LastPurchaseDate", "01-01-1900 00:00:00");

                                // Retrieve the inserted customerID
                                customer.CustomerID = (int)customerCommand.ExecuteScalar();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error while inserting customer: " + ex.Message);
                }
            }
        }

        public void UpdateCustomer(Customer updatedCustomer)
        {
            if (updatedCustomer.CustomerID == 0)
            {
                throw new ArgumentException("Customer ID is invalid.");
            }

            var existingCustomer = GetCustomerbyID(updatedCustomer.CustomerID);
            if (existingCustomer != null)
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

                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@CustomerID", updatedCustomer.CustomerID);
                                    command.Parameters.AddWithValue("@LastPurchaseDate", updatedCustomer.LastPurchaseDate);

                                    // Update Persons table
                                    command.Parameters.AddWithValue("@PersonID", updatedCustomer.PersonID);
                                    command.Parameters.AddWithValue("@FirstName", updatedCustomer.FirstName);
                                    command.Parameters.AddWithValue("@LastName", updatedCustomer.LastName);
                                    command.Parameters.AddWithValue("@Phone", updatedCustomer.Phone);
                                    command.Parameters.AddWithValue("@Email", updatedCustomer.Email);

                                    // Update Addresses table
                                    command.Parameters.AddWithValue("@AddressID", updatedCustomer.AddressID);
                                    command.Parameters.AddWithValue("@Street", updatedCustomer.Street);
                                    command.Parameters.AddWithValue("@HouseNumber", updatedCustomer.HouseNumber);
                                    command.Parameters.AddWithValue("@ZipCode", updatedCustomer.ZipCode);
                                    command.Parameters.AddWithValue("@City", updatedCustomer.City);
                                    command.Parameters.AddWithValue("@Country", updatedCustomer.Country);

                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                        Console.WriteLine("Customer update successful.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("No rows were updated.");
                                        transaction.Rollback();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                Console.WriteLine($"Error while updating customer: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while connecting to the database: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Customer with ID {updatedCustomer.CustomerID} not found.");
            }
        }
    


    public void DeleteCustomerByID(int CostumerID)
        {
 

            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                 
                    var customer = GetCustomerbyID(CostumerID);
                    if (customer != null)
                    {                                               
                        string deleteCustomerQuery = "DELETE FROM Customers WHERE customerID = @customerID";
                        using (SqlCommand command = new SqlCommand(deleteCustomerQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@customerID", CostumerID);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    
                    transaction.Rollback();
                    throw new Exception("Error while deleting customer: " + ex.Message);
                }

            }
        }


    }
}
