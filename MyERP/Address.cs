namespace MyERP
{
    // Define a class Address to represent an address
    public class Address
    {
        // Public properties to store the address details
        public int AddressID { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        // Constructor to initialize the address details
        public Address(string street, string houseNumber, string zipCode, string city, string country)
        {
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }

        // Public property to get or set the full address
        public string FullAddress
        {
            get
            {
                // Create a list to store the address parts
                List<string> addressParts = new List<string>();

                // Check if the street and house number are not empty
                if (!string.IsNullOrWhiteSpace(Street) && !string.IsNullOrWhiteSpace(HouseNumber))
                {
                    // Add the street and house number to the address parts
                    addressParts.Add($"{Street} {HouseNumber}".Trim());
                }
                // Check if the street is not empty
                else if (!string.IsNullOrWhiteSpace(Street))
                {
                    // Add the street to the address parts
                    addressParts.Add(Street.Trim());
                }
                // Check if the house number is not empty
                else if (!string.IsNullOrWhiteSpace(HouseNumber))
                {
                    // Add the house number to the address parts
                    addressParts.Add(HouseNumber.Trim());
                }

                // Check if the zip code is not empty
                if (!string.IsNullOrWhiteSpace(ZipCode))
                {
                    // Add the zip code to the address parts
                    addressParts.Add(ZipCode.Trim());
                }

                // Check if the city is not empty
                if (!string.IsNullOrWhiteSpace(City))
                {
                    // Add the city to the address parts
                    addressParts.Add(City.Trim());
                }

                // Check if the country is not empty
                if (!string.IsNullOrWhiteSpace(Country))
                {
                    // Add the country to the address parts
                    addressParts.Add(Country.Trim());
                }

                // Return the full address as a comma-separated string
                return addressParts.Count > 0 ? string.Join(", ", addressParts) : string.Empty;
            }

            set
            {
                // Check if the value is empty
                if (string.IsNullOrWhiteSpace(value))
                {
                    // Clear the address details
                    ClearAddress();
                    return;
                }

                // Split the value into address parts
                var addressParts = value.Split(',').Select(part => part.Trim()).ToArray();

                // Switch on the number of address parts
                switch (addressParts.Length)
                {
                    case 5:
                        // Set the street, house number, zip code, city, and country
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        ZipCode = addressParts[2];
                        City = addressParts[3];
                        Country = addressParts[4];
                        break;
                    case 4:
                        // Set the street, house number, zip code, and city
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        ZipCode = addressParts[2];
                        City = addressParts[3];
                        break;
                    case 3:
                        // Set the street, house number, and city
                        Street = addressParts[0];
                        HouseNumber = addressParts[1];
                        City = addressParts[2];
                        break;
                    case 2:
                        // Set the street and city
                        Street = addressParts[0];
                        City = addressParts[1];
                        break;
                    default:
                        // Clear the address details
                        ClearAddress();
                        break;
                }
            }
        }

        // Method to clear all address fields
        private void ClearAddress()
        {
            // Set all address fields to empty strings
            Street = string.Empty;
            HouseNumber = string.Empty;
            ZipCode = string.Empty;
            City = string.Empty;
            Country = string.Empty;
        }

        // Default constructor
        public Address()
        {
            
        }
    }
}