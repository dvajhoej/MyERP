SELECT      
			Companies.name, 
			Addresses.street,
			Addresses.houseNumber,
			Addresses.zipCode,
			Addresses.city,
			Addresses.country,
			companies.currency
FROM Addresses 
full JOIN companies ON Addresses.addressID = companies.addressID