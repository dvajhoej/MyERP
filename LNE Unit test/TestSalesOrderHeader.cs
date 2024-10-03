using MyERP;

namespace LNE_Unit_test
{
    public class TestSalesOrderHeader
    {
        [Fact]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var salesOrder = new SalesOrderHeader();

            // Assert
            Assert.Equal(0, salesOrder.OrderNumber);
            Assert.Equal(0, salesOrder.CustomerNumber);
            Assert.Equal(SalesOrderHeader.OrderStatus.Oprettet, salesOrder.Status); // Default status is 'Created'
            Assert.Equal(0, salesOrder.TotalPrice); // Default TotalPrice is 0
        }

        [Fact]
        public void Constructor_WithParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            var customerNumber = 2002;

            // Act
            var salesOrder = new SalesOrderHeader(customerNumber);

            // Assert
            Assert.Equal(customerNumber, salesOrder.CustomerNumber);
            Assert.Equal(SalesOrderHeader.OrderStatus.Oprettet, salesOrder.Status);
            Assert.Equal(DateTime.Now.Date, salesOrder.CreationDate.Date); // Ensure CreationDate is today
        }

        [Fact]
        public void AddOrderLine_ShouldAddLineToOrderLines()
        {
            // Arrange
            var salesOrder = new SalesOrderHeader();
            var orderLine = new SalesOrderLine
            {
                ProductID = 1,
                Name = "Product 1",
                Quantity = 2,
                Price = 10.00
            };

            // Act

            salesOrder.AddOrderLine(orderLine);

            // Assert
            Assert.Single(salesOrder.GetOrderLines());
            Assert.Equal(orderLine, salesOrder.GetOrderLines()[0]);
        }

       
        [Fact]
        public void SalesOrderHeader_ShouldSetAndGetCustomerDetails()
        {
            // Arrange
            var salesOrder = new SalesOrderHeader
            {
                Firstname = "John",
                Lastname = "Doe",
                Phone = "1234567890",
                Email = "john.doe@example.com",
                Fullname = "John Doe"
            };

            // Assert
            Assert.Equal("John", salesOrder.Firstname);
            Assert.Equal("Doe", salesOrder.Lastname);
            Assert.Equal("1234567890", salesOrder.Phone);
            Assert.Equal("john.doe@example.com", salesOrder.Email);
            Assert.Equal("John Doe", salesOrder.Fullname);
        }

        [Fact]
        public void SalesOrderHeader_ShouldSetAndGetOrderLineProductDetails()
        {
            // Arrange
            var salesOrder = new SalesOrderHeader
            {
                ProductNumber = 101,
                Name = "Sample Product",
                Quantity = 5,
                Price = 25.00
            };

            // Assert
            Assert.Equal(101, salesOrder.ProductNumber);
            Assert.Equal("Sample Product", salesOrder.Name);
            Assert.Equal(5, salesOrder.Quantity);
            Assert.Equal(25.00, salesOrder.Price);
        }
    }
}