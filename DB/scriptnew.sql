
/****** Object:  Database [FoodPortal]    Script Date: 27-06-2023 10:22:02 ******/
CREATE DATABASE [FoodPortal]

USE [FoodPortal]
GO

/****** Object:  Table [dbo].[AdditionalCategoryMaster]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalCategoryMaster](
	[Id] [nvarchar](6) NOT NULL,
	[AdditionalCategory] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[AdditionalProducts]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalProducts](
	[Id] [nvarchar](6) NOT NULL,
	[AdditionalCategoryId] [nvarchar](6) NULL,
	[AdditionalProductsName] [nvarchar](30) NOT NULL,
	[IsVeg] [bit] NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[AdditionalProductsImages] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[AdditionalProductsDetails]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalProductsDetails](
	[Id] [bigint] IDENTITY(301,1) NOT NULL,
	[AdditionalProductsId] [nvarchar](6) NULL,
	[OrderId] [nvarchar](10) NULL,
	[Quantity] [int] NULL,
	[Cost] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[AddOnsDetails]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddOnsDetails](
	[Id] [bigint] IDENTITY(201,1) NOT NULL,
	[AddOnsId] [nvarchar](6) NULL,
	[OrderId] [nvarchar](10) NULL,
	[Quantity] [int] NULL,
	[Cost] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[AddOnsMaster]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddOnsMaster](
	[Id] [nvarchar](6) NOT NULL,
	[AddOnsName] [nvarchar](30) NOT NULL,
	[UnitPrice] [decimal](18, 0) NULL,
	[AddOnsImage] [nvarchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[AllergyDetails]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AllergyDetails](
	[Id] [int] IDENTITY(10,1) NOT NULL,
	[AddAllergy] [nvarchar](100) NULL,
	[OrderId] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[DeliveryOption]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOption](
	[Id] [nvarchar](6) NOT NULL,
	[DeliveryOption] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[FoodTypeCount]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FoodTypeCount](
	[Id] [nvarchar](6) NOT NULL,
	[OrderId] [nvarchar](10) NULL,
	[FoodTypeCount] [int] NULL,
	[IsVeg] [bit] NULL,
	[PlateSizeId] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[GroupSize]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupSize](
	[Id] [nvarchar](6) NOT NULL,
	[GroupType] [nvarchar](20) NOT NULL,
	[MinValue] [int] NOT NULL,
	[MaxValue] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Orders]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [nvarchar](10) NOT NULL,
	[DeliveryOptionId] [nvarchar](6) NULL,
	[PlateSizeId] [nvarchar](6) NULL,
	[AdditionalNote] [nvarchar](1000) NULL,
	[Address] [nvarchar](500) NULL,
	[Date] [date] NULL,
	[GroupSizeId] [nvarchar](6) NULL,
	[TimeSlotId] [int] NULL,
	[UserName] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[PlateSize]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlateSize](
	[Id] [nvarchar](6) NOT NULL,
	[PlateType] [nvarchar](20) NOT NULL,
	[VegPlateCost] [decimal](18, 0) NULL,
	[NonvegPlateCost] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[StdFoodCategoryMaster]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StdFoodCategoryMaster](
	[Id] [nvarchar](6) NOT NULL,
	[Category] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[StdFoodOrderDetails]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StdFoodOrderDetails](
	[Id] [bigint] IDENTITY(11,1) NOT NULL,
	[OrderId] [nvarchar](10) NULL,
	[ProductsId] [nvarchar](6) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[StdProducts]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StdProducts](
	[Id] [nvarchar](6) NOT NULL,
	[CategoriesId] [nvarchar](6) NULL,
	[ProductsName] [varchar](20) NULL,
	[IsVeg] [bit] NULL,
	[FoodProductImage] [nvarchar](1) NULL,
	[IsGluten] [bit] NULL,
	[IsSpicy] [bit] NULL,
	[IsVegan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[TimeSlots]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeSlots](
	[Id] [int] IDENTITY(20,1) NOT NULL,
	[AddTimeSlot] [nvarchar](25) NOT NULL,
	[ChosenSlot] [time](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[TrackStatus]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrackStatus](
	[Id] [nvarchar](6) NOT NULL,
	[OrderId] [nvarchar](10) NULL,
	[Status] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[Users]    Script Date: 27-06-2023 10:22:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Name] [nvarchar](20) NOT NULL,
	[EmailId] [nvarchar](32) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[PhoneNumber] [nvarchar](10) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](15) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/*********Foreign key*********/
ALTER TABLE [dbo].[AdditionalProducts]  WITH CHECK ADD FOREIGN KEY([AdditionalCategoryId])
REFERENCES [dbo].[AdditionalCategoryMaster] ([Id])
GO
ALTER TABLE [dbo].[AdditionalProductsDetails]  WITH CHECK ADD FOREIGN KEY([AdditionalProductsId])
REFERENCES [dbo].[AdditionalProducts] ([Id])
GO
ALTER TABLE [dbo].[AdditionalProductsDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[AddOnsDetails]  WITH CHECK ADD FOREIGN KEY([AddOnsId])
REFERENCES [dbo].[AddOnsMaster] ([Id])
GO
ALTER TABLE [dbo].[AddOnsDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[AllergyDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[AllergyDetails] CHECK CONSTRAINT [FK_OrderId]
GO
ALTER TABLE [dbo].[FoodTypeCount]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[FoodTypeCount]  WITH CHECK ADD FOREIGN KEY([PlateSizeId])
REFERENCES [dbo].[PlateSize] ([Id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([DeliveryOptionId])
REFERENCES [dbo].[DeliveryOption] ([Id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([PlateSizeId])
REFERENCES [dbo].[PlateSize] ([Id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_GroupSize] FOREIGN KEY([GroupSizeId])
REFERENCES [dbo].[GroupSize] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_GroupSize]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_TimeSlotId] FOREIGN KEY([TimeSlotId])
REFERENCES [dbo].[TimeSlots] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_TimeSlotId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_UserName] FOREIGN KEY([UserName])
REFERENCES [dbo].[Users] ([UserName])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_UserName]
GO
ALTER TABLE [dbo].[StdFoodOrderDetails]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO
ALTER TABLE [dbo].[StdFoodOrderDetails]  WITH CHECK ADD FOREIGN KEY([ProductsId])
REFERENCES [dbo].[StdProducts] ([Id])
GO
ALTER TABLE [dbo].[StdProducts]  WITH CHECK ADD FOREIGN KEY([CategoriesId])
REFERENCES [dbo].[StdFoodCategoryMaster] ([Id])
GO
ALTER TABLE [dbo].[TrackStatus]  WITH CHECK ADD FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
GO

