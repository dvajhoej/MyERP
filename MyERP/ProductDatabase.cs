using System;
using System.Collections.Generic;
using System.Linq;

namespace MyERP
{
    public partial class Database
    {
        public Product GetProductByNumber(int productNumber)
        {
            return products.FirstOrDefault(p => p.ProductNumber == productNumber);
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(products);
        }

        public void AddProduct(Product product)
        {
            if (product.ProductNumber == 0)
            {
                products.Add(product);
            }
        }

        public void UpdateProduct(Product updatedProduct)
        {
            if (updatedProduct.ProductNumber != 0)
            {
                var existingProduct = GetProductByNumber(updatedProduct.ProductNumber);
                if (existingProduct != null)
                {
                    int index = products.IndexOf(existingProduct);
                    products[index] = updatedProduct;
                }
            }
        }

        public void DeleteProductByNumber(int productNumber)
        {
            var product = GetProductByNumber(productNumber);
            if (product != null)
            {
                products.Remove(product);
            }
        }
    }
}
