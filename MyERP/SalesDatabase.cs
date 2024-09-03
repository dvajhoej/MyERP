using System;
using System.Collections.Generic;
using System.Linq;

namespace MyERP
{
    public partial class Database
    {
        public SalesOrderHeader GetSalesOrderHeaderByNumber(int orderNumber)
        {
            return salesOrders.FirstOrDefault(sale => sale.OrderNumber == orderNumber);
        }

        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            return new List<SalesOrderHeader>(salesOrders);
        }

        public void AddSalesOrderHeader(SalesOrderHeader sale)
        {
            if (sale.OrderNumber == 0)
            {
                salesOrders.Add(sale);
            }
        }

        public void UpdateSalesOrderHeader(SalesOrderHeader updatedSale)
        {
            var existingSale = GetSalesOrderHeaderByNumber(updatedSale.OrderNumber);
            if (existingSale != null)
            {
                int index = salesOrders.IndexOf(existingSale);
                salesOrders[index] = updatedSale;
            }
        }

        public void DeleteSalesOrderHeader(int orderNumber)
        {
            var sale = GetSalesOrderHeaderByNumber(orderNumber);
            if (sale != null)
            {
                salesOrders.Remove(sale);
            }
        }
    }
}
