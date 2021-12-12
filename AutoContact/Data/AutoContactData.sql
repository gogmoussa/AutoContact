
/* * * INSERT FIRST: NO DEPENDENCIES * * */

-- Car Table
INSERT INTO dbo.Car (VIN, Make, Model, Colour, Odometer)
VALUES ('SCBFR7ZA5CC072256', 'Bently', 'Continental', 'Black', 52000);
INSERT INTO dbo.Car (VIN, Make, Model, Colour, Odometer)
VALUES ('2GCEC19Z1S1244154', 'Chevrolet', 'GMT-400', 'Blue', 163000);
INSERT INTO dbo.Car (VIN, Make, Model, Colour, Odometer)
VALUES ('1G4HP54KX24151104', 'Buick', 'LeSabre', 'Brown', 86000);

-- LoanerCar Table
INSERT INTO dbo.LoanerCar (VIN, Make, Model, Colour, Odometer)
VALUES ('JH4DA1740JS012019', 'Acura', 'Integra', 'Red', 32000);
INSERT INTO dbo.LoanerCar (VIN, Make, Model, Colour, Odometer)
VALUES ('1GCHK29U86E255778', 'Chevrolet', 'Silverado', 'Silver', 75000);
INSERT INTO dbo.LoanerCar (VIN, Make, Model, Colour, Odometer)
VALUES ('1GKDT13S852309288', 'GMC', 'Envoy', 'Green', 64000);

-- Location Table

-- Address Table
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('3', '', 'Norwood Drive', 'London', 'Ontario', 'Canada');
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('14', '', 'Jarvis Drive', 'St. Thomas', 'Ontario', 'Canada');
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('21', '', 'Lime Court', 'Hamilton', 'Ontario', 'Canada');
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('36', '', 'Hole Lane', 'Brampton', 'Ontario', 'Canada');
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('45', '', 'Common End', 'Toronto', 'Ontario', 'Canada');
INSERT INTO dbo.Address (StreetNum, UnitNum, StreetName, CityName, ProvinceName, Country)
VALUES ('52', '', 'Meadowbrook Road', 'Ottawa', 'Ontario', 'Canada');


/* * * INSERT SECOND: EMAIL / ADDRESS DEPENDENCIES * * */

