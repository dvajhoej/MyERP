namespace MyERP
{
    public class SalesOrderLine
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }

        public double Amount
        {
            get
            {
                return Quantity * Price;
            }
        }

        public SalesOrderLine(int productNumber, string name, double quantity, double price)
        {
            ProductNumber = productNumber;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public SalesOrderLine()
        {

        }


    }
}
