using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MyERP.DBHELPER;

namespace MyERP
{
    public partial class Database
    {
        public Product GetProductById(int productNumber)
        {
            return products.FirstOrDefault(prod => prod.ProductNumber == productNumber);
        }

        public List<Product> GetAllProducts()
        {
            string connectionString = DatabaseString.ConnectionString;
            List<Product> products = new List<Product>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT productID, name, description, sellingPrice, purchasePrice, location, quantity, unit FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductNumber = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            SellingPrice = (double)reader.GetDecimal(3),
                            PurchasePrice = (double)reader.GetDecimal(4),
                            Location = reader.GetString(5),
                            QuantityInStock = Convert.ToDouble(reader.GetDecimal(6)),
                            Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(7))
                        };
                        products.Add(product);
                    }
                }
            }

            return products;
        }


        public void InsertProduct(Product product)
        {
            if (product.ProductNumber == 0)
            {
                products.Add(product);
            }
        }

        public void UpdateProduct(Product updateProduct)
        {
            if (updateProduct.ProductNumber != 0)
            {
                var existingProduct = GetProductById(updateProduct.ProductNumber);
                if (existingProduct != null)
                {
                    int index = products.IndexOf(existingProduct);
                    products[index] = updateProduct;
                }
            }
        }

        public void DeleteProductById(int productNumber)
        {
            var product = GetProductById(productNumber);
            if (product != null)
            {
                products.Remove(product);
            }
        }

    }
}
