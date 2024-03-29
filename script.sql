USE [database]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [uniqueidentifier] NOT NULL,
	[ReferenceId] [uniqueidentifier] NOT NULL,
	[Current] [bit] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[AddressLine1] [nvarchar](250) NOT NULL,
	[AddressLine2] [nvarchar](250) NULL,
	[City] [nvarchar](50) NOT NULL,
	[State] [nvarchar](50) NOT NULL,
	[Zip] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY NONCLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Agency]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Agency](
	[AgencyId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LegalName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Agency] PRIMARY KEY NONCLUSTERED 
(
	[AgencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[CarrierId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Carrier] PRIMARY KEY NONCLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientId] [uniqueidentifier] NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[AddressId] [uniqueidentifier] NULL,
	[PhoneId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY NONCLUSTERED 
(
	[ClientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
	[PhoneId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY NONCLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Office]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Office](
	[OfficeId] [uniqueidentifier] NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Office] PRIMARY KEY NONCLUSTERED 
(
	[OfficeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Order]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [uniqueidentifier] NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[OfficeId] [uniqueidentifier] NOT NULL,
	[ClientId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Number] [nvarchar](10) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Amount] [decimal](10, 2) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[PriceLb] [decimal](10, 2) NOT NULL,
	[CantLb] [decimal](10, 2) NOT NULL,
	[ValorAduanalId] [uniqueidentifier] NULL,
	[TipoPagoId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY NONCLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Package]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Package](
	[PackageId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Package] PRIMARY KEY NONCLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PackageItem]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackageItem](
	[PackageItemId] [uniqueidentifier] NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Qty] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_PackageItem] PRIMARY KEY NONCLUSTERED 
(
	[PackageItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Phone]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phone](
	[PhoneId] [uniqueidentifier] NOT NULL,
	[ReferenceId] [uniqueidentifier] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Current] [bit] NOT NULL,
	[Number] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Product]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [uniqueidentifier] NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY NONCLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shipping]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipping](
	[PackingId] [uniqueidentifier] NOT NULL,
	[AgencyId] [uniqueidentifier] NOT NULL,
	[OfficeId] [uniqueidentifier] NOT NULL,
	[CarrierId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Shipping] PRIMARY KEY NONCLUSTERED 
(
	[PackingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingItem]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingItem](
	[ShippingItemId] [uniqueidentifier] NOT NULL,
	[PackingId] [uniqueidentifier] NOT NULL,
	[PackageId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Qty] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_ShippingItem] PRIMARY KEY NONCLUSTERED 
(
	[ShippingItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TipoPago]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPago](
	[TipoPagoId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[TipoPagoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](250) NOT NULL,
	[PasswordSalt] [nvarchar](250) NOT NULL,
	[SecureCode] [nvarchar](250) NULL,
	[ExpiresSecureCode] [datetime] NULL,
	[Status] [nvarchar](20) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[Timestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ValorAduanal]    Script Date: 19/8/2019 16:48:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ValorAduanal](
	[ValorAduanalId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](250) NULL,
	[UM] [nvarchar](50) NOT NULL,
	[Value] [decimal](10, 2) NOT NULL,
	[Article] [nvarchar](100) NOT NULL,
	[Observaciones] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[ValorAduanalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_AddressId]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Phone] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phone] ([PhoneId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Phone]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [RefAgency8] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([AgencyId])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [RefAgency8]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_AddressId] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([AddressId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_AddressId]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Phone] FOREIGN KEY([PhoneId])
REFERENCES [dbo].[Phone] ([PhoneId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Phone]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [RefClient10] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [RefClient10]
GO
ALTER TABLE [dbo].[Office]  WITH CHECK ADD  CONSTRAINT [RefAgency9] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([AgencyId])
GO
ALTER TABLE [dbo].[Office] CHECK CONSTRAINT [RefAgency9]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_TipoPago] FOREIGN KEY([TipoPagoId])
REFERENCES [dbo].[TipoPago] ([TipoPagoId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_TipoPago]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [RefAgency17] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([AgencyId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [RefAgency17]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [RefClient18] FOREIGN KEY([ClientId])
REFERENCES [dbo].[Client] ([ClientId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [RefClient18]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [RefOffice19] FOREIGN KEY([OfficeId])
REFERENCES [dbo].[Office] ([OfficeId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [RefOffice19]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [RefValorAduanal1] FOREIGN KEY([ValorAduanalId])
REFERENCES [dbo].[ValorAduanal] ([ValorAduanalId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [RefValorAduanal1]
GO
ALTER TABLE [dbo].[Package]  WITH CHECK ADD  CONSTRAINT [RefOrder20] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[Package] CHECK CONSTRAINT [RefOrder20]
GO
ALTER TABLE [dbo].[PackageItem]  WITH CHECK ADD  CONSTRAINT [RefPackage1] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([PackageId])
GO
ALTER TABLE [dbo].[PackageItem] CHECK CONSTRAINT [RefPackage1]
GO
ALTER TABLE [dbo].[PackageItem]  WITH CHECK ADD  CONSTRAINT [RefProduct2] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[PackageItem] CHECK CONSTRAINT [RefProduct2]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [RefAgency12] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([AgencyId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [RefAgency12]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [RefAgency4] FOREIGN KEY([AgencyId])
REFERENCES [dbo].[Agency] ([AgencyId])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [RefAgency4]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [RefCarrier5] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carrier] ([CarrierId])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [RefCarrier5]
GO
ALTER TABLE [dbo].[Shipping]  WITH CHECK ADD  CONSTRAINT [RefOffice16] FOREIGN KEY([OfficeId])
REFERENCES [dbo].[Office] ([OfficeId])
GO
ALTER TABLE [dbo].[Shipping] CHECK CONSTRAINT [RefOffice16]
GO
ALTER TABLE [dbo].[ShippingItem]  WITH CHECK ADD  CONSTRAINT [RefPackage14] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Package] ([PackageId])
GO
ALTER TABLE [dbo].[ShippingItem] CHECK CONSTRAINT [RefPackage14]
GO
ALTER TABLE [dbo].[ShippingItem]  WITH CHECK ADD  CONSTRAINT [RefProduct15] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[ShippingItem] CHECK CONSTRAINT [RefProduct15]
GO
ALTER TABLE [dbo].[ShippingItem]  WITH CHECK ADD  CONSTRAINT [RefShipping13] FOREIGN KEY([PackingId])
REFERENCES [dbo].[Shipping] ([PackingId])
GO
ALTER TABLE [dbo].[ShippingItem] CHECK CONSTRAINT [RefShipping13]
GO
