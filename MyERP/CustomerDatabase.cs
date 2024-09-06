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
        public Customer GetCustomerbyID(int id)
        {
            return customers.FirstOrDefault(customer => customer.CustomerID == id);
        }

        // Fetch all companies
        public List<Customer> GetAllCustomers()
        {
            return new List<Customer>(customers);
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
