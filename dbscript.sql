USE [ProductTrackAppDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12.10.2023 17:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12.10.2023 17:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[UserId] [int] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12.10.2023 17:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Brand] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[ProductCode] [nvarchar](50) NOT NULL,
	[EmployeeId] [int] NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12.10.2023 17:03:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](25) NOT NULL,
	[ManagerId] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231007125021_init', N'7.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231008135325_removedUnnecessaryProperties', N'7.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231008150918_addedStatusPropertyForProduct', N'7.0.11')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231008163203_addedDefaultStatusValueForProduct', N'7.0.11')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (1, N'Lenovo', N'Ideapad 3', N'Laptop', N'LIP3', 3, 0)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (4, N'Asus', N'Rog Strix', N'Laptop', N'ARSP', 3, 0)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (6, N'Monster', N'Abra A5', N'Laptop', N'MAP5', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (7, N'Apple', N'Iphone 11', N'Phone', N'AIP11', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (8, N'Macbook', N'Air', N'Laptop', N'MAP', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (9, N'Samsung', N'S21', N'Phone', N'SSP21', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (10, N'Asus', N'Tuf Gaming', N'Monitor', N'ATTx8', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (30, N'Monster', N'Tulpar', N'Laptop', N'MTLP', 3, 1)
INSERT [dbo].[Products] ([Id], [Brand], [Model], [Category], [ProductCode], [EmployeeId], [Status]) VALUES (31, N'Modifiyeli Yamaha', N'R1', N'Motorcycle', N'YR1P', 3, 0)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [Name], [LastName], [Username], [Password], [Email], [Role], [ManagerId]) VALUES (1, N'Ahmet', N'Demir', N'user', N'123', N'alperen01343@gmail.com', N'User', 2)
INSERT [dbo].[Users] ([Id], [Name], [LastName], [Username], [Password], [Email], [Role], [ManagerId]) VALUES (2, N'Mehmet', N'Özçelik', N'manager', N'123', N'alperen01343@gmail.com', N'Manager', NULL)
INSERT [dbo].[Users] ([Id], [Name], [LastName], [Username], [Password], [Email], [Role], [ManagerId]) VALUES (3, N'Alperen', N'Alp', N'employee', N'123', N'alperen01343@gmail.com', N'Employee', NULL)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT (CONVERT([bit],(1))) FOR [Status]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__Role__38996AB5]  DEFAULT (N'User') FOR [Role]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Users] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Users] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Users]
GO
