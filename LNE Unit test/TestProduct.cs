using System;
using Xunit;
using MyERP;

namespace LNE_Unit_test
{
    public class TestProduct
    {
        [Fact]
        public void CalculateMarginPercentage_ValidPrices_ReturnsCorrectMargin()
        {
            // Arrange
            var product = new Product
            {
                SellingPrice = 200.0,
                PurchasePrice = 100.0
            };

            // Act
            var result = product.CalculateMarginPercentage();

            // Assert
            Assert.Equal(100.0, result);
        }

        [Fact]
        public void CalculateMarginPercentage_PurchasePriceIsZero_ThrowsDivideByZeroException()
        {
            // Arrange
            var product = new Product
            {
                SellingPrice = 200.0,
                PurchasePrice = 0.0
            };

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => product.CalculateMarginPercentage());
        }

        [Fact]
        public void Profit_ValidPrices_ReturnsCorrectProfit()
        {
            // Arrange
            var product = new Product
            {
                SellingPrice = 150.0,
                PurchasePrice = 100.0
            };

            // Act
            var result = product.Profit;

            // Assert
            Assert.Equal(50.0, result);
        }

        [Fact]
        public void Location_ValidAlphanumericValue_SetsLocation()
        {
            // Arrange
            var product = new Product();

            // Act
            product.Location = "A1B2";

            // Assert
            Assert.Equal("A1B2", product.Location);
        }

        [Fact]
        public void Location_InvalidLength_ThrowsArgumentException()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => product.Location = "A1B");
            Assert.Equal("Location must be exactly 4 alphanumeric characters.", exception.Message);
        }

        [Fact]
        public void Location_NonAlphanumericValue_ThrowsArgumentException()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => product.Location = "A!B2");
            Assert.Equal("Location must be exactly 4 alphanumeric characters.", exception.Message);
        }

        [Fact]
        public void MarginPercentage_PurchasePriceZero_ReturnsNA()
        {
            // Arrange
            var product = new Product
            {
                SellingPrice = 150.0,
                PurchasePrice = 0.0
            };

            // Act
            var result = product.MarginPercentage;

            // Assert
            Assert.Equal("N/A", result);
        }

        [Fact]
        public void GetMarginPercentageFormatted_ValidPrices_ReturnsFormattedPercentage()
        {
            // Arrange
            var product = new Product
            {
                SellingPrice = 300.0,
                PurchasePrice = 150.0
            };

            // Act
            var result = product.GetMarginPercentageFormatted();

            // Assert
            Assert.Equal("100,00%", result);
        }

        [Fact]
        public void FullProductConstructor_InitializesAllPropertiesCorrectly()
        {
            // Arrange
            var product = new Product(1, "Test Product", "Test Description", 250.0, 150.0, "A1B2", 50, UnitType.Stk);

            // Assert
            Assert.Equal(1, product.ProductNumber);
            Assert.Equal("Test Product", product.Name);
            Assert.Equal("Test Description", product.Description);
            Assert.Equal(250.0, product.SellingPrice);
            Assert.Equal(150.0, product.PurchasePrice);
            Assert.Equal("A1B2", product.Location);
            Assert.Equal(50, product.QuantityInStock);
            Assert.Equal(UnitType.Stk, product.Unit);
        }
    }
}
