using MyERP.DBHELPER;
using System.Data.SqlClient;

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

        public List<SalesOrderLine> GetAllSalesOrderLines()
        {
            string connectionstring = DatabaseString.ConnectionString;
            lines.Clear();

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
                               "INNER JOIN  SalesOrderHeader ON SalesOrderLines.salesOrderHeadID = SalesOrderHeader.orderID";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SalesOrderLine line = new SalesOrderLine
                        {
                            Name = reader.GetString(0),
                            Description = reader.GetString(1),
                            Price = (double)reader.GetDecimal(2),
                            ProductID = reader.GetInt32(3),
                            Quantity = (double)reader.GetDecimal(4),
                            OrderID = reader.GetInt32(5),
                            Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(6)),

                        };
                        lines.Add(line);
                    }
                }
            }

            return lines;
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
