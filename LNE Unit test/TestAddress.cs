using MyERP;

namespace LNE_Unit_test
{
    public class TestAddress
    {
        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues()
        {
            var address = new Address();


            Assert.Null(address.Street);
            Assert.Null(address.HouseNumber);
            Assert.Null(address.ZipCode);
            Assert.Null(address.City);
            Assert.Null(address.Country);
            Assert.Equal("", address.FullAddress);
        }

        [Fact]
        public void ParameterizedConstructor_ShouldAssignValuesToProperties()
        {
            var address = new Address("Street", "51", "BE-9500", "Testrup", "Danmark");

            Assert.Equal("Street", address.Street);
            Assert.Equal("51", address.HouseNumber);
            Assert.Equal("BE-9500", address.ZipCode);
            Assert.Equal("Testrup", address.City);
            Assert.Equal("Danmark", address.Country);
        }

        [Fact]
        public void FullAddress_ShouldCombineStreetHouseNumberZipCodeCityCountry()
        {
            var address = new Address("Street", "51", "BE-9500", "Testrup", "Danmark");

            Assert.Equal("Street 51, BE-9500, Testrup, Danmark", address.FullAddress);
        }

        [Fact]
        public void FullAddress_WithOnlyStreetAndCity_ShouldAssignToStreetAndCityLeaveHousenumberZipCodeCountryEmpty()
        {
            var address = new Address();
            address.FullAddress = "Street, Testrup";

            Assert.Equal("Street", address.Street);
            Assert.Equal("Testrup", address.City);
            Assert.Null(address.HouseNumber);
            Assert.Null(address.ZipCode);
            Assert.Null(address.Country);
            Assert.Equal("Street, Testrup", address.FullAddress);
        }

        [Fact]
        public void List_AddAndRemovePersons_ShouldUpdateCorrectly()
        {
            var address1 = new Address();
            var address2 = new Address();
            var address3 = new Address();

            List<Address> addresses = new List<Address>();

            Assert.Empty(addresses);

            addresses.Add(address1);

            Assert.Contains(address1, addresses);
            Assert.Single(addresses);

            addresses.Add(address2);
            addresses.Add(address3);
            Assert.Equal(3, addresses.Count);
            Assert.Contains(address3, addresses);

            addresses.Remove(address2);

            Assert.DoesNotContain(address2, addresses);
        }
    }
}