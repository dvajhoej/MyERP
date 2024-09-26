using Xunit;
using MyERP;
using System;

namespace LNE_Unit_test
{
    public class TestCustomer
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var firstName = "John";
            var lastName = "Doe";
            var email = "john.doe@example.com";
            var phone = "1234567890";
            var lastPurchaseDate = new DateTime(2023, 1, 15);

            // Act
            var customer = new Customer(firstName, lastName, email, phone, lastPurchaseDate);

            // Assert
            Assert.Equal(firstName, customer.FirstName);
            Assert.Equal(lastName, customer.LastName);
            Assert.Equal(email, customer.Email);
            Assert.Equal(phone, customer.Phone);
            Assert.Equal(lastPurchaseDate, customer.LastPurchaseDate);
        }

        [Fact]
        public void DefaultConstructor_ShouldSetPropertiesToDefaults()
        {
            // Arrange & Act
            var customer = new Customer();

            // Assert
            Assert.Equal(0, customer.CustomerID); // Default int value is 0
            Assert.Null(customer.LastPurchaseDate); // Default for nullable DateTime is null
            Assert.Null(customer.FirstName);
            Assert.Null(customer.LastName);
            Assert.Null(customer.Email);
            Assert.Null(customer.Phone);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString_WhenLastPurchaseDateIsNotNull()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerID = 1,
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Phone = "9876543210",
                LastPurchaseDate = new DateTime(2023, 5, 20)
            };

            // Act
            var result = customer.ToString();

            // Assert
            Assert.Equal("Jane Doe, Email: jane.doe@example.com, Phone: 9876543210, Customer Number: 1, Last Purchase: 20-05-2023", result);
        }

        [Fact]
        public void ToString_ShouldReturnFormattedString_WhenLastPurchaseDateIsNull()
        {
            // Arrange
            var customer = new Customer
            {
                CustomerID = 2,
                FirstName = "Tom",
                LastName = "Smith",
                Email = "tom.smith@example.com",
                Phone = "1122334455",
                LastPurchaseDate = null
            };

            // Act
            var result = customer.ToString();

            // Assert
            Assert.Equal("Tom Smith, Email: tom.smith@example.com, Phone: 1122334455, Customer Number: 2, Last Purchase: ", result);
        }
    }
}
