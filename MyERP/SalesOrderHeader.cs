using System.Windows.Markup;

namespace MyERP
{
    // Define a class SalesOrderHeader to represent a sales order header
    public class SalesOrderHeader
    {
        // Public properties to store the sales order header details
        public int OrderNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public int CustomerNumber { get; set; }
        public OrderStatus Status { get; set; }

        // Private field to store the order lines
        private List<SalesOrderLine> OrderLines { get; set; } = new List<SalesOrderLine>();

        // Public property to get the total price
        public decimal TotalPrice { get; set; }

        // Public property to get the order amount
        public double OrderAmount
        {
            get
            {
                // Calculate the order amount by summing the amounts of all order lines
                return OrderLines.Sum(line => line.Amount);
            }
        }

        // Default constructor
        public SalesOrderHeader()
        {
            // Initialize the creation date and status
            CreationDate = DateTime.Now;
            Status = OrderStatus.Oprettet;
        }

        // Constructor to initialize the sales order header with a customer number
        public SalesOrderHeader(int customerNumber)
        {
            // Initialize the customer number, creation date, and status
            CustomerNumber = customerNumber;
            CreationDate = DateTime.Now;
        }

        // Enum to represent the order status
        public enum OrderStatus
        {
            Oprettet,
            Bekræftet,
            Pakket,
            Færdig
        }

        // Method to add an order line
        public void AddOrderLine(SalesOrderLine line)
        {
            // Add the order line to the list of order lines
            OrderLines.Add(line);
        }

        // Method to edit an order line
        public void EditOrderLine(int index, SalesOrderLine updatedOrderLine)
        {
            // Check if the index is valid
            if (index < 0 || index >= OrderLines.Count)
            {
                // Throw an exception if the index is invalid
                throw new ArgumentOutOfRangeException("Invalid order line index.");
            }

            // Update the order line at the specified index
            OrderLines[index] = updatedOrderLine;
        }

        // Method to get the order lines as a read-only list
        public IReadOnlyList<SalesOrderLine> GetOrderLines()
        {
            // Return the order lines as a read-only list
            return OrderLines.AsReadOnly();
        }

        // Private fields to store the sales order line and customer details
        private SalesOrderLine _salesOrderLine = new SalesOrderLine();
        private Customer _customer = new Customer();

        // Public properties to get and set the customer details
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

        // Public properties to get and set the sales order line details
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