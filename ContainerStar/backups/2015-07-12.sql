USE ContainerStar;

ALTER TABLE [dbo].[InvoiceStornos]  
 ADD [DateVExportDate] datetime2(2) NULL,
 [DateVExportFile] nvarchar(128) NULL
GO

CREATE TABLE [dbo].[TransportOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[CommunicationPartnerId] [int] NULL,
	[IsOffer] [bit] NOT NULL default(0),
	[DeliveryPlace] [nvarchar](128) NOT NULL,
	[Street] [nvarchar](128) NOT NULL,
	[ZIP] [int] NOT NULL,
	[City] [nvarchar](128) NOT NULL,
	[Comment] [nvarchar](128) NULL,
	[OrderDate] [datetime2](2) NULL,
	[OrderedFrom] [nvarchar](128) NULL,
	[OrderNumber] [nvarchar](50) NULL,
	[CustomerOrderNumber] [nvarchar](50) NULL,
	[Discount] [float] NULL,
	[BillTillDate] [datetime2](2) NULL,
	[Status] int NOT NULL default(1),
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_TRANSPORT_ORDER_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[TransportOrders]  WITH CHECK ADD  CONSTRAINT [FK_TRANSPORT_ORDER_CUSTOMER_ID] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([ID])
GO

ALTER TABLE [dbo].[TransportOrders]  WITH CHECK ADD  CONSTRAINT [FK_TRANSPORT_ORDER_COMMUNICATION_PARTNER_ID] FOREIGN KEY([CommunicationPartnerId])
REFERENCES [dbo].[CommunicationPartners] ([ID])
GO

CREATE TABLE [dbo].[TransportPositions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransportOrderId] [int] NOT NULL,
	[TransportProductId] [int] NOT NULL,
	[Price] [float] NOT NULL,	
	[Amount] [int] NOT NULL default(1),
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [PK_TRANSPORT_POSITION_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[TransportPositions]  WITH CHECK ADD  CONSTRAINT [FK_TRANSPORT_POSITION_TRANSPORT_ORDER_ID] FOREIGN KEY([TransportOrderId])
REFERENCES [dbo].[TransportOrders] ([ID])
GO

ALTER TABLE [dbo].[TransportPositions]  WITH CHECK ADD  CONSTRAINT [FK_TRANSPORT_POSITION_TRANSPORT_PRODUCT_ID] FOREIGN KEY([TransportProductId])
REFERENCES [dbo].[TransportProducts] ([ID])
GO

SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] ON;

IF NOT EXISTS (SELECT ID FROM [ContainerStar].[dbo].[PERMISSION] WHERE ID = 14 )
BEGIN
	INSERT INTO [ContainerStar].[dbo].[PERMISSION] ([Id], [Name], [Description], [CreateDate], [ChangeDate], [DeleteDate])
	VALUES(14, 'TransportOrders', 'Transport Auftrag' ,GETDATE() ,GETDATE() ,NULL);
	INSERT INTO [ContainerStar].dbo.ROLE_PERMISSION_RSP(RoleId ,PermissionId ,CreateDate ,ChangeDate) 
	VALUES (1 ,14 ,getdate() ,getdate());
END
ELSE
BEGIN
	UPDATE [ContainerStar].[dbo].[PERMISSION]
	SET [DESCRIPTION] = 'Transport Auftrag'
	WHERE ID = 14
END

SET IDENTITY_INSERT [ContainerStar].[dbo].[PERMISSION] OFF;

GO

INSERT INTO [dbo].[Numbers]
           ([NumberType]
           ,[CurrentNumber])
     VALUES
           (4
           ,1)
GO
