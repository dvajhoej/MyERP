using MyERP.DBHELPER;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using TECHCOOL.UI;
using TECHCOOL;
using Org.BouncyCastle.Utilities;

namespace MyERP
{
    public partial class Database
    {
        public SalesOrderHeader GetSalesOrderHeaderById(int orderNumber)
        {
            foreach (var sale in sales)
            {
                if (sale.OrderNumber == orderNumber)
                {
                    return sale;
                }

            }
            return null;

            //return sales.FirstOrDefault(sale => sale.OrderNumber == orderNumber);
        }

        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            string connectionString = DatabaseString.ConnectionString;
            sales.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT                                                                                             " +
                               "    SalesOrderHeader.orderID,                                                                      " +
                               "    SalesOrderHeader.creationDate,                                                                 " +
                               "    SalesOrderHeader.completionDate,                                                               " +
                               "    SalesOrderHeader.customerID,                                                                   " +
                               "    Persons.firstname,                                                                             " +
                               "    Persons.lastname,                                                                              " +
                               "    COALESCE(SUM(SalesOrderLines.quantity * Products.sellingPrice), 0) AS totalSalePrice           " +
                               "FROM                                                                                               " +
                               "    SalesOrderHeader                                                                               " +
                               "    LEFT JOIN Customers ON SalesOrderHeader.customerID = Customers.customerID                      " +
                               "    LEFT JOIN Persons ON Customers.personID = Persons.personID                                     " +
                               "    INNER JOIN SalesOrderLines ON SalesOrderHeader.orderID = SalesOrderLines.salesOrderHeadID      " +
                               "    INNER JOIN Products ON SalesOrderLines.productID = Products.productID                          " +
                               "GROUP BY                                                                                           " +
                               "    SalesOrderHeader.orderID,                                                                      " +
                               "    SalesOrderHeader.creationDate,                                                                 " +
                               "    SalesOrderHeader.completionDate,                                                               " +
                               "    SalesOrderHeader.customerID,                                                                   " +
                               "    Persons.firstname,                                                                             " +
                               "    Persons.lastname                                                                               ";


                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderHeader sale = new SalesOrderHeader
                        {
                            OrderNumber = reader.GetInt32(0),
                            CreationDate = reader.GetDateTime(1),
                            CompletionDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            CustomerNumber = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            Firstname = reader.IsDBNull(4) ? (String?)null : reader.GetString(4),
                            Lastname = reader.IsDBNull(5) ? (String?)null : reader.GetString(5),
                            TotalPrice = reader.GetDecimal(6),
                        };

                        sales.Add(sale);
                    }
                }
            }

            return sales;
        }

        public void InsertSalesOrderHeader(SalesOrderHeader salesOrder)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into SalesOrderHeader table
                    string insertHeaderQuery = "INSERT INTO SalesOrderHeader (" +
                                                    "creationDate, " +
                                                    "completionDate, " +
                                                    "customerID) " +
                                               "OUTPUT INSERTED.orderID " +
                                               "VALUES (" +
                                                    "@CreationDate, " +
                                                    "@CompletionDate, " +
                                                    "@CustomerID)";

                    using (SqlCommand headerCommand = new SqlCommand(insertHeaderQuery, connection, transaction))
                    {
                        headerCommand.Parameters.AddWithValue("@CreationDate", salesOrder.CreationDate);
                        headerCommand.Parameters.AddWithValue("@CompletionDate", salesOrder.CompletionDate ?? (object)DBNull.Value);
                        headerCommand.Parameters.AddWithValue("@CustomerID", salesOrder.CustomerNumber);

                        // Retrieve the inserted orderID
                        salesOrder.OrderNumber = (int)headerCommand.ExecuteScalar();
                    }

                    foreach (var line in salesOrder.GetOrderLines())
                    {
                        string insertLineQuery = "INSERT INTO SalesOrderLines (" +
                                                     "salesOrderHeadID, " +
                                                     "productID, " +
                                                     "quantity, " +
                                                     "price) " +
                                                 "VALUES (" +
                                                     "@OrderID, " +
                                                     "@ProductID, " +
                                                     "@Quantity, " +
                                                     "@Price)";

                        using (SqlCommand lineCommand = new SqlCommand(insertLineQuery, connection, transaction))
                        {
                            lineCommand.Parameters.AddWithValue("@OrderID", salesOrder.OrderNumber);
                            lineCommand.Parameters.AddWithValue("@ProductID", line.ProductNumber);
                            lineCommand.Parameters.AddWithValue("@Quantity", line.Quantity);
                            lineCommand.Parameters.AddWithValue("@Price", line.Price);

                            lineCommand.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    Console.WriteLine("Sales order and lines inserted successfully.");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error inserting sales order: " + ex.Message);
                }
            }
        }




        public void UpdateSalesOrderHeader(SalesOrderHeader updateSale)
        {
            var existingSale = GetSalesOrderHeaderById(updateSale.OrderNumber);
            if (existingSale != null)
            {
                int index = sales.IndexOf(existingSale);
                sales[index] = updateSale;
            }
        }

        public void DeleteSalesOrderHeader(int orderNumber)
        {
            var sale = GetSalesOrderHeaderById(orderNumber);
            if (sale != null)
            {
                sales.Remove(sale);
            }
        }
    }
}
