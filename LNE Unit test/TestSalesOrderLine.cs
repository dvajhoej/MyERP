using Xunit;
using MyERP;

namespace LNE_Unit_test
{
    public class TestSalesOrderLine
    {
        [Fact]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var orderLine = new SalesOrderLine();

            // Assert
            Assert.Equal(0, orderLine.ProductID);
            Assert.Null(orderLine.Name);
            Assert.Equal(0, orderLine.Quantity);
            Assert.Equal(0, orderLine.Price);
            Assert.Equal(0, orderLine.OrderID);
            Assert.Null(orderLine.Description);
        }

        [Fact]
        public void Constructor_WithParameters_ShouldInitializeCorrectly()
        {
            // Arrange
            var productID = 101;
            var name = "Test Product";
            var quantity = 5;
            var price = 20.00;

            // Act
            var orderLine = new SalesOrderLine(productID, name, quantity, price);

            // Assert
            Assert.Equal(productID, orderLine.ProductID);
            Assert.Equal(name, orderLine.Name);
            Assert.Equal(quantity, orderLine.Quantity);
            Assert.Equal(price, orderLine.Price);
        }

        [Fact]
        public void Amount_ShouldReturnCorrectCalculation()
        {
            // Arrange
            var orderLine = new SalesOrderLine
            {
                Quantity = 3,
                Price = 10.00
            };

            // Act
            var amount = orderLine.Amount;

            // Assert
            Assert.Equal(30.00, amount); // 3 * 10 = 30
        }

        [Fact]
        public void ShouldSetAndGetProperties()
        {
            // Arrange
            var orderLine = new SalesOrderLine
            {
                ProductID = 101,
                Name = "Product Name",
                Quantity = 10,
                Price = 15.50,
                OrderID = 202,
                Description = "Sample product description",
                Unit = UnitType.Stk
            };

            // Assert
            Assert.Equal(101, orderLine.ProductID);
            Assert.Equal("Product Name", orderLine.Name);
            Assert.Equal(10, orderLine.Quantity);
            Assert.Equal(15.50, orderLine.Price);
            Assert.Equal(202, orderLine.OrderID);
            Assert.Equal("Sample product description", orderLine.Description);
            Assert.Equal(UnitType.Stk, orderLine.Unit);
        }
    }
}
