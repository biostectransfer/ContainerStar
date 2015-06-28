USE ContainerStar;

ALTER TABLE [dbo].[Positions]
 ADD [Amount] int NOT NULL default (1)
GO

ALTER TABLE [dbo].[Orders]
 ALTER COLUMN [CommunicationPartnerId] int NULL
GO