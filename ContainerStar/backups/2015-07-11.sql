USE ContainerStar;

ALTER TABLE [dbo].[Containers]  
 ADD [IsSold] bit NOT NULL default(0)
GO

UPDATE [dbo].[Orders] 
 SET [Status] = 1
 GO

ALTER TABLE [dbo].[Orders]  
 ALTER COLUMN [Status] int NOT NULL
GO

ALTER TABLE dbo.Orders ADD CONSTRAINT
	DF_Orders_Status DEFAULT 1 FOR Status
GO