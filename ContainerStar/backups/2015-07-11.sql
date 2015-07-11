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

CREATE TABLE [dbo].[InvoiceStornos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[ProceedsAccount] [int] NOT NULL,
	[Price] [float] NOT NULL,	
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [PK_INVOICE_STORNO_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvoiceStornos]  WITH CHECK ADD  CONSTRAINT [FK_INVOICE_STORNO_INVOICE_ID] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([ID])
GO

SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] ON;

IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 13 )
BEGIN
	INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
	VALUES(13, 'InvoiceStornos', 'Storno Rechnung' ,GETDATE() ,GETDATE() ,NULL);
	INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
	VALUES (1 ,13 ,getdate() ,getdate());
END
ELSE
BEGIN
	UPDATE [ContainerStar].[dbo].[PERMISSION]
	SET [DESCRIPTION] = 'Storno Rechnung'
	WHERE ID = 13
END

SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] OFF;