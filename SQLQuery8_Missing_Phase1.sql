USE invetory_MS;

IF COL_LENGTH('Stock_Transaction', 'Zone_ID') IS NULL
BEGIN
    ALTER TABLE Stock_Transaction
    ADD Zone_ID UNIQUEIDENTIFIER NULL;

    ALTER TABLE Stock_Transaction
    ADD CONSTRAINT FK_Stock_Transaction_Location
        FOREIGN KEY (Zone_ID) REFERENCES Location(Zone_ID);
END;

IF COL_LENGTH('Item', 'Total_Quantity') IS NULL
BEGIN
    ALTER TABLE Item
    ADD Total_Quantity DECIMAL(10,2) NOT NULL DEFAULT 0;
END;
