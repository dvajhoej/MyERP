using MyERP.DBHELPER;
using System.Data.SqlClient;

namespace MyERP
{
    // Define a partial class Database to store data
    public partial class Database
    {
        // Method to get a product by ID
        public Product? GetProductById(int id)
        {
            // Iterate through the list of products
            foreach (var product in products)
            {
                // Check if the product ID matches
                if (product.ProductID == id)
                {
                    // Return the product
                    return product;
                }
            }
            // Return null if no product is found
            return null;
        }

        // Method to get all products
        public List<Product> GetAllProducts()
        {
            // Define the connection string
            string connectionString = DatabaseString.ConnectionString;

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Define the query to get all products
                string query =      " SELECT " +
                                    "           productID," +
                                    "           name," +
                                    "           description," +
                                    "           sellingPrice," +
                                    "           purchasePrice," +
                                    "           location," +
                                    "           quantity," +
                                    "           unit," +
                                    "           itemnumber" +
                                    " FROM Products";

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
                        // Create a new Product object
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

                        // Add the product to the list
                        products.Add(product);
                    }
                }
            }

            // Return the list of products
            return products;
        }

        // Method to insert a product
        public void InsertProduct(Product product)
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
                    // Define the query to insert a product
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

                    // Create a new SqlCommand object
                    using (SqlCommand command = new SqlCommand(insertProductQuery, connection, transaction))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@ItemNumber", product.ProductNumber);
                        command.Parameters.AddWithValue("@Name", product.Name);
                        command.Parameters.AddWithValue("@Description", product.Description);
                        command.Parameters.AddWithValue("@SellingPrice", product.SellingPrice);
                        command.Parameters.AddWithValue("@PurchasePrice", product.PurchasePrice);
                        command.Parameters.AddWithValue("@Location", product.Location);
                        command.Parameters.AddWithValue("@Quantity", product.QuantityInStock);
                        command.Parameters.AddWithValue("@Unit", product.Unit.ToString());

                        // Retrieve the inserted itemID
                        product.ProductID = (int)command.ExecuteScalar();  // Make sure itemID is the primary key
                        

                        // Use itemID as needed
                    }
                    
                    transaction.Commit();
                    Instance.products.Add(product);
                }
                catch (Exception ex)
                {
                    // Rollback the transaction
                    transaction.Rollback();

                    // Throw an exception with a error message
                    throw new Exception("Error while inserting product: " + ex.Message);
                }
            }
        }

        // Method to update a product
        public void UpdateProduct(Product updateProduct)
        {
            // Check if the product ID is valid
            if (updateProduct.ProductID == 0)
            {
                throw new ArgumentException("Product ID is invalid.");
            }

            // Get the existing product
            var existingProduct = GetProductById(updateProduct.ProductID);

            // Check if the existing product is not null
            if (existingProduct != null)
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
                                // Define the query to update a product
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

                                // Create a new SqlCommand object
                                using (SqlCommand command = new SqlCommand(updateQuery, connection, transaction))
                                {
                                    // Add parameters to the command
                                    command.Parameters.AddWithValue("@ItemNumber", updateProduct.ProductNumber);
                                    command.Parameters.AddWithValue("@Name", updateProduct.Name);
                                    command.Parameters.AddWithValue("@Description", updateProduct.Description);
                                    command.Parameters.AddWithValue("@SellingPrice", updateProduct.SellingPrice);
                                    command.Parameters.AddWithValue("@PurchasePrice", updateProduct.PurchasePrice);
                                    command.Parameters.AddWithValue("@Location", updateProduct.Location);
                                    command.Parameters.AddWithValue("@Quantity", updateProduct.QuantityInStock);
                                    command.Parameters.AddWithValue("@Unit", updateProduct.Unit.ToString());
                                    command.Parameters.AddWithValue("@ProductID", updateProduct.ProductID);

                                    // Execute the command
                                    int rowsAffected = command.ExecuteNonQuery();

                                    // Check if any rows were updated
                                    if (rowsAffected > 0)
                                    {
                                        // Commit the transaction
                                        transaction.Commit();

                                        // Print a success message
                                        Console.WriteLine("Product update successful.");
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
                                Console.WriteLine($"Error while updating product: {ex.Message}");
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
                Console.WriteLine($"Product with ID {updateProduct.ProductID} not found.");
            }
        }

        // Method to delete a product by ID
        public void DeleteProductById(int productID)
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
                    // Get the product to delete
                    var product = GetProductById(productID);

                    // Check if the product is not null
                    if (product != null)
                    {
                        // Define the query to delete a product
                        string deleteCustomerQuery = "DELETE FROM Products WHERE productID = @productID";

                        // Create a new SqlCommand object
                        using (SqlCommand command = new SqlCommand(deleteCustomerQuery, connection, transaction))
                        {
                            // Add a parameter to the command
                            command.Parameters.AddWithValue("@productID", product.ProductID);

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
                    throw new Exception("Error while deleting product: " + ex.Message);
                }
            }
        }
    }
}