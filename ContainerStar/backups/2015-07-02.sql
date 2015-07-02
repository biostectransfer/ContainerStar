USE ContainerStar;

ALTER TABLE [dbo].[InvoicePositions]  
 ADD [Amount] int NOT NULL default(1)
GO