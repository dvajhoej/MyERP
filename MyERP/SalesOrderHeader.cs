using System.Windows.Markup;

namespace MyERP
{
    public enum OrderStatus
    {
        None,
        Created,
        Confirmed,
        Packed,
        Completed
    }

    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int CustomerNumber { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.None;
        private List<SalesOrderLine> OrderLines { get; set; } = new List<SalesOrderLine>();

        public double OrderAmount
        {
            get
            {
                return OrderLines.Sum(line => line.Amount);
            }
        }

        public SalesOrderHeader()
        {

        }

        public SalesOrderHeader(int orderNumber, int customerNumber)
        {
            OrderNumber = orderNumber;
            CustomerNumber = customerNumber;
            CreationDate = DateTime.Now;
            Status = OrderStatus.Created;
        }

        public void AddOrderLine(SalesOrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }

        public IReadOnlyList<SalesOrderLine> GetOrderLines()
        {
            return OrderLines.AsReadOnly();
        }

        private SalesOrderLine _productNumber = new SalesOrderLine();
        private SalesOrderLine _name = new SalesOrderLine();
        private SalesOrderLine _quantity = new SalesOrderLine();
        private SalesOrderLine _price = new SalesOrderLine();
        private Person _fullName = new Person();

        public string Fullname
        {
            get { return _fullName.FullName; }
            set { _fullName.FullName = value; }
        }

        public int ProductNumber
        {
            get { return _productNumber.ProductNumber; }
            set { _productNumber.ProductNumber = value; }
        }
        public string Name
        {
            get { return _name.Name; }
            set { _name.Name = value; }
        }
        public double Quantity
        {
            get { return _quantity.Quantity; }
            set { _quantity.Quantity = value; }
        }
        public double Price
        {
            get { return _price.Price; }
            set { _price.Price = value; }


        }
    }
}