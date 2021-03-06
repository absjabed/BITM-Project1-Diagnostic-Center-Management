USE [master]
GO
/****** Object:  Database [DiagnosticCareAppDB]    Script Date: 2017-02-26 01:32:51 AM ******/
CREATE DATABASE [DiagnosticCareAppDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiagnosticCareAppDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DiagnosticCareAppDB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DiagnosticCareAppDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DiagnosticCareAppDB_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DiagnosticCareAppDB] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiagnosticCareAppDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET  MULTI_USER 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiagnosticCareAppDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DiagnosticCareAppDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DiagnosticCareAppDB', N'ON'
GO
USE [DiagnosticCareAppDB]
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Patient_name] [varchar](50) NULL,
	[Date_of_birth] [date] NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Bill_no] [varchar](30) NOT NULL,
	[Payment_status] [bit] NOT NULL,
	[Due_amount] [decimal](18, 2) NOT NULL,
	[Created_at] [date] NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PatientTest]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientTest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Patient_id] [int] NOT NULL,
	[Test_setup_id] [int] NOT NULL,
	[Created_at] [date] NOT NULL,
 CONSTRAINT [PK_PatientTest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestSetup]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Test_name] [varchar](70) NOT NULL,
	[Fee] [decimal](18, 2) NOT NULL,
	[Type_id] [int] NOT NULL,
	[Created_at] [date] NOT NULL,
 CONSTRAINT [PK_TestSetup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TestType]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Test_type_name] [varchar](100) NOT NULL,
	[Created_at] [date] NULL,
 CONSTRAINT [PK_TestType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[AllTestsInfo_view]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AllTestsInfo_view]
AS
SELECT     TestSetup.Test_name TestName, TestSetup.Fee TestFee, TestType.Test_type_name TestTypeName
FROM       TestSetup INNER JOIN TestType ON TestSetup.Type_id = TestType.Id

