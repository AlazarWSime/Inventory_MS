Using inventory
-- 1. Role Table
CREATE TABLE Role(
Role_ID UNIQUEiDENTIFIER PRIMARY KEY DEFAULT NEWID(),
Role_Name NVARCHAR(50) NOT NULL
);

--2. User Table
CREATE TABLE [User] (
    [User_ID]     UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Role_ID     UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Role(Role_ID),
    Full_Name   NVARCHAR(100) NOT NULL,
    Username    NVARCHAR(50) NOT NULL UNIQUE,
    Password    NVARCHAR(255) NOT NULL,
    Created_By  UNIQUEIDENTIFIER FOREIGN KEY REFERENCES [User]([User_ID]),
    Created_On  DATETIME NOT NULL DEFAULT GETDATE()
);

-- 3. Item_Category Table
CREATE TABLE Item_Category (
    Category_ID    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Category_Name  NVARCHAR(100) NOT NULL
);

-- 4. Supplier Table
CREATE TABLE Supplier (
    Supplier_ID    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Supplier_Name  NVARCHAR(100) NOT NULL,
    Contact_Email  NVARCHAR(100),
    Contact_Phone  NVARCHAR(20),
    Address        NVARCHAR(255)
);

-- 5. Item Table
CREATE TABLE Item (
    Item_ID        UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Category_ID    UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Item_Category(Category_ID),
    Supplier_ID    UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Supplier(Supplier_ID),
    Item_Name      NVARCHAR(100) NOT NULL,
    Description    NVARCHAR(500),
    Price          DECIMAL(10,2) NOT NULL,
    Reorder_Level  INT NOT NULL
);

-- 6. Location Table
CREATE TABLE Location (
    Zone_ID      UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Zone_Name    NVARCHAR(100) NOT NULL,
    Description  NVARCHAR(255)
);

-- 7. Inventory_Stock Table
CREATE TABLE Inventory_Stock (
    Stock_ID          UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Item_ID           UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Item(Item_ID),
    Zone_ID           UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Location(Zone_ID),
    Quantity_On_Hand  DECIMAL(10,2) NOT NULL DEFAULT 0
);

-- 8. Stock_Transaction Table
CREATE TABLE Stock_Transaction (
    Transaction_ID    UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    User_ID           UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES [User](User_ID),
    Item_ID           UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES Item(Item_ID),
    Transaction_Type  NVARCHAR(20) NOT NULL
                      CHECK (Transaction_Type IN
                      ('Purchase','Sale','Transfer','Adjustment')),
    Quantity          DECIMAL(10,2) NOT NULL,
    Transaction_Date  DATETIME NOT NULL DEFAULT GETDATE()
);
use invetory_MS

ALTER TABLE Item
ADD Total_Quantity INT NOT NULL DEFAULT 0;

select * from Item