using System;

namespace MyERP
{
    public class SalesOrderLine
    {
        public int ProductNumber { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Amount
        {
            get
            {
                return Quantity * Price;
            }
        }

        public SalesOrderLine(int productNumber, string name, decimal quantity, decimal price)
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
