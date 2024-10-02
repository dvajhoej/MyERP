namespace MyERP
{
    // Define a class Person to represent a person
    public class Person : Address
    {
        // Public properties to store the person's details
        public int PersonID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

        // Public property to get or set the person's full name
        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                // Split the full name into first and last names
                var nameParts = value?.Split(' ');
                if (nameParts != null && nameParts.Length > 1)
                {
                    // Set the first and last names
                    FirstName = nameParts[0];
                    LastName = string.Join(" ", nameParts.Skip(1));
                }
                else if (nameParts?.Length == 1)
                {
                    // Set the first name and clear the last name
                    FirstName = nameParts[0];
                    LastName = string.Empty;
                }
                else
                {
                    // Clear the first and last names
                    FirstName = string.Empty;
                    LastName = string.Empty;
                }
            }
        }

        // Default constructor
        public Person()
        {
            
        }

        // Constructor to initialize the person's details with address
        public Person(string firstName, string lastName, string email, string phone, string street, string housenumber, string zipcode, string city, string country)
        {
            // Set the person's details
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;

            // Set the address details
            Street = street;
            HouseNumber = housenumber;
            ZipCode = zipcode;
            City = city;
            Country = country;
        }

        // Constructor to initialize the person's details without address
        public Person(string firstName, string lastName, string email, string phone)
        {
            // Set the person's details
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        // Override the ToString method to provide a string representation of the person
        public override string ToString()
        {
            // Return a string that includes the person's details
            return $"{FirstName} {LastName}, Email: {Email}, Phone: {Phone}";
        }
    }
}