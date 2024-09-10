SELECT        Companies.name, Addresses.street, Addresses.houseNumber, Addresses.zipCode, Addresses.city, Addresses.country, companies.currency
FROM            Addresses INNER JOIN
                         companies ON Addresses.addressID = companies.addressID