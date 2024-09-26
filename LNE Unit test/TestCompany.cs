using Xunit;
using MyERP;
using System;

namespace LNE_Unit_test
{
    public class TestCompany
    {
        [Fact]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            var companyName = "Tech Corp";
            var currency = Currency.USD;

            // Act
            var company = new Company(companyName, currency);

            // Assert
            Assert.Equal(companyName, company.CompanyName);
            Assert.Equal(currency, company.Currency);
        }

        [Fact]
        public void DefaultConstructor_ShouldSetPropertiesToDefaults()
        {
            // Arrange & Act
            var company = new Company();

            // Assert
            Assert.Equal(0, company.CompanyID); 
            Assert.Null(company.CompanyName);
            Assert.Equal(default(Currency), company.Currency);
        }

        [Fact]
        public void ShouldSetAndGetCompanyID()
        {
            // Arrange
            var company = new Company();

            // Act
            company.CompanyID = 1001;

            // Assert
            Assert.Equal(1001, company.CompanyID);
            
        }

        [Fact]
        public void ShouldSetAndGetCompanyName()
        {
            // Arrange
            var company = new Company();

            // Act
            company.CompanyName = "Finance Inc.";

            // Assert
            Assert.Equal("Finance Inc.", company.CompanyName);
        }

        [Fact]
        public void ShouldSetAndGetCurrency()
        {
            // Arrange
            var company = new Company();

            // Act
            company.Currency = Currency.EUR;

            // Assert
            Assert.Equal(Currency.EUR, company.Currency);
        }
    }
}
