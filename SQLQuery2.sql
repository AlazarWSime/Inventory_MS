select * from [Role]
SELECT * FROM Role;


-- If Role table is empty run this
INSERT INTO [Role] (Role_ID, Role_Name)
VALUES
--('124a1384-5056-4a08-9844-f9dc43435b78', 'System Administration'),
(NEWID(), 'Inventory Manager'),
(NEWID(), 'Warehouse Staff');


DELETE FROM [Role]

Delete From [Role] where Role_ID = '8A465B68-7EE0-426F-8F4A-7D7D4148493A'

Select * from [User]

Delete From [User] Where [User_ID] = '22C3FCF7-722B-4AE3-AAFF-7472A0BB0305';