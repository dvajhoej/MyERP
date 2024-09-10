using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using MyERP.DBHELPER;

namespace MyERP
{
    
    public partial class Database
    {
        
        // Fetch a company by ID
        public Customer GetCustomerbyID(int id)
        {
            return customers.FirstOrDefault(customer => customer.CustomerID == id);
        }

        // Fetch all companies
        public List<Customer> GetAllCustomers()
        {
            
            string connectionString = DatabaseString.ConnectionString;
            List<Customer> customers = new List<Customer>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT " +
                    " Customers.customerID," +
                    " Persons.firstname," +
                    " Persons.lastname," +
                    " Addresses.street," +
                    " Addresses.houseNumber," +
                    " Addresses.zipCode," +
                    " Addresses.city," +
                    " Addresses.country," +
                    " Persons.phone," +
                    " Persons.email," +
                    " Customers.lastPurchaseDate" +                  
                    " FROM " +
                    " Addresses" +
                    " INNER JOIN Customers ON Addresses.addressID = Customers.addressID" +
                    " INNER JOIN Persons ON Addresses.addressID = Persons.addressID" +
                    " AND " +
                    " Customers.personID = Persons.personID";

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
                          
                        };
                            customers.Add(customer);
                    }
                }
            }

            return customers;
        }
        

        // Insert a company
        public void InsertCustomer(Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                customers.Add(customer);
            }
        }

        // Update an existing company
        public void UpdateCustomer(Customer updatedCustomer)
        {
            if (updatedCustomer.CustomerID != 0)
            {
                var existingCustomer = GetCustomerbyID(updatedCustomer.CustomerID);
                if (existingCustomer != null)
                {
                    int index = customers.IndexOf(existingCustomer);
                    customers[index] = updatedCustomer;
                }
            }
        }

        // Delete a company by ID
        public void DeleteCustomerByID(int id)
        {
            var customer = GetCustomerbyID(id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
        }
        
    }
   
}
