USE ContainerStar;

ALTER TABLE [dbo].[Customers]
 ADD [Bank] nvarchar(128) NULL,
 [AccountNumber] nvarchar(128) NULL,
 [BLZ] nvarchar(128) NULL
GO

ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [UstId] nvarchar(20) NULL
GO
ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [Country] nvarchar(50) NULL
GO
ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [BIC] nvarchar(20) NULL
GO
ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [IBAN] nvarchar(30) NULL
GO
ALTER TABLE [dbo].[Customers]
 ADD [IsProspectiveCustomer] bit NOT NULL default(0)
GO
ALTER TABLE [dbo].[Positions]
 ADD [IsMain] bit NOT NULL default(0)
GO
ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [ZIP] nvarchar(10) NOT NULL
GO
ALTER TABLE [dbo].[Orders]
 ALTER COLUMN [ZIP] nvarchar(10) NOT NULL
GO
ALTER TABLE [dbo].[CommunicationPartners]
 ALTER COLUMN [FirstName] nvarchar(128) NULL
GO
ALTER TABLE [dbo].[CommunicationPartners]
 ALTER COLUMN [Phone] nvarchar(20) NULL
GO
ALTER TABLE [dbo].[TransportOrders]
 ALTER COLUMN [ZIP] nvarchar(10) NOT NULL
GO
ALTER TABLE [dbo].[Invoices]
 ADD [PayInDays] int NOT NULL default (0),
 [PayPartOne] int NULL,
 [PayPartTwo] int NULL,
 [PayPartTree] int NULL
GO
ALTER TABLE [dbo].[ContainerTypes]
 ADD [DispositionRelevant] bit NOT NULL default (1)
GO
ALTER TABLE [dbo].[Positions]
 ADD [PaymentType] int NOT NULL default(0)
GO
ALTER TABLE [dbo].[Customers]
 ALTER COLUMN [Number] int NOT NULL
GO
ALTER TABLE [dbo].[Containers]
 ADD [MinPrice] float NOT NULL default(0)
GO
ALTER TABLE [dbo].[Invoices]
 ADD [LastReminderDate] datetime2(2) NULL
GO