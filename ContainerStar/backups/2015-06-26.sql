USE ContainerStar;

ALTER TABLE [dbo].[Orders]
 ALTER COLUMN [RentOrderNumber] nvarchar(50) NULL
GO

ALTER TABLE [dbo].[Orders]
 ADD [CustomerOrderNumber] nvarchar(50) NULL,
 [AutoProlongation] [bit] NOT NULL default(1)
GO