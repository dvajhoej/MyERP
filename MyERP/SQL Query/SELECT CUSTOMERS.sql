SELECT        Customers.customerID, Persons.firstname, Persons.lastname, Addresses.street, Addresses.houseNumber, Addresses.zipCode, Addresses.city, Persons.phone, Persons.email, Customers.lastPurchaseDate
FROM            Addresses INNER JOIN
                         Customers ON Addresses.addressID = Customers.addressID INNER JOIN
                         Persons ON Addresses.addressID = Persons.addressID AND Customers.personID = Persons.personID