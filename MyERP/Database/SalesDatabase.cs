﻿using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    public partial class Database
    {
        public SalesOrderHeader GetSalesOrderHeaderById(int orderID)
        {
            foreach (var sale in salesOrderHeaders)
            {
                if (sale.OrderNumber == orderID)
                {
                    return sale;
                }

            }
            return null;

            //return sales.FirstOrDefault(sale => sale.OrderNumber == orderNumber);
        }
        public SalesOrderLine GetSalesOrderLineById(int salesOrderLineID)
        {
            foreach (var line in salesOrderLines)
            {
                if (line.SalesOrderLineID == salesOrderLineID)
                {
                    return line;
                }

            }
            return null;

            //return sales.FirstOrDefault(sale => sale.OrderNumber == orderNumber);
        }

        public List<SalesOrderLine> GetAllSalesOrderLines()
        {
            string connectionstring = DatabaseString.ConnectionString;
            salesOrderLines.Clear();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string query = "SELECT" +
                               "     Products.name," +
                               "     Products.description," +
                               "     Products.sellingPrice," +
                               "     SalesOrderLines.productID," +
                               "     SalesOrderLines.quantity," +
                               "     SalesOrderHeader.orderID, " +
                               "     Products.unit " +
                               "FROM  " +
                               "     Products " +
                               "INNER JOIN  SalesOrderLines ON Products.productID = SalesOrderLines.productID " +
                               "INNER JOIN  SalesOrderHeader ON SalesOrderLines.salesOrderHeadID = SalesOrderHeader.SalesOrderHeadID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderLine salesOrderLine = new SalesOrderLine
                        {
                            Name = reader.GetString(0),
                            Description = reader.GetString(1),
                            Price = (double)reader.GetDecimal(2),
                            ProductID = reader.GetInt32(3),
                            Quantity = (double)reader.GetDecimal(4),
                            SalesOrderHeadID = reader.GetInt32(5),
                            Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(6)),

                        };
                        salesOrderLines.Add(salesOrderLine);
                    }
                }
            }

            return salesOrderLines;
        }


        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            string connectionString = DatabaseString.ConnectionString;
            salesOrderHeaders.Clear();

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

                        salesOrderHeaders.Add(sale);
                    }
                }
            }

            return salesOrderHeaders;
        }

        public void InsertSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string insertSalesOrderHeaderQuery = "INSERT INTO SalesOrderHeader (" +
                                                                 " creationDate," +
                                                                 " completionDate," +
                                                                 " customerID," +
                                                                 " status)" +
                                                             " OUTPUT INSERTED.orderID " +
                                                             " VALUES (" +
                                                                 " @CreationDate," +
                                                                 " @CompletionDate," +
                                                                 " @CustomerID," +
                                                                 " @Status)";

                        using (SqlCommand command = new SqlCommand(insertSalesOrderHeaderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@CreationDate", salesOrderHeader.CreationDate);
                            command.Parameters.AddWithValue("@CompletionDate", salesOrderHeader.CompletionDate);
                            command.Parameters.AddWithValue("@CustomerID", salesOrderHeader.CustomerNumber);
                            command.Parameters.AddWithValue("@Status", salesOrderHeader.Status);

                            salesOrderHeader.OrderNumber = (int)command.ExecuteScalar();
                        }
                        Instance.salesOrderHeaders.Add(salesOrderHeader);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error while inserting salesOrderHead: " + ex.Message);
                    }
                }
            }
        }

        public void InsertSalesOrderline(SalesOrderLine salesOrderLine)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string insertSalesOrderLineQuery = "INSERT INTO SalesOrderLines (" +
                                                                 " salesOrderHeadID," +
                                                                 " productID," +
                                                                 " quantity)" +
                                                             " OUTPUT INSERTED.salesOrderID " +
                                                             " VALUES (" +
                                                                 " @SalesOrderHeadID," +
                                                                 " @ProductID," +
                                                                 " @Quantity)";

                        using (SqlCommand command = new SqlCommand(insertSalesOrderLineQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderLine.SalesOrderHeadID);
                            command.Parameters.AddWithValue("@ProductID", salesOrderLine.ProductID);
                            command.Parameters.AddWithValue("@Quantity", salesOrderLine.Quantity);

                            salesOrderLine.SalesOrderHeadID = (int)command.ExecuteScalar();
                        }
                        Instance.salesOrderLines.Add(salesOrderLine);
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception("Error while inserting salesOrderLine: " + ex.Message);
                    }
                }
            }
        }

        public void UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            if (salesOrderHeader.OrderNumber == 0)
            {
                throw new ArgumentException("SalesOrderID is invalid.");
            }
            var existingSalesOrderHeader = GetSalesOrderHeaderById(salesOrderHeader.OrderNumber);
            if (existingSalesOrderHeader != null)
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
                                string updateQuery = "UPDATE SalesOrderLines SET " +
                                                         "creationDate = @CreationDate, " +
                                                         "compleionDate = @CompletionDate" +
                                                         "customerID = @CustomerID " +
                                                         "status = @Status" +
                                                     "WHERE orderID = @OrderID;";


                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Update SalesorderHead table
                                    command.Parameters.AddWithValue("@CreationDate", salesOrderHeader.CreationDate);
                                    command.Parameters.AddWithValue("@CompletionDate", salesOrderHeader.CompletionDate);
                                    command.Parameters.AddWithValue("@CustomerID", salesOrderHeader.CustomerNumber);
                                    command.Parameters.AddWithValue("@Status", salesOrderHeader.Status);



                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                        Console.WriteLine("SalesOrderHeader update successful.");
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
                                Console.WriteLine($"Error while updating salesOrderHeader: {ex.Message}");
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
                Console.WriteLine($"Company with ID {salesOrderHeader.OrderNumber} not found.");
            }
        }

        public void UpdateSalesOrderLines(SalesOrderLine salesOrderLines)
        {
            if (salesOrderLines.SalesOrderLineID == 0)
            {
                throw new ArgumentException("SalesOrderID is invalid.");
            }
            var existingSalesOrderLines = GetSalesOrderLineById(salesOrderLines.SalesOrderLineID);
            if (existingSalesOrderLines != null)
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
                                string updateQuery = "UPDATE SalesOrderLines SET " +
                                                            " salesOrderHeadID = @salesOrderHeadID " +
                                                            " productID = @ProductID " +
                                                            " quantity = @Quantity " +
                                                     "WHERE salesOrderLineID = @SalesOrderLineID;";


                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Update SalesorderHead table
                                    command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderLines.Name);
                                    command.Parameters.AddWithValue("@ProductID", salesOrderLines.Description);
                                    command.Parameters.AddWithValue("@Quantity", salesOrderLines.Price);


                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                        Console.WriteLine("SalesOrderLine update successful.");
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
                                Console.WriteLine($"Error while updating SalesOrderLine: {ex.Message}");
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
                Console.WriteLine($"SalesOrderLine with ID {salesOrderLines.SalesOrderLineID} not found.");
            }
        }

        public void DeleteSalesOrderHeadByID(int salesOrderHeadID)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    var salesOrderHead = GetSalesOrderHeaderById(salesOrderHeadID);
                    if (salesOrderHead != null)
                    {
                        string deleteSalesOrderHeaderQuery = "DELETE FROM SalesOrderHeader WHERE salesOrderHeadID = @SalesOrderHeadID";
                        using (SqlCommand command = new SqlCommand(deleteSalesOrderHeaderQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderHead.OrderNumber);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                    }
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw new Exception("Error while deleting salesOrderHead: " + ex.Message);
                }
            }
        }


        public void DeleteSalesOrderLinebyID(int salesOrderLineID)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    var salesOrderLine = GetSalesOrderLineById(salesOrderLineID);
                    if (salesOrderLine != null)
                    {
                        string deleteSalesOrderLineQuery = "DELETE FROM SalesOrderLines WHERE salesOrderLineID = @SalesOrderLineID";
                        using (SqlCommand command = new SqlCommand(deleteSalesOrderLineQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@SalesOrderLineID", salesOrderLine.SalesOrderLineID);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                    }
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw new Exception("Error while deleting salesOrderHead: " + ex.Message);
                }
            }
        }
    }
}