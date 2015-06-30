USE ContainerStar;

CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[InvoiceNumber] [nvarchar](50) NOT NULL,
	[PayDate] [datetime2](2) NULL,
	[WithTaxes] [bit] NOT NULL,
	[ManualPrice] [float] NULL,
	[TaxValue] [float] NOT NULL,
	[Discount] [float] NOT NULL,
	[BillTillDate] [datetime2](2) NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_INVOICE_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 


ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_INVOICE_ORDER_ID] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([ID])


CREATE TABLE [dbo].[InvoicePositions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[Price] [float] NOT NULL,	
	[FromDate] [datetime2](2) NOT NULL,
	[ToDate] [datetime2](2) NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [PK_INVOICE_POSITION_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]



ALTER TABLE [dbo].[InvoicePositions]  WITH CHECK ADD  CONSTRAINT [FK_INVOICE_POSITION_INVOICE_ID] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoices] ([ID])


ALTER TABLE [dbo].[InvoicePositions]  WITH CHECK ADD  CONSTRAINT [FK_INVOICE_POSITION_POSITION_ID] FOREIGN KEY([PositionId])
REFERENCES [dbo].[Positions] ([ID])
GO