USE ContainerStar;

ALTER TABLE [dbo].[Invoices]  
 ADD [IsSellInvoice] bit NOT NULL default(0)
GO