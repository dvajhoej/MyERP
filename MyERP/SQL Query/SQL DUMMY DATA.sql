-- DUMMY DATA,


INSERT INTO Products (itemNumber, name, description, sellingPrice, purchasePrice, location, quantity, unit)
VALUES 
(1, 'Product A', 'Description A', 100.00, 70.00, 'A123', 10, 'Stk'),
(2, 'Product B', 'Description B', 150.00, 90.00, 'B456', 5, 'Pakker'),
(3, 'Product C', 'Description C', 200.00, 120.00, 'C789', 20, 'Meter'),
(4, 'Product D', 'Description D', 50.00, 35.00, 'D012', 15, 'Time'),
(5, 'Product E', 'Description E', 75.00, 45.00, 'E345', 25, 'Stk');

GO

INSERT INTO Addresses (street, houseNumber, zipCode, city, country)
VALUES 
('Main St', '12A', '12345', 'City A', 'Country A'),
('2nd Ave', '5B', '67890', 'City B', 'Country B'),
('Elm St', '22', '54321', 'City C', 'Country A'),
('Oak St', '14C', '09876', 'City D', 'Country C'),
('Pine St', '33D', '13579', 'City E', 'Country D'),
('Nørregade', '123', '1000', 'København', 'Danmark'),
('Østergade', '45B', '5000', 'Odense', 'Danmark'),
('Søndergade', '7', '8000', 'Aarhus', 'Danmark'),
('Vesterbrogade', '89', '9000', 'Aalborg', 'Danmark'),
('Hovedgaden', '56A', '4000', 'Roskilde', 'Danmark');

GO

INSERT INTO Persons (firstname, lastname, addressID, email, phone)
VALUES 
('John', 'Doe', 1, 'john.doe@example.com', '123-4567'),
('Jane', 'Smith', 2, 'jane.smith@example.com', '987-6543'),
('Michael', 'Johnson', 3, 'michael.johnson@example.com', '456-7890'),
('Emily', 'Davis', 4, 'emily.davis@example.com', '321-6548'),
('Sarah', 'Brown', 5, 'sarah.brown@example.com', '654-3210');


GO

INSERT INTO Customers (personID, addressID, lastPurchaseDate)
VALUES 
(1, 1, '2024-01-05'),
(2, 2, '2024-02-10'),
(3, 3, '2024-03-15'),
(4, 4, '2024-04-20'),
(5, 5, '2024-05-25');


GO

INSERT INTO SalesOrderHeader (creationDate, completionDate, customerID, status)
VALUES 
('2024-06-01', '2024-06-02', 1, 'Completed'),
('2024-06-03', '2024-06-04', 2, 'Packed'),
('2024-06-05', '2024-06-06', 3, 'Confirmed'),
('2024-06-07', '2024-06-08', 4, 'Created'),
('2024-06-09', NULL, 5, 'None');


GO

INSERT INTO SalesOrderLines (salesOrderHeadID, productID, quantity)
VALUES 
(1, 1, 2),
(1, 2, 3),
(1, 3, 4),
(1, 4, 1),
(1, 5, 5),
(2, 1, 2),
(2, 2, 3),
(2, 3, 4),
(2, 4, 1),
(2, 5, 5),
(3, 1, 2),
(3, 2, 3),
(3, 3, 4),
(3, 4, 1),
(3, 5, 5),
(4, 1, 2),
(4, 2, 3),
(4, 3, 4),
(4, 4, 1),
(4, 5, 5),
(5, 1, 2),
(5, 2, 3),
(5, 3, 4),
(5, 4, 1),
(5, 5, 5);

INSERT INTO companies (name, addressID, currency)
VALUES 
('Flødebolle Fabrikken', 6, 'DKK'),
('Snorkel Snedkeri', 7, 'SEK'),
('Rundstykke Rockets', 8, 'USD'),
('Pølse Paradis', 9, 'EUR'),
('Kaffe & Klapvogne A/S', 10, 'DKK');