GO
/****** Object:  View [dbo].[unpaidBillView]    Script Date: 2017-02-26 01:32:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[unpaidBillView] AS
SELECT     Patient.Patient_name, Patient.Bill_no, Patient.Mobile,SUM( TestSetup.Fee) as Total, Patient.Payment_status,PatientTest.Created_at
FROM         Patient INNER JOIN
                      PatientTest ON Patient.Id = PatientTest.Patient_id INNER JOIN
                      TestSetup ON PatientTest.Test_setup_id = TestSetup.Id
                      where Patient.Payment_status !=1
                      group by Patient.Patient_name,Patient.Bill_no,Patient.Mobile,Patient.Payment_status,PatientTest.Created_at


GO
SET IDENTITY_INSERT [dbo].[Patient] ON 

INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (1, N'Jamil Hossain', CAST(N'1994-03-02' AS Date), N'01819845700', N'BITM-6155', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2017-02-14' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (2, N'Jubair Islam', CAST(N'1994-03-02' AS Date), N'01676749372', N'DIAG-06906', 0, CAST(650.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (3, N'Jubair Islam', CAST(N'1994-03-02' AS Date), N'01676749360', N'DIAG-09657', 0, CAST(900.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (4, N'Akash', CAST(N'1994-03-02' AS Date), N'01677674158', N'DIAG-07490', 0, CAST(400.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (5, N'Dhrubo', CAST(N'1994-03-02' AS Date), N'01245785426', N'DIAG-03687', 0, CAST(500.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (6, N'Annan', CAST(N'1994-03-02' AS Date), N'21010245741', N'DIAG-09282', 0, CAST(150.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (7, N'Shuvo', CAST(N'1997-05-05' AS Date), N'10203014152', N'DIAG-09025', 0, CAST(400.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (8, N'Jamil', CAST(N'1980-07-07' AS Date), N'01676745214', N'DIAG-04008', 0, CAST(400.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (9, N'Mohona', CAST(N'2010-02-03' AS Date), N'01674852478', N'DIAG-146P6230', 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (10, N'Dhrubo', CAST(N'1994-03-02' AS Date), N'12121212121212', N'DIAG-213P9448', 0, CAST(150.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (11, N'gfdg', CAST(N'1980-07-07' AS Date), N'021464565', N'DIAG-266P8079', 0, CAST(500.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (12, N'ewtg', CAST(N'1980-07-07' AS Date), N'0124', N'DIAG-288P6088', 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (13, N'Sakib', CAST(N'1997-05-05' AS Date), N'01674875125', N'DIAG-458P1133', 0, CAST(400.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (14, N'ada', CAST(N'2010-02-03' AS Date), N'012454784', N'DIAG-525P7990', 0, CAST(900.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (15, N'qwerty', CAST(N'1980-07-07' AS Date), N'012457845', N'DIAG-55P3635', 0, CAST(200.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (16, N'Sowmik', CAST(N'1980-07-07' AS Date), N'01674745421', N'DIAG-021P6150', 0, CAST(1100.00 AS Decimal(18, 2)), CAST(N'2017-02-25' AS Date))
INSERT [dbo].[Patient] ([Id], [Patient_name], [Date_of_birth], [Mobile], [Bill_no], [Payment_status], [Due_amount], [Created_at]) VALUES (17, N'Rubel', CAST(N'1994-03-02' AS Date), N'01245785462', N'DIAG-146A5061', 1, CAST(0.00 AS Decimal(18, 2)), CAST(N'2017-02-26' AS Date))
SET IDENTITY_INSERT [dbo].[Patient] OFF
SET IDENTITY_INSERT [dbo].[PatientTest] ON 

INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (1, 1, 1, CAST(N'2017-02-14' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (4, 1, 3, CAST(N'2017-02-14' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (5, 4, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (6, 5, 5, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (7, 6, 4, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (8, 7, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (9, 8, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (10, 9, 3, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (11, 10, 4, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (12, 11, 5, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (13, 12, 3, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (14, 13, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (15, 14, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (16, 14, 5, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (17, 15, 3, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (18, 16, 5, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (19, 16, 1, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (20, 16, 3, CAST(N'2017-02-25' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (21, 17, 5, CAST(N'2017-02-26' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (22, 17, 3, CAST(N'2017-02-26' AS Date))
INSERT [dbo].[PatientTest] ([Id], [Patient_id], [Test_setup_id], [Created_at]) VALUES (23, 17, 1, CAST(N'2017-02-26' AS Date))
SET IDENTITY_INSERT [dbo].[PatientTest] OFF
SET IDENTITY_INSERT [dbo].[TestSetup] ON 

INSERT [dbo].[TestSetup] ([Id], [Test_name], [Fee], [Type_id], [Created_at]) VALUES (1, N'Complete Blood count', CAST(400.00 AS Decimal(18, 2)), 1, CAST(N'2017-02-14' AS Date))
INSERT [dbo].[TestSetup] ([Id], [Test_name], [Fee], [Type_id], [Created_at]) VALUES (3, N'Hand X-ray ', CAST(200.00 AS Decimal(18, 2)), 2, CAST(N'2017-02-14' AS Date))
INSERT [dbo].[TestSetup] ([Id], [Test_name], [Fee], [Type_id], [Created_at]) VALUES (4, N'RBS', CAST(150.00 AS Decimal(18, 2)), 1, CAST(N'2017-02-19' AS Date))
INSERT [dbo].[TestSetup] ([Id], [Test_name], [Fee], [Type_id], [Created_at]) VALUES (5, N'Urine Test', CAST(500.00 AS Decimal(18, 2)), 4, CAST(N'2017-02-23' AS Date))
INSERT [dbo].[TestSetup] ([Id], [Test_name], [Fee], [Type_id], [Created_at]) VALUES (6, N'Pregnancy Profile', CAST(-500.00 AS Decimal(18, 2)), 5, CAST(N'2017-02-26' AS Date))
SET IDENTITY_INSERT [dbo].[TestSetup] OFF
SET IDENTITY_INSERT [dbo].[TestType] ON 

INSERT [dbo].[TestType] ([Id], [Test_type_name], [Created_at]) VALUES (1, N'Blood', CAST(N'2017-02-14' AS Date))
INSERT [dbo].[TestType] ([Id], [Test_type_name], [Created_at]) VALUES (2, N'X-Ray', CAST(N'2017-02-14' AS Date))
INSERT [dbo].[TestType] ([Id], [Test_type_name], [Created_at]) VALUES (3, N'Echo', CAST(N'2017-02-19' AS Date))
INSERT [dbo].[TestType] ([Id], [Test_type_name], [Created_at]) VALUES (4, N'Urine', CAST(N'2017-02-23' AS Date))
INSERT [dbo].[TestType] ([Id], [Test_type_name], [Created_at]) VALUES (5, N'USG', CAST(N'2017-02-26' AS Date))
SET IDENTITY_INSERT [dbo].[TestType] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Patient]    Script Date: 2017-02-26 01:32:52 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Patient] ON [dbo].[Patient]
(
	[Mobile] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Patient_1]    Script Date: 2017-02-26 01:32:52 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Patient_1] ON [dbo].[Patient]
(
	[Bill_no] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TestSetup]    Script Date: 2017-02-26 01:32:52 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TestSetup] ON [dbo].[TestSetup]
(
	[Test_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TestType]    Script Date: 2017-02-26 01:32:52 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TestType] ON [dbo].[TestType]
(
	[Test_type_name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTest_Patient] FOREIGN KEY([Patient_id])
REFERENCES [dbo].[Patient] ([Id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTest_Patient]
GO
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTest_TestSetup] FOREIGN KEY([Test_setup_id])
REFERENCES [dbo].[TestSetup] ([Id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTest_TestSetup]
GO
ALTER TABLE [dbo].[TestSetup]  WITH CHECK ADD  CONSTRAINT [FK_TestSetup_TestType] FOREIGN KEY([Type_id])
REFERENCES [dbo].[TestType] ([Id])
GO
ALTER TABLE [dbo].[TestSetup] CHECK CONSTRAINT [FK_TestSetup_TestType]
GO
USE [master]
GO
ALTER DATABASE [DiagnosticCareAppDB] SET  READ_WRITE 
GO
