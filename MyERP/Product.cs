namespace MyERP
{
    // Define a class Product to represent a product
    public class Product
    {
        // Public properties to store the product's details
        public int ProductID { get; set; }
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double SellingPrice { get; set; }
        public double PurchasePrice { get; set; }

        // Private field to store the location
        private string location;

        // Public property to get or set the location
        public string Location
        {
            get => location;
            set
            {
                // Check if the location is exactly 4 alphanumeric characters
                if (value.Length != 4 || !IsAlphanumeric(value))
                {
                    // Throw an exception if the location is invalid
                    throw new ArgumentException("Location must be exactly 4 alphanumeric characters.");
                }
                location = value;
            }
        }

        // Public property to get the margin percentage
        public string MarginPercentage
        {
            get
            {
                try
                {
                    // Calculate the margin percentage and return it as a string
                    return CalculateMarginPercentage().ToString("F2") + "%";
                }
                catch (DivideByZeroException)
                {
                    // Return "N/A" if the purchase price is 0
                    return "N/A";
                }
            }
        }

        // Public property to get the profit
        public double Profit
        {
            get
            {
                // Calculate the profit by subtracting the purchase price from the selling price
                return SellingPrice - PurchasePrice;
            }
        }

        // Public properties to store the quantity in stock and unit type
        public double QuantityInStock { get; set; }
        public UnitType Unit { get; set; }

        // Constructor to initialize the product's details
        public Product(int productNumber, string name, string description, double sellingPrice, double purchasePrice, string location, double quantityInStock, UnitType unit)
        {
            // Set the product's details
            ProductNumber = productNumber;
            Name = name;
            Description = description;
            SellingPrice = sellingPrice;
            PurchasePrice = purchasePrice;
            Location = location;
            QuantityInStock = quantityInStock;
            Unit = unit;
        }

        // Default constructor
        public Product()
        {
            
        }

        // Method to calculate the margin percentage
        public double CalculateMarginPercentage()
        {
            // Check if the purchase price is 0
            if (PurchasePrice == 0)
            {
                // Throw an exception if the purchase price is 0
                throw new DivideByZeroException("Purchase price cannot be 0 when calculating margin percentage.");
            }
            // Calculate the margin percentage by dividing the profit by the purchase price and multiplying by 100
            return (Profit / PurchasePrice) * 100;
        }

        // Method to get the margin percentage formatted as a string
        public string GetMarginPercentageFormatted()
        {
            // Calculate the margin percentage and return it as a string
            return CalculateMarginPercentage().ToString("F2") + "%";
        }

        // Method to check if a string is alphanumeric
        private bool IsAlphanumeric(string value)
        {
            // Iterate over each character in the string
            foreach (char c in value)
            {
                // Check if the character is not a letter or digit
                if (!char.IsLetterOrDigit(c))
                {
                    // Return false if the character is not alphanumeric
                    return false;
                }
            }
            // Return true if all characters are alphanumeric
            return true;
        }
    }

    // Enum to represent the unit type
    public enum UnitType
    {
        Stk,
        Pakker,
        Time,
        Meter
    }
}