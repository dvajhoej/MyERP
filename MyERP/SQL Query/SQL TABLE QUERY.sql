---- DROP TABELS FOR RESET,

DROP TABLE IF EXISTS SalesOrderLines
DROP TABLE IF EXISTS SalesOrderHeader
DROP TABLE IF EXISTS Products
DROP TABLE IF EXISTS Customers
DROP TABLE IF EXISTS Persons
DROP TABLE IF EXISTS Addresses


---- #B2 

CREATE TABLE Products (
    productID INT PRIMARY KEY IDENTITY(1,1),
	itemNumber INT UNIQUE,
    name VARCHAR(50),
    description TEXT,
    sellingPrice DECIMAL(10,2),
    purchasePrice DECIMAL(10,2),
    location VARCHAR(4),
    quantity DECIMAL(10,2),
    unit VARCHAR(6),
    CONSTRAINT chk_unit CHECK (unit IN ('Stk', 'Pakker', 'Time', 'Meter')),
    CONSTRAINT chk_location CHECK (location LIKE '[A-Za-z0-9][A-Za-z0-9][A-Za-z0-9][A-Za-z0-9]')
);

 --#B3


CREATE TABLE Addresses (
	addressID INT PRIMARY KEY IDENTITY(1,1),
	street VARCHAR(58 ),
	houseNumber VARCHAR(21),
	zipCode VARCHAR(10),
	city VARCHAR(168),
	country VARCHAR(56)

	);



--#B4

CREATE TABLE Persons (
	personID INT PRIMARY KEY IDENTITY(1,1),
	firstname VARCHAR(50),
	lastname VARCHAR(100),
	addressID INT FOREIGN KEY  REFERENCES Addresses(addressID),
	email NVARCHAR(100),
	phone VARCHAR(25)
	);


--#B5

CREATE TABLE Customers (
	customerID INT PRIMARY KEY IDENTITY(1,1),
	personID INT FOREIGN KEY  REFERENCES Persons(PersonID),
	addressID INT FOREIGN KEY  REFERENCES Addresses(addressID),
	lastPurchaseDate DATETIME NULL
	);



--#B6

CREATE TABLE SalesOrderHeader (
	orderID INT PRIMARY KEY IDENTITY(1,1),
	creationDate DATETIME DEFAULT GETDATE(),
	completionDate DATETIME,
	customerID INT FOREIGN KEY  REFERENCES Customers(customerID),
	status VARCHAR(15),
	CONSTRAINT chk_status CHECK (status IN ('None', 'Created', 'Confirmed', 'Packed', 'Completed')),
	);



--#B7

CREATE TABLE SalesOrderLines (
	salesOrderID INT PRIMARY KEY IDENTITY(1,1),
    salesOrderHeadID INT FOREIGN KEY REFERENCES SalesOrderHeader(OrderID),
    productID INT FOREIGN KEY REFERENCES Products(productID),
    quantity DECIMAL(10,2)
);

