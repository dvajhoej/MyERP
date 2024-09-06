using System;
using System.Collections.Generic;
using System.Linq;

namespace MyERP
{
    public partial class Database
    {
        public SalesOrderHeader GetSalesOrderHeaderById(int orderNumber)
        {
            return sales.FirstOrDefault(sale => sale.OrderNumber == orderNumber);
        }

        public List<SalesOrderHeader> GetAllSalesOrderHeaders()
        {
            return new List<SalesOrderHeader>(sales);
        }

        public void InsertSalesOrderHeader(SalesOrderHeader sale)
        {
            if (sale.OrderNumber == 0)
            {
                sales.Add(sale);
            }
        }

        public void UpdateSalesOrderHeader(SalesOrderHeader updateSale)
        {
            var existingSale = GetSalesOrderHeaderById(updateSale.OrderNumber);
            if (existingSale != null)
            {
                int index = sales.IndexOf(existingSale);
                sales[index] = updateSale;
            }
        }

        public void DeleteSalesOrderHeader(int orderNumber)
        {
            var sale = GetSalesOrderHeaderById(orderNumber);
            if (sale != null)
            {
                sales.Remove(sale);
            }
        }
    }
}
