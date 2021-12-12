/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/

/*
DROP TABLE dbo.CarClient
GO
DROP TABLE dbo.AccessLevel
GO
DROP TABLE dbo.Department
GO
DROP TABLE dbo.PurchaseOrderLineItem
GO
DROP TABLE dbo.PurchaseOrder
GO
DROP TABLE dbo.AppointmentInvoice
GO
DROP TABLE dbo.Appointment
GO
DROP TABLE dbo.Invoice
GO
DROP TABLE dbo.Employee
GO
DROP TABLE dbo.Client
GO
DROP TABLE dbo.Car
GO
DROP TABLE dbo.LoanerCar
GO
DROP TABLE dbo.Part
GO
DROP TABLE dbo.Category
GO
DROP TABLE dbo.Vendor
GO
DROP TABLE dbo.Address
GO
*/

BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Car(
	CarId bigint NOT NULL IDENTITY(1,1),
	VIN varchar(17) NOT NULL,
	Make varchar(50) NOT NULL,
	Model varchar(50) NOT NULL,
	Colour varchar(50) NOT NULL,
	Odometer bigint NOT NULL,
CONSTRAINT PK_Car PRIMARY KEY CLUSTERED(
	CarId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Appointment(
	AppointmentId bigint NOT NULL IDENTITY(1,1),
	AppointmentDate datetime NOT NULL,
	AppointmentStartTime datetime NOT NULL,
	BookedAtTime datetime NOT NULL,
	Message varchar(100) NOT NULL,
	BookingEmployeeId bigint NOT NULL,
	ClientId bigint NULL,
	CarId bigint NOT NULL,
CONSTRAINT PK_Appointment PRIMARY KEY CLUSTERED(
	AppointmentId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Appointment] WITH CHECK ADD CONSTRAINT [FK_Appointment_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([CarId])
GO
ALTER TABLE [dbo].[Appointment] CHECK CONSTRAINT [FK_Appointment_Car]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Address(
    AddressId bigint NOT NULL IDENTITY(1,1),
	StreetNum varchar(10) NOT NULL,
	UnitNum varchar(10) NOT NULL,
	StreetName nvarchar(50) NOT NULL,
	CityName nvarchar(50) NOT NULL,
	ProvinceName nvarchar(50) NOT NULL,
	Country nvarchar(50) NOT NULL,
CONSTRAINT PK_Address PRIMARY KEY CLUSTERED(
	AddressId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.Client(
    ClientId bigint NOT NULL IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
	DriverLicence varchar(17) NULL,
	BirthDate date NULL,
	AddressId bigint NOT NULL,
    HashPass varchar(max) NOT NULL,
    HashSalt varchar(max) NOT NULL,
	Email nvarchar(50) NOT NULL,
	PhoneNum nvarchar(20) NOT NULL,
CONSTRAINT PK_Client PRIMARY KEY CLUSTERED(
	ClientId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] WITH CHECK ADD CONSTRAINT [FK_Client_Address] FOREIGN KEY([AddressId]) 
REFERENCES [dbo].[Address] ([AddressId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Address]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.CarClient(
	CarClientId bigint NOT NULL IDENTITY(1,1),
	CarId bigint NOT NULL,
	ClientId bigint NOT NULL,
	IsOwner bit NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CarClient] WITH CHECK ADD CONSTRAINT [FK_CarClient_Car] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([CarId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarClient] CHECK CONSTRAINT [FK_CarClient_Car]
GO
ALTER TABLE [dbo].[CarClient] WITH CHECK ADD CONSTRAINT [FK_CarClient_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CarClient] CHECK CONSTRAINT [FK_CarClient_Client]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Category
	(
	CategoryId bigint NOT NULL IDENTITY(1,1),
	Category nvarchar(50) NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Category ADD CONSTRAINT
	PK_Category PRIMARY KEY CLUSTERED 
	(
	CategoryId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Category SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Address SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Vendor
	(
	VendorId bigint NOT NULL IDENTITY(1,1),
	Name nvarchar(50) NULL,
	AddressId bigint NULL,
	Phone nchar(20) NULL,
	Email nvarchar(50) NULL,
	MainContact nvarchar(50) NULL,
	Type nvarchar(50) NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.Vendor ADD CONSTRAINT
	PK_Vendor PRIMARY KEY CLUSTERED 
	(
	VendorId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Vendor ADD CONSTRAINT
	FK_Vendor_Address FOREIGN KEY
	(
	AddressId
	) REFERENCES dbo.Address
	(
	AddressId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.Vendor SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.PurchaseOrder
	(
	PurchaseOrderId bigint NOT NULL IDENTITY(1,1),
	VendorId bigint NOT NULL,
	Amount money NOT NULL,
	PODate date NOT NULL,
	CancelledDate date NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PurchaseOrder ADD CONSTRAINT
	PK_PurchaseOrder PRIMARY KEY CLUSTERED 
	(
	PurchaseOrderId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.PurchaseOrder ADD CONSTRAINT
	FK_PurchaseOrder_Vendor FOREIGN KEY
	(
	VendorId
	) REFERENCES dbo.Vendor
	(
	VendorId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.PurchaseOrder SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Part
	(
	PartId bigint NOT NULL IDENTITY(1,1),
	Name varchar(50) NULL,
	Description varchar(MAX) NULL,
	VendorId bigint NULL,
	CostPrice money NULL,
	ReorderQty int NOT NULL,
	EconomicalOrderQty int NOT NULL,
	QtyOnHand int NULL,
	QtyOnOrder int NULL,
	CategoryId bigint NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Part ADD CONSTRAINT
	PK_Part PRIMARY KEY CLUSTERED 
	(
	PartId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.Part ADD CONSTRAINT
	FK_Part_Vendor FOREIGN KEY
	(
	VendorId
	) REFERENCES dbo.Vendor
	(
	VendorId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.Part ADD CONSTRAINT
	FK_Part_Category FOREIGN KEY
	(
	CategoryId
	) REFERENCES dbo.Category
	(
	CategoryId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.Part SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.PurchaseOrderLineItem
	(
	PurchaseOrderLineItemId bigint NOT NULL IDENTITY(1,1),
	PurchaseOrderId bigint NOT NULL,
	PartId bigint NOT NULL,
	Qty int NOT NULL,
	Price money NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.PurchaseOrderLineItem ADD CONSTRAINT
	PK_PurchaseOrderLineItem PRIMARY KEY CLUSTERED 
	(
	PurchaseOrderLineItemId
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE dbo.PurchaseOrderLineItem ADD CONSTRAINT
	FK_PurchaseOrderLineItem_PurchaseOrder FOREIGN KEY
	(
	PurchaseOrderId
	) REFERENCES dbo.PurchaseOrder
	(
	PurchaseOrderId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.PurchaseOrderLineItem ADD CONSTRAINT
	FK_PurchaseOrderLineItem_Part FOREIGN KEY
	(
	PurchaseOrderId
	) REFERENCES dbo.Part
	(
	PartId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.PurchaseOrderLineItem SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Employee(
    EmployeeId bigint NOT NULL IDENTITY(1,1),
	FirstName varchar(50) NOT NULL,
	LastName varchar(50) NOT NULL,
    AddressId bigint NOT NULL,
	EmployeeSIN char(9) NOT NULL,
	Manager bigint NULL,
	HireDate date NOT NULL,
	TerminationDate date NULL,
	TerminationReason nchar(10) NULL,
    HashPass varchar(max) NOT NULL,
    HashSalt varchar(max) NOT NULL,
	Email nvarchar(50) NOT NULL,
	PhoneNum nvarchar(20) NOT NULL,
CONSTRAINT PK_Employee PRIMARY KEY CLUSTERED(
	EmployeeId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee] WITH CHECK ADD CONSTRAINT [FK_Employee_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Address]
GO
-- ALTER TABLE [dbo].[Employee] WITH CHECK ADD CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([Manager])
-- REFERENCES [dbo].[Employee] ([EmployeeId])
-- GO
-- ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Department(
	DepartmentId bigint NOT NULL IDENTITY(1,1),
	DepartmentName varchar(50) NOT NULL,
	EmployeeId bigint NOT NULL,
CONSTRAINT PK_Department PRIMARY KEY CLUSTERED(
	DepartmentId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Department] WITH CHECK ADD CONSTRAINT [FK_Department_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Employee]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.LoanerCar(
	LoanerCarId bigint NOT NULL IDENTITY(1,1),
	VIN varchar(17) NOT NULL,
	Make varchar(50) NOT NULL,
	Model varchar(50) NOT NULL,
	Colour varchar(50) NOT NULL,
	Odometer bigint NOT NULL,
CONSTRAINT PK_LoanerCar PRIMARY KEY CLUSTERED(
	LoanerCarId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
GO
CREATE TABLE dbo.Invoice(
	InvoiceId bigint NOT NULL IDENTITY(1,1),
	EmployeeId bigint NOT NULL,
	LoanerCarId bigint NULL,
	PartId bigint NULL,
	Cost money NOT NULL,
	HoursWorked float NULL,
	CreatedDate date NOT NULL,
	CancelledDate date NULL,
	CompletedDate date NULL,
	InvoiceDate date NULL,
	PaidDate date NULL,
CONSTRAINT PK_Invoice PRIMARY KEY CLUSTERED(
	InvoiceId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId]) ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Employee]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_LoanerCar] FOREIGN KEY([LoanerCarId])
REFERENCES [dbo].[LoanerCar] ([LoanerCarId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_LoanerCar]
GO
ALTER TABLE dbo.Invoice ADD CONSTRAINT
	FK_Invoice_Part FOREIGN KEY
	(
	PartId
	) REFERENCES dbo.Part
	(
	PartId
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
GO
ALTER TABLE dbo.Invoice SET (LOCK_ESCALATION = TABLE)
GO
COMMIT

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.AppointmentInvoice(
	AppointmentInvoiceId bigint NOT NULL IDENTITY(1,1),
	AppointmentId bigint NOT NULL,
	InvoiceId bigint NOT NULL,
CONSTRAINT PK_AppointmentInvoice PRIMARY KEY CLUSTERED(
	AppointmentInvoiceId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppointmentInvoice]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentInvoice_Appointment] FOREIGN KEY([AppointmentId])
REFERENCES [dbo].[Appointment] ([AppointmentId])  ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppointmentInvoice] CHECK CONSTRAINT [FK_AppointmentInvoice_Appointment]
GO
ALTER TABLE [dbo].[AppointmentInvoice]  WITH CHECK ADD  CONSTRAINT [FK_AppointmentInvoice_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AppointmentInvoice] CHECK CONSTRAINT [FK_AppointmentInvoice_Invoice]
GO

-- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- -- --
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE dbo.AccessLevel(
    AccessLevelId bigint NOT NULL IDENTITY(1,1),
    ClientId bigint,
    EmployeeId bigint,
    AccessLevel VARCHAR(20) NOT NULL,
CONSTRAINT PK_AccessLevel PRIMARY KEY CLUSTERED(
	AccessLevelId ASC
) WITH (
		PAD_INDEX = OFF,
		STATISTICS_NORECOMPUTE = OFF,
		IGNORE_DUP_KEY = OFF,
		ALLOW_ROW_LOCKS = ON,
		ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccessLevel] WITH CHECK ADD CONSTRAINT [FK_AccessLevel_Client] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AccessLevel] CHECK CONSTRAINT [FK_AccessLevel_Client]
GO
ALTER TABLE [dbo].[AccessLevel] WITH CHECK ADD CONSTRAINT [FK_AccessLevel_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[AccessLevel] CHECK CONSTRAINT [FK_AccessLevel_Employee]
GO