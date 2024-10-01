using System.Windows.Markup;

namespace MyERP
{
   

    public class SalesOrderHeader
    {
        public int OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int CustomerNumber { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Oprettet;
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
            CreationDate = DateTime.Now;

        }
        public enum OrderStatus
        {

            Oprettet,
            Bekræftet,
            Pakket,
            Færdig
        }
        public SalesOrderHeader(int customerNumber)
        {

            CustomerNumber = customerNumber;
            CreationDate = DateTime.Now;
            Status = OrderStatus.Oprettet;
        }

        public void AddOrderLine(SalesOrderLine orderLine)
        {
            OrderLines.Add(orderLine);
        }

        public void EditOrderLine(int index, SalesOrderLine updatedOrderLine)
        {
            if (index < 0 || index >= OrderLines.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid order line index.");
            }

            OrderLines[index] = updatedOrderLine;
        }


        public IReadOnlyList<SalesOrderLine> GetOrderLines()
        {
            return OrderLines.AsReadOnly();
        }

        private SalesOrderLine _salesOrderLine = new SalesOrderLine();
        private Customer _customer = new Customer();
  

        public string? Firstname
        {
            get { return _customer.FirstName; }
            set { _customer.FirstName = value; }
        }

        public string? Lastname
        {
            get { return _customer.LastName; }
            set { _customer.LastName = value; }
        }

        public string? Phone
        {
            get { return _customer.Phone; }
            set { _customer.Phone = value; }
        }

        public string? Email
        {
            get { return _customer.Email; }
            set { _customer.Email = value; }
        }

        public string Fullname
        {
            get { return _customer.FullName; }
            set { _customer.FullName = value; }
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