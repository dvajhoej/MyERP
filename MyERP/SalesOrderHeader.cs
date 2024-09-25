using System.Windows.Markup;

namespace MyERP
{
   

    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int CustomerNumber { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Created;
        private List<SalesOrderLine> OrderLines { get; set; } = new List<SalesOrderLine>();
        public decimal TotalPrice { get; set; }
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
        public enum OrderStatus
        {
            
            Created,
            Confirmed,
            Packed,
            Completed
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

        private SalesOrderLine _salesOrderLine = new SalesOrderLine();
        private Person _person = new Person();
  

        public string? Firstname
        {
            get { return _person.FirstName; }
            set { _person.FirstName = value; }
        }

        public string? Lastname
        {
            get { return _person.LastName; }
            set { _person.LastName = value; }
        }

        public string? Phone
        {
            get { return _person.Phone; }
            set { _person.Phone = value; }
        }

        public string? Email
        {
            get { return _person.Email; }
            set { _person.Email = value; }
        }

        public string Fullname
        {
            get { return _person.FullName; }
            set { _person.FullName = value; }
        }

        public int ProductNumber
        {
            get { return _salesOrderLine.ProductID; }
            set { _salesOrderLine.ProductID = value; }
        }
        public string Name
        {
            get { return _salesOrderLine.Name; }
            set { _salesOrderLine.Name = value; }
        }
        public double Quantity
        {
            get { return _salesOrderLine.Quantity; }
            set { _salesOrderLine.Quantity = value; }
        }
        public double Price
        {
            get { return _salesOrderLine.Price; }
            set { _salesOrderLine.Price = value; }


        }
    }
}