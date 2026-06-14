ALTER TABLE Stock_Transaction
ADD Zone_ID UNIQUEIDENTIFIER 

ALTER TABLE Stock_Transaction
ADD CONSTRAINT FK_StockTransaction_Location 
FOREIGN KEY (Zone_ID) REFERENCES Location(Zone_ID);