using MyERP.DBHELPER;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Method to get an invoice by ID
        public Invoice? GetInvoiceById(int invoiceID)
        {
            // Iterate through the list of invoices
            foreach (var invoice in invoices)
            {
                // Check if the invoice ID matches
                if (invoice.InvoiceID == invoiceID)
                {
                    // Return the invoice
                    return invoice;
                }
            }
            // Return null if no invoice is found
            return null;
        }

        // Method to get all invoices
        public List<Invoice> GetAllInvoices()
        {
            // Define the connection string
            string connectionString = DatabaseString.ConnectionString;

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get all invoices
                string query = "SELECT" +
                    " InvoiceID," +
                    " salesOrderHeadID," +
                    " invoiceDate" +
                    " FROM Invoices";

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
                        // Create a new Invoice object
                        Invoice invoice = new Invoice
                        {
                            InvoiceID = reader.GetInt32(0),
                            SalesOrderHeadID = reader.IsDBNull(1) ? 0 : reader.GetInt32(1),
                        };

                        // Add the invoice to the list
                        invoices.Add(invoice);
                    }
                }
            }

            // Return the list of invoices
            return invoices;
        }

        // Method to insert an invoice
        public void InsertInvoice(SalesOrderHeader salesOrderHeader, Invoice invoice)
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
                    // Define the query to insert an invoice
                    string query = "INSERT INTO Invoices (salesOrderHeadID) " +
                                   "OUTPUT INSERTED.invoiceID, INSERTED.invoiceDate " +
                                   "VALUES (@SalesOrderHeadID)";

                    // Create a new SqlCommand object
                    using (SqlCommand invoiceCommand = new SqlCommand(query, connection, transaction))
                    {
                        // Add a parameter to the command
                        invoiceCommand.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderHeader.OrderNumber);

                        // Execute the command and get the inserted invoice ID and date
                        using (SqlDataReader reader = invoiceCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                invoice.InvoiceID = reader.GetInt32(0);
                                invoice.InvoiceDate = reader.GetDateTime(1);
                            }
                        }
                    }

                    // Set the order header ID of the invoice to the order number
                    invoice.SalesOrderHeadID = salesOrderHeader.OrderNumber;

                    // Add the invoice to the list
                    Instance.invoices.Add(invoice);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while inserting invoice: " + ex.Message);
                }
            }
        }
    }
} 