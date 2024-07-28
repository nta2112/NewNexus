USE [master]
GO
/****** Object:  Database [Nexus]    Script Date: 7/28/2024 7:27:56 PM ******/
CREATE DATABASE [Nexus]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Nexus', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Nexus.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Nexus_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Nexus_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Nexus] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Nexus].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Nexus] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Nexus] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Nexus] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Nexus] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Nexus] SET ARITHABORT OFF 
GO
ALTER DATABASE [Nexus] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Nexus] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Nexus] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Nexus] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Nexus] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Nexus] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Nexus] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Nexus] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Nexus] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Nexus] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Nexus] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Nexus] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Nexus] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Nexus] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Nexus] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Nexus] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Nexus] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Nexus] SET RECOVERY FULL 
GO
ALTER DATABASE [Nexus] SET  MULTI_USER 
GO
ALTER DATABASE [Nexus] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Nexus] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Nexus] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Nexus] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Nexus] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Nexus] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Nexus', N'ON'
GO
ALTER DATABASE [Nexus] SET QUERY_STORE = OFF
GO
USE [Nexus]
GO
/****** Object:  Table [dbo].[Bill]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[bill_id] [int] IDENTITY(1,1) NOT NULL,
	[order_id] [nvarchar](11) NOT NULL,
	[amount] [decimal](10, 2) NOT NULL,
	[status] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[bill_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Connection]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Connection](
	[connection_id] [nvarchar](16) NOT NULL,
	[account_id] [nvarchar](16) NULL,
	[package_id] [int] NULL,
	[status] [nvarchar](50) NULL,
	[installation_date] [date] NULL,
	[termination_date] [date] NULL,
	[order_id] [nvarchar](11) NULL,
PRIMARY KEY CLUSTERED 
(
	[connection_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConnectionType]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConnectionType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[account_id] [nvarchar](16) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[phone] [nvarchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[employee_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
	[role_id] [int] NOT NULL,
	[shop_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[employee_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[order_id] [nvarchar](11) NOT NULL,
	[product_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC,
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[order_id] [nvarchar](11) NOT NULL,
	[account_id] [nvarchar](16) NOT NULL,
	[status] [nvarchar](50) NOT NULL,
	[package_id] [int] NOT NULL,
	[shop_id] [int] NOT NULL,
	[order_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[payment_id] [int] IDENTITY(1,1) NOT NULL,
	[bill_id] [int] NULL,
	[amount] [decimal](10, 2) NOT NULL,
	[payment_date] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[payment_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[vendor_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[img] [nvarchar](max) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RetailShop]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RetailShop](
	[shop_id] [int] IDENTITY(1,1) NOT NULL,
	[address] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[shop_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicePackage]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePackage](
	[package_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](100) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[connection_type_id] [int] NOT NULL,
	[status] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[package_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 7/28/2024 7:27:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[vendor_id] [int] IDENTITY(1,1) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[vendor_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ConnectionType] ON 

INSERT [dbo].[ConnectionType] ([id], [name]) VALUES (2, N'B')
INSERT [dbo].[ConnectionType] ([id], [name]) VALUES (3, N'D')
INSERT [dbo].[ConnectionType] ([id], [name]) VALUES (1, N'T')
SET IDENTITY_INSERT [dbo].[ConnectionType] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([employee_id], [name], [email], [password], [role_id], [shop_id]) VALUES (1, N'An', N'ann@gmail.com', N'1', 1, 3)
INSERT [dbo].[Employee] ([employee_id], [name], [email], [password], [role_id], [shop_id]) VALUES (5, N'new', N'new@gmail.com', N'1', 3, 2)
INSERT [dbo].[Employee] ([employee_id], [name], [email], [password], [role_id], [shop_id]) VALUES (15, N'2', N'2@gmail.com', N'2', 2, 4)
INSERT [dbo].[Employee] ([employee_id], [name], [email], [password], [role_id], [shop_id]) VALUES (16, N'3', N'3@gmail.com', N'3', 3, 3)
INSERT [dbo].[Employee] ([employee_id], [name], [email], [password], [role_id], [shop_id]) VALUES (17, N'4', N'4@gmail.com', N'4', 4, 5)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([product_id], [name], [description], [price], [vendor_id], [quantity], [img]) VALUES (1, N'1', N'1', CAST(1.00 AS Decimal(10, 2)), 1, 1, N'/images/remix-rumble-1080x1080.jpg')
INSERT [dbo].[Product] ([product_id], [name], [description], [price], [vendor_id], [quantity], [img]) VALUES (3, N'teffffffff', N'te', CAST(10.00 AS Decimal(10, 2)), 6, 13, N'/images/640px-Morgan_Freeman_Deauville_2018.jpg')
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[RetailShop] ON 

INSERT [dbo].[RetailShop] ([shop_id], [address]) VALUES (1, N'175 TySon Hoan Kiem Ha Noi')
INSERT [dbo].[RetailShop] ([shop_id], [address]) VALUES (2, N'175 Tay Son Hoan Kiem Ha Noi')
INSERT [dbo].[RetailShop] ([shop_id], [address]) VALUES (3, N'1799999 Tay Son')
INSERT [dbo].[RetailShop] ([shop_id], [address]) VALUES (4, N'1A Tong Dan Hoan Kiem Ha Noi')
INSERT [dbo].[RetailShop] ([shop_id], [address]) VALUES (5, N'177 Tay Son Hoan Kiem Ha Noi')
SET IDENTITY_INSERT [dbo].[RetailShop] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (2, N'Account 
department ')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (3, N'Employees
 retail ')
INSERT [dbo].[Role] ([role_id], [role_name]) VALUES (4, N'Technical
people')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[ServicePackage] ON 

INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (2, N'Dial – Up Connection Hourly Basis 10 Hrs ', N'10 Hrs. – 50$ (validity is for one Month)', CAST(50.00 AS Decimal(10, 2)), 3, 0)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (6, N'Dial – Up Connection Hourly Basis 30 Hrs', N'30 Hrs. – 130$ (validity is for 3 Months)', CAST(130.00 AS Decimal(10, 2)), 3, 0)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (8, N'Dial – Up Connection Hourly Basis 60 Hrs', N'60 Hrs. – 260$ (validity is for 6 Months)', CAST(260.00 AS Decimal(10, 2)), 3, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (10, N'Dial – Up Connection Unlimited 28Kbps', N'Monthly – 75$', CAST(75.00 AS Decimal(10, 2)), 3, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (11, N'Dial – Up Connection Unlimited 28Kbps', N'Dial – Up Connection Unlimited 28Kbps Quarterly – 150$', CAST(150.00 AS Decimal(10, 2)), 3, 0)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (12, N'Dial – Up Connection :Unlimited 56 Kbps Monthly', N'Dial – Up Connection :Unlimited 56 Kbps Monthly– 100$', CAST(100.00 AS Decimal(10, 2)), 3, 0)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (14, N'Dial – Up Connection :Unlimited 56 Kbps Quarterly ', N'Dial – Up Connection :Unlimited 56 Kbps Quarterly 180$', CAST(180.00 AS Decimal(10, 2)), 3, 0)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (15, N'Broad Band Connection Hourly Basis 30 Hrs.', N'Broad Band Connection Hourly Basis 30 Hrs. – 175$ (validity is for 1 Month)', CAST(175.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (16, N'Broad Band Connection Hourly Basis 60 Hrs.', N'Broad Band Connection Hourly Basis 60 Hrs. – 315$ (validity is for 6 Months)', CAST(315.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (17, N'Broad Band Connection Unlimited 64Kbps. Monthly', N'Broad Band Connection Unlimited 64Kbps. Monthly – 225$', CAST(225.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (18, N'Broad Band Connection Unlimited 64Kbps. Quarterly', N'Broad Band Connection Unlimited 64Kbps. Quarterly– 400$', CAST(400.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (19, N'Broad Band Connection Unlimited 128 Kbps.Monthly ', N'Broad Band Connection Unlimited 128 Kbps.Monthly – 350$', CAST(350.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (21, N'Broad Band Connection Unlimited 128 Kbps.Quarterly', N'Broad Band Connection Unlimited 128 Kbps.Quarterly – 445$', CAST(445.00 AS Decimal(10, 2)), 2, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (27, N'Land Line Connection Local Plan Unlimited', N'75$ (Valid for an year and this is the rental)
The call charges are : 55cents / minute', CAST(75.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (28, N'Land Line Connection Local Plan Monthly Plan', N'35$(Valid for a month and this is the rental)
The call charges are: 75cents / minute', CAST(35.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (29, N'Land Line Connection STD Plan Monthly', N'125$ 
Local : 70cents/minute
STD : 2.25$ / minute
Messaging For Mobiles : 1.00$ / Minute', CAST(125.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (30, N'Land Line Connection STD Plan Half - Yearly', N'420$ Local : 60cents / minute
STD : 2.00$ / minute
Messaging For Mobiles : 1.15$ / Minute', CAST(420.00 AS Decimal(10, 2)), 1, 1)
INSERT [dbo].[ServicePackage] ([package_id], [name], [description], [price], [connection_type_id], [status]) VALUES (31, N'Land Line Connection STD Plan Yearly – $', N'Local : 60cents / minute
STD : 1.75$ / minute
Messaging For Mobiles : 1.25$ / Minute', CAST(1.75 AS Decimal(10, 2)), 1, 1)
SET IDENTITY_INSERT [dbo].[ServicePackage] OFF
GO
SET IDENTITY_INSERT [dbo].[Vendor] ON 

INSERT [dbo].[Vendor] ([vendor_id], [email], [Name]) VALUES (1, N'Chanel@gmail.com', N'Chanel')
INSERT [dbo].[Vendor] ([vendor_id], [email], [Name]) VALUES (2, N'vendor@gmail.com', N'Vendor')
INSERT [dbo].[Vendor] ([vendor_id], [email], [Name]) VALUES (3, N'NotVendor@gmail.com', N'NotVendor')
INSERT [dbo].[Vendor] ([vendor_id], [email], [Name]) VALUES (6, N'annnn@gmail.com', N'annn the vendor')
SET IDENTITY_INSERT [dbo].[Vendor] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Connecti__72E12F1B03978E64]    Script Date: 7/28/2024 7:27:57 PM ******/
ALTER TABLE [dbo].[ConnectionType] ADD UNIQUE NONCLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServicePackage] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ('Unknown') FOR [Name]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Connection]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[Customer] ([account_id])
GO
ALTER TABLE [dbo].[Connection]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[Connection]  WITH CHECK ADD FOREIGN KEY([package_id])
REFERENCES [dbo].[ServicePackage] ([package_id])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([shop_id])
REFERENCES [dbo].[RetailShop] ([shop_id])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Product] ([product_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([account_id])
REFERENCES [dbo].[Customer] ([account_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([package_id])
REFERENCES [dbo].[ServicePackage] ([package_id])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([shop_id])
REFERENCES [dbo].[RetailShop] ([shop_id])
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([bill_id])
REFERENCES [dbo].[Bill] ([bill_id])
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([vendor_id])
REFERENCES [dbo].[Vendor] ([vendor_id])
GO
ALTER TABLE [dbo].[ServicePackage]  WITH CHECK ADD FOREIGN KEY([connection_type_id])
REFERENCES [dbo].[ConnectionType] ([id])
GO
USE [master]
GO
ALTER DATABASE [Nexus] SET  READ_WRITE 
GO
