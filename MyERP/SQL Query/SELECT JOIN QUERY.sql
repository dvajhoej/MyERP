SELECT 
    SalesOrderHeader.orderID, 
    SalesOrderHeader.customerID, 
    SalesOrderHeader.status, 
    Products.productID, 
    Products.name AS productName, 
    Products.description, 
    Products.sellingPrice, 
    Products.purchasePrice, 
    Products.location, 
    Products.unit, 
    SalesOrderLines.quantity
FROM SalesOrderHeader
JOIN SalesOrderLines ON SalesOrderHeader.orderID = SalesOrderLines.salesOrderHeadID
JOIN Products ON SalesOrderLines.productID = Products.productID
WHERE SalesOrderLines.salesOrderHeadID = 5;
