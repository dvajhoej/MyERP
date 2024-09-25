using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    public partial class Database
    {
        public Invoice? GetInvoiceById(int invoiceID)
        {
            foreach (var invoice in invoices)
            {
                if (invoice.InvoiceID == invoiceID)
                {
                    return invoice;
                }

            }
            return null;
        }

        public List<Invoice> GetAllInvoices()
        {
            string connectionString = DatabaseString.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT" +
                    " InvoiceID," +
                    " salesOrderHeadID," +
                    " invoiceDate" +
                    " FROM Invoices";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Invoice invoice = new Invoice
                        {
                            InvoiceID = reader.GetInt32(0),
                            SalesOrderHeadID = reader.GetInt32(1),
                        };
                        invoices.Add(invoice);
                    }
                }
            }
            return invoices;
        }

        public void InsertInvoice(SalesOrderHeader salesOrderHeader, Invoice invoice)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into Addresses table
                    string query = "INSERT INTO Invoices (salesOrderHeadID) " +
                                   "OUTPUT INSERTED.invoiceID, INSERTED.invoiceDate " +
                                   "VALUES (@SalesOrderHeadID)";

                    using (SqlCommand invoiceCommand = new SqlCommand(query, connection, transaction))
                    {
                        invoiceCommand.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderHeader.OrderNumber);

                        using (SqlDataReader reader = invoiceCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                invoice.InvoiceID = reader.GetInt32(0);
                                invoice.InvoiceDate = reader.GetDateTime(1);
                            }
                        }
                    }
                                      
                    invoice.SalesOrderHeadID = salesOrderHeader.OrderNumber;
                    instance.invoices.Add(invoice);
                    transaction.Commit();
                }

                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error while inserting invoice: " + ex.Message);
                }
            }
        }
    }
}
