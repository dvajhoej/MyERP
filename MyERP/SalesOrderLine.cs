namespace MyERP
{
    public class SalesOrderLine
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public int OrderID { get; set; }
        public string Description { get; set; }
        public UnitType Unit { get; set; }
        

        public double Amount
        {
            get
            {
                return Quantity * Price;
            }
        }

        public SalesOrderLine(int productID, string name, double quantity, double price)
        {
            ProductID = productID;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public SalesOrderLine()
        {

        }


    }
}
