SELECT        SalesOrderHeader.orderID, Products.itemNumber, Products.name, Products.description, Products.sellingPrice, Products.purchasePrice, SalesOrderLines.quantity
FROM            Addresses INNER JOIN
                         Customers ON Addresses.addressID = Customers.addressID INNER JOIN
                         Persons ON Addresses.addressID = Persons.addressID AND Customers.personID = Persons.personID INNER JOIN
                         SalesOrderHeader ON Customers.customerID = SalesOrderHeader.customerID INNER JOIN
                         SalesOrderLines ON SalesOrderHeader.orderID = SalesOrderLines.salesOrderHeadID INNER JOIN
                         Products ON SalesOrderLines.productID = Products.productID