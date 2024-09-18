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

        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            string connectionString = DatabaseString.ConnectionString;
            sales.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SalesOrderHeader.orderID, " +
                                "   SalesOrderHeader.creationDate, " +
                                "   SalesOrderHeader.completionDate, " +
                                "   SalesOrderHeader.customerID, " +
                                "   Persons.firstname, " +
                                "   Persons.lastname, " +
                                "   COALESCE(SUM(SalesOrderLines.quantity * Products.sellingPrice), 0) AS totalSalePrice " +
                                "   FROM SalesOrderHeader " +
                                "   INNER JOIN Customers ON SalesOrderHeader.customerID = Customers.customerID " +
                                "   INNER JOIN Persons ON Customers.personID = Persons.personID " +
                                "   INNER JOIN SalesOrderLines ON SalesOrderHeader.orderID = SalesOrderLines.salesOrderHeadID " +
                                "   INNER JOIN Products ON SalesOrderLines.productID = Products.productID " +
                                "   GROUP BY SalesOrderHeader.orderID, " +
                                "   SalesOrderHeader.creationDate, " +
                                "   SalesOrderHeader.completionDate, " +
                                "   SalesOrderHeader.customerID, " +
                                "   Persons.firstname, " +
                                "   Persons.lastname;";


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
                            CompletionDate = reader.IsDBNull(2) ? (DateTime?)null : reader.GetDateTime(10),
                            CustomerNumber = reader.GetInt32(3),
                            Firstname = reader.GetString(4),
                            Lastname = reader.GetString(5),
                            TotalPrice = reader.GetDecimal(6),
                        };

                        sales.Add(sale);
                    }
                }
            }

            return sales;
        }

        public void InsertSalesOrderHeader(SalesOrderHeader sale)
        {
            if (sale.OrderNumber == 0)
            {
                sales.Add(sale);
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