-- Client Table
INSERT INTO dbo.Client (FirstName, LastName, DriverLicence, BirthDate, AddressId, Email, PhoneNum, HashPass, HashSalt)
VALUES ('Karen', 'Komplaint', 'K9576-16858-72512', '1987-01-12', 4, 'karen.komplaint@aol.com', '555-555-6547',
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');
INSERT INTO dbo.Client (FirstName, LastName, DriverLicence, BirthDate, AddressId, Email, PhoneNum, HashPass, HashSalt)
VALUES ('Ken', 'Komplaint', 'K3164-38618-53426', '1985-10-26', 5, 'ken.komplaint@aol.com', '555-555-7894',
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');
INSERT INTO dbo.Client (FirstName, LastName, DriverLicence, BirthDate, AddressId, Email, PhoneNum, HashPass, HashSalt)
VALUES ('Sally', 'Struthers', 'S1678-24564-75247', '1947-08-28', 6, 'sally.struthers@gmail.com', '555-555-9872',
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');

-- Employee Table
INSERT INTO dbo.Employee (FirstName, LastName, AddressId, Email, PhoneNum, EmployeeSIN, Manager, HireDate, TerminationDate, TerminationReason, HashPass, HashSalt)
VALUES ('Owen', 'Owner', 1, 'owen.owner@gmail.com', '555-555-0123', '123456789', 0, '2019-01-01', null, null,
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');
INSERT INTO dbo.Employee (FirstName, LastName, AddressId, Email, PhoneNum, EmployeeSIN, Manager, HireDate, TerminationDate, TerminationReason, HashPass, HashSalt)
VALUES ('Mark', 'Mechanic', 2, 'mark.mechanic@yahoo.com', '555-555-3210', '147258369', 0, '2020-02-02', null, null,
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');
INSERT INTO dbo.Employee (FirstName, LastName, AddressId, Email, PhoneNum, EmployeeSIN, Manager, HireDate, TerminationDate, TerminationReason, HashPass, HashSalt)
VALUES ('Ralph', 'Reception', 3, 'ralph.reception@hotmail.com', '555-555-4567', '159147123', 0, '2021-03-03', null, null,
'01FDB598C69C27952944E7DAED976687BF1FF5EC10C40A07546F50FBA27757B7B2B70FBFE0C13F8C7C0229A6BF3F747333C0776CF57F8812C6D179B25176A2CE', '6FD7D30C536ECC0414B413992D2111D9FFE47D748BDC4AD9');


/* * * INSERT THIRD: CLIENT / EMPLOYEE DEPENDENCIES * * */

-- CarClient Table
INSERT INTO dbo.CarClient (CarId, ClientId, IsOwner)
VALUES (1, 1, 1);
INSERT INTO dbo.CarClient (CarId, ClientId, IsOwner)
VALUES (2, 2, 0);
INSERT INTO dbo.CarClient (CarId, ClientId, IsOwner)
VALUES (3, 3, 1);

-- Department Table (Human Resoruces? Drivers? Legal Team?)
INSERT INTO dbo.Department (DepartmentName, EmployeeId)
VALUES ('Owners', 1);
INSERT INTO dbo.Department (DepartmentName, EmployeeId)
VALUES ('Mechanics', 2);
INSERT INTO dbo.Department (DepartmentName, EmployeeId)
VALUES ('Administration', 3);

-- AccessLevel Table
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (null, 1, 'Admin');
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (null, 2, 'Employee');
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (null, 3, 'Employee');
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (1, null, 'Client');
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (2, null, 'Client');
INSERT INTO dbo.AccessLevel (ClientId, EmployeeId, AccessLevel)
VALUES (3, null, 'Client'); 


/* * * (NEW!!!) INSERT NEXT - INVENTORY * * */ 

-- Category Table
INSERT INTO dbo.Category (Category)
VALUES ('BMW Stuff');
INSERT INTO dbo.Category (Category)
VALUES ('Ford Stuff');
INSERT INTO dbo.Category (Category)
VALUES ('Toyota Stuff');

-- Vendor Table 	
INSERT INTO dbo.Vendor (Name, AddressId, Phone, Email, MainContact, Type)
VALUES ('BMW Supplier', 1, '555-555-5555', 'bmw.supplies@gmail.com', 'Frank', 'Trusted');
INSERT INTO dbo.Vendor (Name, AddressId, Phone, Email, MainContact, Type)
VALUES ('Ford Supplier', 2, '555-555-5555', 'ford.supplies@gmail.com', 'Bob', 'Trusted');
INSERT INTO dbo.Vendor (Name, AddressId, Phone, Email, MainContact, Type)
VALUES ('Toyota Supplier', 3, '555-555-5555', 'toyota.supplies@gmail.com', 'Fred', 'Trusted');

-- Part Table
INSERT INTO dbo.Part (VendorId, CategoryId, Name, Description, CostPrice, ReorderQty, EconomicalOrderQty, QtyOnHand, QtyOnOrder)
VALUES (1, 1, 'BMW SPlug', 'BMW Spark Plug', 9.99, 10, 20, 12, 0);
INSERT INTO dbo.Part (VendorId, CategoryId, Name, Description, CostPrice, ReorderQty, EconomicalOrderQty, QtyOnHand, QtyOnOrder)
VALUES (2, 2, 'F BPad', 'Ford Brake Pad', 19.99, 5, 10, 8, 0);
INSERT INTO dbo.Part (VendorId, CategoryId, Name, Description, CostPrice, ReorderQty, EconomicalOrderQty, QtyOnHand, QtyOnOrder)
VALUES (3, 3, 'T HLight', 'Toyota Headlight', 49.99, 6, 8, 10, 0);

/* * * INSERT LAST - MULTIPLE DEPENDENCIES * * */

-- Invoice Table (Time Datatype May Require Changing)
INSERT INTO dbo.Invoice (EmployeeId, LoanerCarId, PartId, Cost, HoursWorked, CreatedDate, CancelledDate, CompletedDate, InvoiceDate, PaidDate)
VALUES (2, 1, 1, '49.99', '8.5', '2021-01-14', null, '2021-01-13', '2021-01-14', '2021-01-15');
INSERT INTO dbo.Invoice (EmployeeId, LoanerCarId, PartId, Cost, HoursWorked, CreatedDate, CancelledDate, CompletedDate, InvoiceDate, PaidDate)
VALUES (2, 2, 2, '59.99', '9.0', '2021-02-15', null, '2021-02-14', '2021-02-15', '2021-02-16');
INSERT INTO dbo.Invoice (EmployeeId, LoanerCarId, PartId, Cost, HoursWorked, CreatedDate, CancelledDate, CompletedDate, InvoiceDate, PaidDate)
VALUES (2, 3, 3, '69.99','10.5', '2021-03-16', null, '2021-03-15', '2021-03-16', '2021-03-17');

-- Appointment Table
INSERT INTO dbo.Appointment (AppointmentDate, AppointmentStartTime, BookedAtTime, Message, BookingEmployeeId, ClientId, CarId)
VALUES ('2021-12-14', '2021-12-14 13:30', '2021-09-06', 'Oil change', 3, 1, 1);
INSERT INTO dbo.Appointment (AppointmentDate, AppointmentStartTime, BookedAtTime, Message, BookingEmployeeId, ClientId, CarId)
VALUES ('2021-12-15', '2021-12-15 14:00', '2021-10-07', 'Car wont start', 3, 2, 2);
INSERT INTO dbo.Appointment (AppointmentDate, AppointmentStartTime, BookedAtTime, Message, BookingEmployeeId, ClientId, CarId)
VALUES ('2021-12-16', '2021-12-16 14:30', '2021-11-08', 'The brakes screech', 3, 3, 3);

-- AppointmentInvoice Table
INSERT INTO dbo.AppointmentInvoice (AppointmentId, InvoiceId)
VALUES (1, 1);
INSERT INTO dbo.AppointmentInvoice (AppointmentId, InvoiceId)
VALUES (2, 2);
INSERT INTO dbo.AppointmentInvoice (AppointmentId, InvoiceId)
VALUES (3, 3);

-- Purchase Order Table
INSERT INTO dbo.PurchaseOrder (VendorId, Amount, PODate, CancelledDate)
VALUES (1, 99.99, '2021-09-06', null);
INSERT INTO dbo.PurchaseOrder (VendorId, Amount, PODate, CancelledDate)
VALUES (2, 199.99, '2021-09-06', null);
INSERT INTO dbo.PurchaseOrder (VendorId, Amount, PODate, CancelledDate)
VALUES (3, 299.99, '2021-09-06', null);

-- PO Line Item Table
INSERT INTO dbo.PurchaseOrderLineItem (PurchaseOrderId, PartId, Qty, Price)
VALUES (1, 1, 3, 19.99);
INSERT INTO dbo.PurchaseOrderLineItem (PurchaseOrderId, PartId, Qty, Price)
VALUES (2, 2, 5, 29.99);
INSERT INTO dbo.PurchaseOrderLineItem (PurchaseOrderId, PartId, Qty, Price)
VALUES (3, 3, 6, 39.99);