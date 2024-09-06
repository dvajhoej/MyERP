using System;

namespace MyERP
{
    public class Product
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }

        private string location;
        public string Location
        {
            get => location;
            set
            {
                if (value.Length != 4 || !IsAlphanumeric(value))
                {
                    throw new ArgumentException("Location must be exactly 4 alphanumeric characters.");
                }
                location = value;
            }
        }
        public string MarginPercentage
        {
            get
            {
                try
                {
                    return CalculateMarginPercentage().ToString("F2") + "%";
                }
                catch (DivideByZeroException)
                {
                    return "N/A";
                }
            }
        }

        public decimal QuantityInStock { get; set; }
        public UnitType Unit { get; set; }

        public Product(int productNumber, string name, string description, decimal sellingPrice, decimal purchasePrice, string location, decimal quantityInStock, UnitType unit)
        {
            ProductNumber = productNumber;
            Name = name;
            Description = description;
            SellingPrice = sellingPrice;
            PurchasePrice = purchasePrice;
            Location = location;
            QuantityInStock = quantityInStock;
            Unit = unit;
        }

        public Product()
        {
            
        }

        public decimal CalculateProfit()
        {
            return SellingPrice - PurchasePrice;
        }

        public decimal CalculateMarginPercentage()
        {
            if (PurchasePrice == 0)
            {
                throw new DivideByZeroException("Purchase price cannot be 0 when calculating margin percentage.");
            }
            return (CalculateProfit() / PurchasePrice) * 100;
        }

        public string GetMarginPercentageFormatted()
        {
            return CalculateMarginPercentage().ToString("F2") + "%";
        }

        private bool IsAlphanumeric(string value)
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
