ALTER TABLE Stock_Transaction
ADD CONSTRAINT FK_StockTransaction_Location 
FOREIGN KEY (Zone_ID) REFERENCES Location(Zone_ID);

INSERT INTO Location (Zone_ID, Zone_Name, Description)
VALUES 
    (NEWID(), 'Main Warehouse', 'Primary storage area - Zone A'),
    (NEWID(), 'Zone B', 'Secondary storage - Electronics'),
    (NEWID(), 'Zone C', 'Perishable Goods Storage'),
    (NEWID(), 'Retail Shelf', 'Display area for sales'),
    (NEWID(), 'Receiving Bay', 'Incoming goods area');