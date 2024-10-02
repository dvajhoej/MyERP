namespace MyERP
{
    // Define a class SalesOrderLine to represent a sales order line
    public class SalesOrderLine
    {
        // Public properties to store the sales order line details
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int SalesOrderLineID { get; set; }
        public int SalesOrderHeadID { get; set; }
        public string Description { get; set; }
        public UnitType Unit { get; set; }

        // Public property to get the amount
        public double Amount
        {
            get
            {
                // Calculate the amount by multiplying the quantity and price
                return Quantity * Price;
            }
        }

        // Constructor to initialize the sales order line details
        public SalesOrderLine(int productID, string name, double quantity, double price)
        {
            // Initialize the sales order line details
            ProductID = productID;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        // Default constructor
        public SalesOrderLine()
        {
            
        }
    }
}