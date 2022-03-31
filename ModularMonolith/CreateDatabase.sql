USE [master]
GO

/****** Object:  Database [ModularMonolith]    Script Date: 3/31/2022 5:50:32 PM ******/
CREATE DATABASE [ModularMonolith2]
GO

USE [ModularMonolith2]
GO
/****** Object:  Schema [Communication]    Script Date: 3/31/2022 4:57:05 PM ******/
CREATE SCHEMA [Communication]
GO
/****** Object:  Schema [Order]    Script Date: 3/31/2022 4:57:05 PM ******/
CREATE SCHEMA [Order]
GO
/****** Object:  Table [Communication].[Emails]    Script Date: 3/31/2022 4:57:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Communication].[Emails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Address] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[IsSended] [bit] NOT NULL,
	[Attempts] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Order].[OrderItems]    Script Date: 3/31/2022 4:57:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Order].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Order].[Orders]    Script Date: 3/31/2022 4:57:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Order].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Order].[Products]    Script Date: 3/31/2022 4:57:05 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Order].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [Communication].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [Order].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Communication].[Emails] CHECK CONSTRAINT [FK_Emails_Orders_OrderId]
GO
ALTER TABLE [Order].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [Order].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Order].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [Order].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [Order].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [Order].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Products_ProductId]
GO

INSERT INTO [Order].[Products] ([Name],[Price]) VALUES('Product 1', 1)
INSERT INTO [Order].[Products] ([Name],[Price]) VALUES('Product 2', 10)
GO