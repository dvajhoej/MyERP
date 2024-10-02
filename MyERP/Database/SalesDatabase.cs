using MyERP.DBHELPER;
using System.Data.SqlClient;
using static MyERP.SalesOrderHeader;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Method to get a sales order header by ID
        public SalesOrderHeader GetSalesOrderHeaderById(int orderID)
        {
            // Iterate through the list of sales order headers
            foreach (var sale in salesOrderHeaders)
            {
                // Check if the order ID matches
                if (sale.OrderNumber == orderID)
                {
                    // Return the sales order header
                    return sale;
                }
            }
            // Return null if no sales order header is found
            return null;
        }

        // Method to get a sales order line by ID
        public SalesOrderLine GetSalesOrderLineById(int salesOrderLineID)
        {
            // Iterate through the list of sales order lines
            foreach (var line in salesOrderLines)
            {
                // Check if the sales order line ID matches
                if (line.SalesOrderLineID == salesOrderLineID)
                {
                    // Return the sales order line
                    return line;
                }
            }
            // Return null if no sales order line is found
            return null;
        }

        // Method to get all sales order lines
        public List<SalesOrderLine> GetAllSalesOrderLines()
        {
            // Define the connection string
            string connectionstring = DatabaseString.ConnectionString;

            // Clear the list of sales order lines
            salesOrderLines.Clear();

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                // Define the query to get all sales order lines
                string query = "SELECT" +
                               "     Products.name," +
                               "     Products.description," +
                               "     Products.sellingPrice," +
                               "     SalesOrderLines.productID," +
                               "     SalesOrderLines.quantity," +
                               "     SalesOrderHeader.orderID, " +
                               "     Products.unit, " +
                               "     SalesOrderLines.salesOrderID " +
                               "FROM  " +
                               "     Products " +
                               "INNER JOIN  SalesOrderLines ON Products.productID = SalesOrderLines.productID " +
                               "INNER JOIN  SalesOrderHeader ON SalesOrderLines.salesOrderHeadID = SalesOrderHeader.orderID";

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
                        // Create a new SalesOrderLine object
                        SalesOrderLine salesOrderLine = new SalesOrderLine
                        {
                            Name = reader.GetString(0),
                            Description = reader.GetString(1),
                            Price = (double)reader.GetDecimal(2),
                            ProductID = reader.GetInt32(3),
                            Quantity = (double)reader.GetDecimal(4),
                            SalesOrderHeadID = reader.GetInt32(5),
                            Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(6)),
                            SalesOrderLineID = reader.GetInt32(7),
                        };

                        // Add the sales order line to the list
                        salesOrderLines.Add(salesOrderLine);
                    }
                }
            }

            // Return the list of sales order lines
            return salesOrderLines;
        }

        // Method to get all sales order headers
        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            // Define the connection string
            string connectionString = DatabaseString.ConnectionString;

            // Clear the list of sales order headers
            salesOrderHeaders.Clear();

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get all sales order headers
                string query = "SELECT                                                                                             " +
                               "    SalesOrderHeader.orderID,                                                                      " +
                               "    SalesOrderHeader.creationDate,                                                                 " +
                               "    SalesOrderHeader.completionDate,                                                               " +
                               "    SalesOrderHeader.customerID,                                                                   " +
                               "    Persons.firstname,                                                                             " +
                               "    Persons.lastname,                                                                              " +
                               "    SalesOrderHeader.status,                                                                       " +
                               "    COALESCE(SUM(SalesOrderLines.quantity * Products.sellingPrice), 0) AS totalSalePrice           " +
                               "FROM                                                                                               " +
                               "    SalesOrderHeader                                                                               " +
                               "    LEFT JOIN Customers ON SalesOrderHeader.customerID = Customers.customerID                      " +
                               "    LEFT JOIN Persons ON Customers.personID = Persons.personID                                     " +
                               "    LEFT JOIN SalesOrderLines ON SalesOrderHeader.orderID = SalesOrderLines.salesOrderHeadID       " +
 "    LEFT JOIN Products ON SalesOrderLines.productID = Products.productID                           " +
                               "GROUP BY                                                                                           " +
                               "    SalesOrderHeader.orderID,                                                                      " +
                               "    SalesOrderHeader.creationDate,                                                                 " +
                               "    SalesOrderHeader.completionDate,                                                               " +
                               "    SalesOrderHeader.customerID,                                                                   " +
                               "    Persons.firstname,                                                                             " +
                               "    Persons.lastname,                                                                              " +
                               "    SalesOrderHeader.status                                                                        ";

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
                        // Create a new SalesOrderHeader object
                        SalesOrderHeader sale = new SalesOrderHeader
                        {
                            OrderNumber = reader.GetInt32(0),
                            CreationDate = reader.GetDateTime(1),
                            CompletionDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(2),
                            CustomerNumber = reader.IsDBNull(3) ? 0 : reader.GetInt32(3),
                            Firstname = reader.IsDBNull(4) ? (String?)null : reader.GetString(4),
                            Lastname = reader.IsDBNull(5) ? (String?)null : reader.GetString(5),
                            Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), reader.GetString(6)),
                            TotalPrice = reader.GetDecimal(7),
                        };

                        // Add the sales order header to the list
                        salesOrderHeaders.Add(sale);
                    }
                }
            }

            // Return the list of sales order headers
            return salesOrderHeaders;
        }

        // Method to insert a sales order header
        public void InsertSalesOrderHeader(SalesOrderHeader salesOrderHeader)
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
                    // Define the query to insert a sales order header
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

                    // Create a new SqlCommand object
                    using (SqlCommand command = new SqlCommand(insertSalesOrderHeaderQuery, connection, transaction))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@CreationDate", salesOrderHeader.CreationDate);
                        command.Parameters.AddWithValue("@CompletionDate", salesOrderHeader.CompletionDate as object ?? DBNull.Value);
                        command.Parameters.AddWithValue("@CustomerID", salesOrderHeader.CustomerNumber);
                        command.Parameters.AddWithValue("@Status", salesOrderHeader.Status.ToString());

                        // Execute the command and get the inserted order ID
                        salesOrderHeader.OrderNumber = (int)command.ExecuteScalar();
                    }

                    // Add the sales order header to the list
                    Instance.salesOrderHeaders.Add(salesOrderHeader);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while inserting salesOrderHead: " + ex.Message);
                }
            }
        }

        // Method to insert a sales order line
        public void InsertSalesOrderline(SalesOrderLine salesOrderLine)
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
                    // Define the query to insert a sales order line
                    string insertSalesOrderLineQuery = "INSERT INTO SalesOrderLines (" +
                                                             " salesOrderHeadID," +
                                                             " productID," +
                                                             " quantity)" +
                                                         " OUTPUT INSERTED.salesOrderID " +
                                                         " VALUES (" +
                                                             " @SalesOrderHeadID," +
                                                             " @ProductID," +
                                                             " @Quantity)";

                    // Create a new SqlCommand object
                    using (SqlCommand command = new SqlCommand(insertSalesOrderLineQuery, connection, transaction))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderLine.SalesOrderHeadID);
                        command.Parameters.AddWithValue("@ProductID", salesOrderLine.ProductID);
                        command.Parameters.AddWithValue("@Quantity", salesOrderLine.Quantity);

                        // Execute the command and get the inserted sales order line ID
                        salesOrderLine.SalesOrderLineID = (int)command.ExecuteScalar();
                    }

                    // Add the sales order line to the list
                    Instance.salesOrderLines.Add(salesOrderLine);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception
                    throw new Exception("Error while inserting salesOrderLine: " + ex.Message);
                }
            }
        }

        // Method to update a sales order header
        public void UpdateSalesOrderHeader(SalesOrderHeader salesOrderHeader)
        {
            // Check if the order ID is valid
            if (salesOrderHeader.OrderNumber == 0)
            {
                throw new ArgumentException("SalesOrderID is invalid.");
            }

            // Get the existing sales order header
            var existingSalesOrderHeader = GetSalesOrderHeaderById(salesOrderHeader.OrderNumber);

            // Check if the existing sales order header is not null
            if (existingSalesOrderHeader != null)
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
                                // Define the query to update a sales order header
                                string updateQuery = "UPDATE SalesOrderHeader SET " +
                                                         "creationDate = @CreationDate, " +
                                                         "completionDate = @CompletionDate," +
                                                         "customerID = @CustomerID, " +
                                                         "status = @Status " +
                                                     "WHERE orderID = @OrderID";

                                // Create a new SqlCommand object
                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@CreationDate", salesOrderHeader.CreationDate);
                                    command.Parameters.AddWithValue("@CompletionDate", salesOrderHeader.CompletionDate as object ?? DBNull.Value);
                                    command.Parameters.AddWithValue("@CustomerID", salesOrderHeader.CustomerNumber);
                                    command.Parameters.AddWithValue("@Status", salesOrderHeader.Status.ToString());
                                    command.Parameters.AddWithValue("@OrderID", salesOrderHeader.OrderNumber);

                                    // Execute the command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    // Check if any rows were updated
                                    if (rowsAffected > 0)
                                    {
                                        // Commit the transaction
                                        transaction.Commit();
                                    }
                                    else
                                    {
                                        // Rollback the transaction
                                        transaction.Rollback();

                                        // Print a message
                                        Console.WriteLine("No rows were updated.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Rollback the transaction
                                transaction.Rollback();

                                // Print an error message
                                Console.WriteLine($"Error while updating salesOrderHeader: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Print an error message
                    Console.WriteLine($"Error while connecting to the database: {ex.Message}");
                }
            }
            else
            {
                // Print a message
                Console.WriteLine($"Customer with ID {salesOrderHeader.OrderNumber} not found.");
            }
        }

        // Method to update a sales order line
        public void UpdateSalesOrderLines(SalesOrderLine salesOrderLines)
        {
            // Check if the sales order line ID is valid
            if (salesOrderLines.SalesOrderLineID == 0)
            {
                throw new ArgumentException("SalesOrderID is invalid.");
            }

            // Get the existing sales order line
            var existingSalesOrderLines = GetSalesOrderLineById(salesOrderLines.SalesOrderLineID);

            // Check if the existing sales order line is not null
            if (existingSalesOrderLines != null)
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
                                // Define the query to update a sales order line
                                string updateQuery = "UPDATE SalesOrderLines SET " +
                                                            " salesOrderHeadID = @salesOrderHeadID, " +
                                                            " productID = @ProductID, " +
                                                            " quantity = @Quantity " +
                                                     "WHERE salesOrderID = @SalesOrderLineID;";

                                // Create a new SqlCommand object
                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderLines.SalesOrderHeadID);
                                    command.Parameters.AddWithValue("@ProductID", salesOrderLines.ProductID);
                                    command.Parameters.AddWithValue("@Quantity", salesOrderLines.Quantity);
                                    command.Parameters.AddWithValue("@SalesOrderLineID", salesOrderLines.SalesOrderLineID);

                                    // Execute the command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    // Check if any rows were updated
                                    if (rowsAffected > 0)
                                    {
                                        // Commit the transaction
                                        transaction.Commit();

                                        // Print a success message
                                        Console.WriteLine("SalesOrderLine update successful.");
                                    }
                                    else
                                    {
                                        // Rollback the transaction
                                        transaction.Rollback();

                                        // Print a message
                                        Console.WriteLine("No rows were updated.");
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // Rollback the transaction
                                transaction.Rollback();

                                // Print an error message
                                Console.WriteLine($"Error while updating SalesOrderLine: {ex.Message}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Print an error message
                    Console.WriteLine($"Error while connecting to the database: {ex.Message}");
                }
            }
            else
            {
                // Print a message
                Console.WriteLine($"SalesOrderLine with ID {salesOrderLines.SalesOrderLineID} not found.");
            }
        }

        // Method to delete a sales order header by ID
        public void DeleteSalesOrderHeadByID(int salesOrderHeadID)
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
                    // Get the sales order header to delete
                    var salesOrderHead = GetSalesOrderHeaderById(salesOrderHeadID);

                    // Check if the sales order header is not null
                    if (salesOrderHead != null)
                    {
                        // Define the query to delete a sales order header
                        string deleteSalesOrderHeaderQuery = "DELETE FROM SalesOrderHeader WHERE orderID = @SalesOrderHeadID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteSalesOrderHeaderQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@SalesOrderHeadID", salesOrderHead.OrderNumber);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while deleting salesOrderHead: " + ex.Message);
                }
            }
        }

        // Method to delete a sales order line by ID
        public void DeleteSalesOrderLinebyID(int salesOrderLineID)
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
                    // Get the sales order line to delete
                    var salesOrderLine = GetSalesOrderLineById(salesOrderLineID);

                    // Check if the sales order line is not null
                    if (salesOrderLine != null)
                    {
                        // Define the query to delete a sales order line
                        string deleteSalesOrderLineQuery = "DELETE FROM SalesOrderLines WHERE salesOrderLineID = @SalesOrderLineID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteSalesOrderLineQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@SalesOrderLineID", salesOrderLine.SalesOrderLineID);

                            // Execute the command
                            command.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while deleting salesOrderHead: " + ex.Message);
                }
            }
        }
    }
}