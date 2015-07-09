USE ContainerStar;

ALTER TABLE [dbo].[Invoices]  
 ADD [ReminderCount] int NOT NULL default(0),
 [DateVExportDate] datetime2(2) NULL,
 [DateVExportFile] nvarchar(128) NULL
GO

ALTER TABLE [dbo].[Orders]  
 ADD [Status] int NOT NULL default(0)
GO