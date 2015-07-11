USE ContainerStar;

ALTER TABLE [dbo].[Containers]  
 ADD [IsSold] bit NOT NULL default(0)
GO