

CREATE TABLE [dbo].[Permission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_PERMISSION_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Name  EN: Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PERMISSION', @level2type=N'COLUMN',@level2name=N'NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Berechtigung  EN: Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PERMISSION'
GO


CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Name  EN: Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ROLE', @level2type=N'COLUMN',@level2name=N'NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Rolle  EN: Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ROLE'
GO

CREATE TABLE [dbo].[Role_Permission_Rsp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [RK_ROLE_PERMISSION_RSP_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Role_Permission_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_PERMISSION_RSP_PERMISSION_ID] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[PERMISSION] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Role_Permission_Rsp] CHECK CONSTRAINT [FK_ROLE_PERMISSION_RSP_PERMISSION_ID]
GO

ALTER TABLE [dbo].[Role_Permission_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_PERMISSION_RSP_ROLE_ID] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ROLE] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Role_Permission_Rsp] CHECK CONSTRAINT [FK_ROLE_PERMISSION_RSP_ROLE_ID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Rolle  EN: Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ROLE_PERMISSION_RSP', @level2type=N'COLUMN',@level2name=N'RoleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Berechtigung  EN: Permission' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ROLE_PERMISSION_RSP', @level2type=N'COLUMN',@level2name=N'PermissionId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Berechtigung  EN: Permissiom' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ROLE_PERMISSION_RSP'
GO

CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [int] NULL,
	[Login] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [PK_USER_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleId])
REFERENCES [dbo].[ROLE] ([ID])
GO

ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_ROLE_ID]
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Rolle  EN: Role' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USER', @level2type=N'COLUMN',@level2name=N'RoleId'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Login  EN: Login' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USER', @level2type=N'COLUMN',@level2name=N'LOGIN'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Name  EN: Name' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USER', @level2type=N'COLUMN',@level2name=N'NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Passwort  EN: Password' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USER', @level2type=N'COLUMN',@level2name=N'PASSWORD'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'DE: Benutzer  EN: User' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'USER'
GO


CREATE TABLE [dbo].[Equipments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](128) NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_EQUIPMENT_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[AdditionalCosts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](128) NOT NULL,
	[Price] [float] NOT NULL,
	[Automatic] [bit] NOT NULL,
	[IncludeInFirstBill] [bit] NOT NULL,
	[ProceedsAccount] [int] NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_ADDITIONAL_COSTS_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[Taxes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Value] [float] NOT NULL,
	[FromDate] [datetime2](2) NOT NULL,
	[ToDate] [datetime2](2) NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_TAXES_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[TransportProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Description] [nvarchar](128) NOT NULL,
	[Price] [float] NOT NULL,
	[ProceedsAccount] [int] NOT NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_TRANSPORT_PRODUCTS_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[Customers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](20) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Street] [nvarchar](128) NOT NULL,
	[ZIP] [int] NOT NULL,
	[City] [nvarchar](128) NOT NULL,
	[Country] [nvarchar](2) NULL,
	[Phone] [nvarchar](20) NULL,
	[Mobile] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Comment] [nvarchar](128) NULL,
	[IBAN] [nvarchar](22) NULL,
	[BIC] [nvarchar](10) NULL,
	[WithTaxes] [bit] NOT NULL,
	[AutoDebitEntry] [bit] NOT NULL,
	[AutoBill] [bit] NOT NULL,
	[Discount] [float] NULL,
	[UstId] [nvarchar](10) NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_CUSTOMERS_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[CommunicationPartners](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](128) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Mobile] [nvarchar](20) NULL,
	[Fax] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,	
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_COMMUNICATION_PARTNERS_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[CommunicationPartners]  WITH CHECK ADD  CONSTRAINT [FK_CUSTOMER_ID] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO

CREATE TABLE [dbo].[ContainerTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Comment] [nvarchar](256) NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_CONTAINER_TYPES_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

CREATE TABLE [dbo].[ContainerType_Equipment_Rsp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContainerTypeId] [int] NOT NULL,
	[EquipmentId] [int] NOT NULL,
	[Amount] [int] NOT NULL default (1),
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [RK_CONTAINERTYPE_EQUIPMENT_RSP_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ContainerType_Equipment_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_CONTAINERTYPE_EQUIPMENT_RSP_CONTAINERTYPE_ID] FOREIGN KEY([ContainerTypeId])
REFERENCES [dbo].[ContainerTypes] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ContainerType_Equipment_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_CONTAINERTYPE_EQUIPMENT_RSP_EQUIPMENT_ID] FOREIGN KEY([EquipmentId])
REFERENCES [dbo].[Equipments] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

CREATE TABLE [dbo].[Containers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](20) NOT NULL,
	[ContainerTypeId] [int] NOT NULL,
	[Length] [int] NOT NULL,
	[Width] [int] NOT NULL,
	[Height] [int] NOT NULL,
	[Color] [nvarchar](50) NOT NULL,
	[Price] [float] NOT NULL,
	[ProceedsAccount] [int] NOT NULL,
	[IsVirtual] [bit] NOT NULL default (0),
	[ManufactureDate] [datetime2](2) NULL,
	[BoughtFrom] [nvarchar](128) NULL,
	[BoughtPrice] [float] NULL,	
	[Comment] [nvarchar](128) NULL,
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL
 CONSTRAINT [PK_CONTAINER_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

ALTER TABLE [dbo].[Containers]  WITH CHECK ADD  CONSTRAINT [FK_CONTAINER_CONTAINERTYPE_ID] FOREIGN KEY([ContainerTypeId])
REFERENCES [dbo].[ContainerTypes] ([ID])
GO

CREATE TABLE [dbo].[Container_Equipment_Rsp](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ContainerId] [int] NOT NULL,
	[EquipmentId] [int] NOT NULL,
	[Amount] [int] NOT NULL default (1),
	[CreateDate] [datetime2](2) NOT NULL,
	[ChangeDate] [datetime2](2) NOT NULL,
	[DeleteDate] [datetime2](2) NULL	
 CONSTRAINT [RK_CONTAINER_EQUIPMENT_RSP_ID] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Container_Equipment_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_CONTAINER_EQUIPMENT_RSP_CONTAINER_ID] FOREIGN KEY([ContainerId])
REFERENCES [dbo].[Containers] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Container_Equipment_Rsp]  WITH CHECK ADD  CONSTRAINT [FK_CONTAINER_EQUIPMENT_RSP_EQUIPMENT_ID] FOREIGN KEY([EquipmentId])
REFERENCES [dbo].[Equipments] ([ID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO