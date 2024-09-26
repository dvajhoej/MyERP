using MyERP;

namespace LNE_Unit_test
{
    public class TestPerson
    {
        [Fact]
        public void DefaultConstructor_ShouldInitializePropertiesToDefaultValues()
        {
            var person = new Person();

            Assert.Null(person.FirstName);
            Assert.Null(person.LastName);
            Assert.Null(person.Email);
            Assert.Null(person.Phone);
            Assert.Null(person.Street);
            Assert.Null(person.HouseNumber);
            Assert.Null(person.ZipCode);
            Assert.Null(person.City);
            Assert.Null(person.Country);
            Assert.Equal("", person.FullName);
        }

        [Fact]
        public void ParameterizedConstructor_ShouldAssignValuesToProperties()
        {
            var person = new Person("John", "Doe", "john.doe@example.com", "1234567890", "testvej", "51", "6000", "Testrup", "Test");

            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
            Assert.Equal("john.doe@example.com", person.Email);
            Assert.Equal("1234567890", person.Phone);
            Assert.Equal("John Doe", person.FullName);
            Assert.Equal("testvej", person.Street);
            Assert.Equal("51", person.HouseNumber);
            Assert.Equal("6000", person.ZipCode);
            Assert.Equal("Testrup", person.City);
            Assert.Equal("Test", person.Country);


        }

        [Fact]
        public void FullName_ShouldCombineFirstNameAndLastName()
        {
            var person = new Person { FirstName = "John", LastName = "Doe" };

            Assert.Equal("John Doe", person.FullName);
        }

        [Fact]
        public void FullNameSetter_ShouldSplitAndAssignFirstNameAndLastName()
        {
            var person = new Person();
            person.FullName = "John Doe";

            Assert.Equal("John", person.FirstName);
            Assert.Equal("Doe", person.LastName);
        }

        [Fact]
        public void FullNameSetter_WithSingleName_ShouldAssignToFirstNameAndLeaveLastNameEmpty()
        {
            var person = new Person();
            person.FullName = "John";

            Assert.Equal("John", person.FirstName);
            Assert.Equal(string.Empty, person.LastName);
        }

        [Fact]
        public void CheckList()
        {
            var person1 = new Person();
            var person2 = new Person();
            var person3 = new Person();
            
            List<Person> persons = new List<Person>();

            Assert.Equal(0, persons.Count);

            persons.Add(person1);

            Assert.Contains(person1, persons);
            Assert.Equal(1, persons.Count);

            persons.Add(person2);
            persons.Add(person3);
            Assert.Equal(3, persons.Count);
            Assert.Contains(person3, persons);

            persons.Remove(person2);

            Assert.DoesNotContain(person2, persons);
        }


    }

}