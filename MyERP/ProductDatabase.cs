using System;
using System.Collections.Generic;
using System.Linq;

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
            return new List<Product>(products);
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
                var existingProduct = GetCompanyById(updateProduct.ProductNumber);
                if (existingProduct != null)
                {
                    int index = companies.IndexOf(existingProduct);
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
