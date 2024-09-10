using MyERP.productView;
using TECHCOOL.UI;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace MyERP.ProductView
{
    public class ProductListScreen : Screen
    {
        private ListPage<Product> listPage;

        public ProductListScreen()
        {
            listPage = new ListPage<Product>();
            List<Product> products = Database.Instance.GetAllProducts();
            listPage.Add(products);
        }

        public override string Title { get; set; } = "Produkter";

        protected override void Draw()
        {
            Clear();

            Console.WriteLine("Tryk F1 for at skabe et produkt");
            Console.WriteLine("Tryk F2 for at tilpasse et produkt");
            Console.WriteLine("Tryk F5 for at slette et produkt");

            listPage.AddKey(ConsoleKey.F1, CreateProduct);
            listPage.AddKey(ConsoleKey.F2, EditProduct);
            listPage.AddKey(ConsoleKey.F5, DeleteProduct);
            listPage.AddKey(ConsoleKey.Escape, Quit);

            listPage.AddColumn("Varenummer", "ProductNumber");
            listPage.AddColumn("Navn", "Name");
            listPage.AddColumn("Antal På Lager", "QuantityInStock");
            listPage.AddColumn("Enhed", "Unit");
            listPage.AddColumn("Indkøbspris", "PurchasePrice");
            listPage.AddColumn("Salgspris", "SellingPrice");
            listPage.AddColumn("Avance (%)", "MarginPercentage");


            var selected = listPage.Select();
            if (selected != null)
            {
                Screen.Display(new ProductViewScreen(selected));
            }
        
        }
        void Quit(Product _)
        {
            Quit();
        }
        private void CreateProduct(Product product)
        {
            var newProduct = new Product();
            listPage.Add(newProduct);
            Screen.Display(new ProductCreateScreen(newProduct));
        }

        private void EditProduct(Product selected)
        {
            Screen.Display(new ProductEditScreen(selected));
        }

        private void DeleteProduct(Product selected)
        {
            if (selected != null)
            {
                listPage.Remove(selected);
                Console.WriteLine($"Produktet '{selected.Name}' er blevet slettet.");
            }
        }
        //private void FetchProductFromDatabase()
        //{
        //    string connectionString = DatabaseString.ConnectionString;
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        string query = "SELECT productID, name, description, sellingPrice, purchasePrice, location, quantity,  unit FROM Products";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        connection.Open();
        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                Product product = new Product
        //                {
        //                    ProductNumber = reader.GetInt32(0),
        //                    Name = reader.GetString(1),
        //                    Description = reader.GetString(2),
        //                    SellingPrice = (double)reader.GetDecimal(3),
        //                    PurchasePrice = (double)reader.GetDecimal(4),
        //                    Location = reader.GetString(5),
        //                    QuantityInStock = Convert.ToDouble(reader.GetDecimal(6)),
        //                    Unit = (UnitType)Enum.Parse(typeof(UnitType), reader.GetString(7)) 
        //                };
        //                listPage.Add(product);
        //            }
        //        }
        //    }
        }
    }
