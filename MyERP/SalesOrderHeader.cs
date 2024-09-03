using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public decimal OrderAmount
        {
            get
            {
                return OrderLines.Sum(line => line.Amount);
            }
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
    }
}
