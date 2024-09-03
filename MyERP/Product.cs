using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyERP
{
    public class Product
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }

        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value.Length != 4 || !IsLetterOrDigit(value))
                {
                    throw new ArgumentException("Location must be exactly 4 digits or letters.");
                }
                location = value;
            }
        }

        public decimal StockQuantity { get; set; }
        public UnitType Unit { get; set; }

        public Product(int productNumber, string name, string description, decimal salePrice, decimal purchasePrice, string location, decimal stockQuantity, UnitType unit)
        {
            ProductNumber = productNumber;
            Name = name;
            Description = description;
            SalePrice = salePrice;
            PurchasePrice = purchasePrice;
            Location = location;
            StockQuantity = stockQuantity;
            Unit = unit;
        }

        public decimal CalculateProfit()
        {
            return SalePrice - PurchasePrice;
        }

        public decimal CalculateProfitMarginPercentage()
        {
            if (PurchasePrice == 0)
            {
                throw new DivideByZeroException("Purchase price cannot be 0 when calculating profit margin percentage.");
            }
            return (CalculateProfit() / PurchasePrice) * 100;
        }

        private bool IsLetterOrDigit(string value)
        {
            foreach (char c in value)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public enum UnitType
    {
        Piece,
        Hour,
        Meter
    }
}
