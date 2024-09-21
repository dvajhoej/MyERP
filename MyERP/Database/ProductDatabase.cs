using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    public partial class Database
    {
        public Product GetProductById(int id)
        {
            foreach (var product in products)
            {
                if (product.ProductID == id)
                {
                    return product;
                }

            }
            return null;
        }

        public List<Product> GetAllProducts()
        {
            string connectionString = DatabaseString.ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT productID, name, description, sellingPrice, purchasePrice, location, quantity, unit, itemnumber FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            SellingPrice = (double)reader.GetDecimal(3),
                            PurchasePrice = (double)reader.GetDecimal(4),
                            Location = reader.GetString(5),
                            QuantityInStock = Convert.ToDouble(reader.GetDecimal(6)),
                            Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(7)),
                            ProductNumber = reader.GetInt32(8),
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }


        public void InsertProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into Product table
                    string insertProductQuery = "INSERT INTO Products (" +
                        " itemNumber," +
                        " name," +
                        " description," +
                        " sellingPrice," +
                        " purchasePrice," +
                        " location," +
                        " quantity," +
                        " unit) " +
                        "OUTPUT INSERTED.productID " +
                        "VALUES (" +
                        " @ItemNumber," +
                        " @Name," +
                        " @Description," +
                        " @SellingPrice," +
                        " @PurchasePrice," +
                        " @Location," +
                        " @Quantity," +
                        " @Unit)";

                    using (SqlCommand command = new SqlCommand(insertProductQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ItemNumber", product.ProductNumber);
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@Location", product.Location);
                        command.Parameters.AddWithValue("@Quantity", product.QuantityInStock);
                        command.Parameters.AddWithValue("@Unit", product.Unit.ToString());

                        // Retrieve the inserted itemID
                        int productID = (int)command.ExecuteScalar();  // Make sure itemID is the primary key
                        product.ProductID = productID;
                        Instance.Products.Add(product);

                        // Use itemID as needed
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Error while inserting product: " + ex.Message);
                }
            }
        }


        public void UpdateProduct(Product updateProduct)
        {
            if (updateProduct.ProductID == 0)
            {
                throw new ArgumentException("Product ID is invalid.");
            }

            var existingProduct = GetProductById(updateProduct.ProductID);
            if (existingProduct != null)
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
                                string updateQuery = "UPDATE Products SET " +
                                                     "itemnumber = @ItemNumber, " +
                                                     "name = @Name, " +
                                                     "description = @Description, " +
                                                     "sellingPrice = @SellingPrice, " +
                                                     "purchasePrice = @PurchasePrice, " +
                                                     "location = @Location, " +
                                                     "quantity = @Quantity, " +
                                                     "unit = @Unit " +
                                                     "WHERE productID = @ProductID;";

                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@ItemNumber", updateProduct.ProductNumber);
                                    command.Parameters.AddWithValue("@Name", updateProduct.Name);
                                    command.Parameters.AddWithValue("@Description", updateProduct.Description);
                                    command.Parameters.AddWithValue("@SellingPrice", updateProduct.SellingPrice);
                                    command.Parameters.AddWithValue("@PurchasePrice", updateProduct.PurchasePrice);
                                    command.Parameters.AddWithValue("@Location", updateProduct.Location);
                                    command.Parameters.AddWithValue("@Quantity", updateProduct.QuantityInStock);
                                    command.Parameters.AddWithValue("@Unit", updateProduct.Unit.ToString());
                                    command.Parameters.AddWithValue("@ProductID", updateProduct.ProductID);

                                    int rowsAffected = command.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        transaction.Commit();
                                        Console.WriteLine("Product update successful.");
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
                                Console.WriteLine($"Error while updating product: {ex.Message}");
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
                Console.WriteLine($"Product with ID {updateProduct.ProductID} not found.");
            }
        }




        public void DeleteProductById(int productID)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseString.ConnectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {

                    var product = GetProductById(productID);
                    if (product != null)
                    {
                        string deleteCustomerQuery = "DELETE FROM Products WHERE productID = @productID";
                        using (SqlCommand command = new SqlCommand(deleteCustomerQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@productID", product.ProductID);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();

                    }
                }
                catch (Exception ex)
                {

                    transaction.Rollback();
                    throw new Exception("Error while deleting product: " + ex.Message);
                }




            }
        }
    }
}
