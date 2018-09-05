USE [BarqBECoreMock]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[MSISDN] [nvarchar](50) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[MPin] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[LastOTPID] [int] NULL,
	[PasswordTrials] [int] NOT NULL,
	[MPinTrials] [int] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountStatus]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountStatus](
	[ID] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_AccountStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OTP]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OTP](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Status] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[ConsumptionTime] [datetime] NULL,
 CONSTRAINT [PK_OTP] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OTPStatus]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OTPStatus](
	[ID] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_OTPStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TransactionID] [nvarchar](50) NOT NULL,
	[FromAccount] [int] NOT NULL,
	[ToAccount] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[IssueTime] [datetime] NOT NULL,
	[LastUpdateTime] [datetime] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[CurrencyCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionStatus]    Script Date: 9/4/2018 5:57:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionStatus](
	[ID] [int] NOT NULL,
	[Description] [nchar](10) NOT NULL,
 CONSTRAINT [PK_TransactionStatus] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([ID], [Name], [MSISDN], [Balance], [Password], [MPin], [Status], [LastOTPID], [PasswordTrials], [MPinTrials]) VALUES (3, N'Ahmed Bahaa', N'123', CAST(344.80 AS Decimal(18, 2)), N'123', N'123', 1, 7, 0, 0)
GO
INSERT [dbo].[Account] ([ID], [Name], [MSISDN], [Balance], [Password], [MPin], [Status], [LastOTPID], [PasswordTrials], [MPinTrials]) VALUES (4, N'Ahmed Shabaan', N'1234', CAST(655.20 AS Decimal(18, 2)), N'123', N'123', 1, 4, 0, 0)
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
INSERT [dbo].[AccountStatus] ([ID], [Description]) VALUES (1, N'Opened')
GO
INSERT [dbo].[AccountStatus] ([ID], [Description]) VALUES (2, N'Closed')
GO
SET IDENTITY_INSERT [dbo].[OTP] ON 
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (2, N'123', 1, CAST(N'2018-08-30T00:00:00.000' AS DateTime), CAST(N'2018-08-30T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (3, N'V20RD6QU', 2, CAST(N'2018-08-30T17:26:59.827' AS DateTime), CAST(N'2018-08-30T17:33:58.960' AS DateTime))
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (4, N'MX4W86DV', 2, CAST(N'2018-08-30T19:31:06.253' AS DateTime), CAST(N'2018-08-30T19:33:16.207' AS DateTime))
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (5, N'8ZUSGSI1', 1, CAST(N'2018-09-02T15:00:42.260' AS DateTime), NULL)
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (6, N'4Q2WBTQ1', 1, CAST(N'2018-09-02T18:51:57.480' AS DateTime), NULL)
GO
INSERT [dbo].[OTP] ([ID], [Code], [Status], [CreationTime], [ConsumptionTime]) VALUES (7, N'LPDAK2HK', 1, CAST(N'2018-09-02T18:59:58.697' AS DateTime), NULL)
GO
SET IDENTITY_INSERT [dbo].[OTP] OFF
GO
INSERT [dbo].[OTPStatus] ([ID], [Description]) VALUES (1, N'Opened')
GO
INSERT [dbo].[OTPStatus] ([ID], [Description]) VALUES (2, N'Closed')
GO
SET IDENTITY_INSERT [dbo].[Transaction] ON 
GO
INSERT [dbo].[Transaction] ([ID], [TransactionID], [FromAccount], [ToAccount], [Status], [IssueTime], [LastUpdateTime], [Amount], [CurrencyCode]) VALUES (1, N'UASD-346874-HS', 3, 4, 2, CAST(N'2018-08-30T19:19:32.090' AS DateTime), CAST(N'2018-08-30T19:19:32.103' AS DateTime), CAST(55.20 AS Decimal(18, 2)), N'SAR')
GO
INSERT [dbo].[Transaction] ([ID], [TransactionID], [FromAccount], [ToAccount], [Status], [IssueTime], [LastUpdateTime], [Amount], [CurrencyCode]) VALUES (2, N'UASD-346874-aaa', 3, 4, 2, CAST(N'2018-08-30T19:23:07.323' AS DateTime), CAST(N'2018-08-30T19:23:07.323' AS DateTime), CAST(55.20 AS Decimal(18, 2)), N'SAR')
GO
INSERT [dbo].[Transaction] ([ID], [TransactionID], [FromAccount], [ToAccount], [Status], [IssueTime], [LastUpdateTime], [Amount], [CurrencyCode]) VALUES (3, N'UASD-346874-bbb', 3, 4, 2, CAST(N'2018-08-30T19:23:20.320' AS DateTime), CAST(N'2018-08-30T19:23:20.320' AS DateTime), CAST(55.20 AS Decimal(18, 2)), N'SAR')
GO
INSERT [dbo].[Transaction] ([ID], [TransactionID], [FromAccount], [ToAccount], [Status], [IssueTime], [LastUpdateTime], [Amount], [CurrencyCode]) VALUES (4, N'UASD-346874-HS', 3, 3, 2, CAST(N'2018-09-03T13:22:20.450' AS DateTime), CAST(N'2018-09-03T13:22:20.450' AS DateTime), CAST(20.00 AS Decimal(18, 2)), N'SAR')
GO
INSERT [dbo].[Transaction] ([ID], [TransactionID], [FromAccount], [ToAccount], [Status], [IssueTime], [LastUpdateTime], [Amount], [CurrencyCode]) VALUES (5, N'UASD-346874-HS', 3, 4, 2, CAST(N'2018-09-04T16:26:26.933' AS DateTime), CAST(N'2018-09-04T16:26:26.933' AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'SAR')
GO
SET IDENTITY_INSERT [dbo].[Transaction] OFF
GO
INSERT [dbo].[TransactionStatus] ([ID], [Description]) VALUES (1, N'Opened    ')
GO
INSERT [dbo].[TransactionStatus] ([ID], [Description]) VALUES (2, N'Closed    ')
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_PasswordTrials]  DEFAULT ((0)) FOR [PasswordTrials]
GO
ALTER TABLE [dbo].[Account] ADD  CONSTRAINT [DF_Account_MPinTrials]  DEFAULT ((0)) FOR [MPinTrials]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_AccountStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[AccountStatus] ([ID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_AccountStatus]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_OTP] FOREIGN KEY([LastOTPID])
REFERENCES [dbo].[OTP] ([ID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_OTP]
GO
ALTER TABLE [dbo].[OTP]  WITH CHECK ADD  CONSTRAINT [FK_OTP_OTPStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[OTPStatus] ([ID])
GO
ALTER TABLE [dbo].[OTP] CHECK CONSTRAINT [FK_OTP_OTPStatus]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account] FOREIGN KEY([FromAccount])
REFERENCES [dbo].[Account] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Account1] FOREIGN KEY([ToAccount])
REFERENCES [dbo].[Account] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Account1]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_TransactionStatus] FOREIGN KEY([Status])
REFERENCES [dbo].[TransactionStatus] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_TransactionStatus]
GO
