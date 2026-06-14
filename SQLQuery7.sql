INSERT INTO Supplier 
    (Supplier_ID, Supplier_Name, Contact_Email, Contact_Phone, Address)
VALUES 
    (NEWID(), 'TechSolutions Ltd', 'contact@techsolutions.com', '+255 712 345 678', 'Dar es Salaam, Tanzania'),
    (NEWID(), 'Global Electronics', 'info@globalelectronics.co.tz', '+255 767 890 123', 'Arusha, Tanzania'),
    (NEWID(), 'OfficeMart Supplies', 'sales@officemart.co.tz', '+255 784 567 890', 'Mwanza, Tanzania'),
    (NEWID(), 'Computer World TZ', 'support@computerworld.co.tz', '+255 655 432 109', 'Dodoma, Tanzania'),
    (NEWID(), 'Elite IT Solutions', 'eliteit@company.com', '+255 711 223 344', 'Zanzibar, Tanzania');

    SELECT * FROM Supplier