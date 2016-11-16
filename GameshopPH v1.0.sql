USE [gameshopph]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [uniqueidentifier] NOT NULL,
	[Username] [varchar](100) NULL,
	[Password] [varchar](100) NULL,
	[SecretQuestion] [text] NULL,
	[SecretAnswer] [varchar](100) NULL,
	[Email] [varchar](100) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Address](
	[AddressId] [uniqueidentifier] NOT NULL,
	[StreetOne] [varchar](20) NULL,
	[StreetTwo] [varchar](20) NULL,
	[StreetThree] [varchar](20) NULL,
	[Barangay] [varchar](20) NULL,
	[Municipality] [varchar](20) NULL,
	[City] [varchar](20) NULL,
	[ZipCode] [varchar](10) NULL,
	[Province] [varchar](20) NULL,
	[Region] [varchar](20) NULL,
	[Country] [varchar](20) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Advertisements]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertisements](
	[AdvertisementId] [uniqueidentifier] NOT NULL,
	[FriendlyId] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NULL,
	[Title] [varchar](100) NULL,
	[Description] [varchar](100) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Advertisements_1] PRIMARY KEY CLUSTERED 
(
	[AdvertisementId] ASC,
	[FriendlyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ContactInformation]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ContactInformation](
	[ContactInformationId] [uniqueidentifier] NOT NULL,
	[Email] [varchar](100) NULL,
	[ContactNumber] [varchar](13) NULL,
	[EmergencyContactNumber] [varchar](13) NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_ContactInformation] PRIMARY KEY CLUSTERED 
(
	[ContactInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Feedbacks]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Feedbacks](
	[ReviewId] [uniqueidentifier] NOT NULL,
	[RevieweeId] [uniqueidentifier] NOT NULL,
	[ReviewerId] [uniqueidentifier] NOT NULL,
	[Rating] [tinyint] NULL,
	[Review] [text] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Feedbacks] PRIMARY KEY CLUSTERED 
(
	[ReviewId] ASC,
	[RevieweeId] ASC,
	[ReviewerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Games]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Games](
	[GameId] [uniqueidentifier] NOT NULL,
	[AdvertisementId] [uniqueidentifier] NOT NULL,
	[SellingInformationId] [uniqueidentifier] NOT NULL,
	[TradingInformationId] [uniqueidentifier] NOT NULL,
	[GameGenre] [tinyint] NOT NULL,
	[GamePlatform] [tinyint] NOT NULL,
	[Title] [varchar](100) NULL,
	[Description] [text] NULL,
	[ReasonForSelling] [text] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Games_1] PRIMARY KEY CLUSTERED 
(
	[GameId] ASC,
	[AdvertisementId] ASC,
	[SellingInformationId] ASC,
	[TradingInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Haggle]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Haggle](
	[HaggleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Price] [money] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Haggle] PRIMARY KEY CLUSTERED 
(
	[HaggleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeetupInformation]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetupInformation](
	[MeetupInformationId] [uniqueidentifier] NOT NULL,
	[AdvertisementId] [uniqueidentifier] NOT NULL,
	[DayOfWeek] [tinyint] NULL,
	[StartTime] [datetime2](7) NULL,
	[EndTime] [datetime2](7) NULL,
	[Notes] [text] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_MeetupInformationId] PRIMARY KEY CLUSTERED 
(
	[MeetupInformationId] ASC,
	[AdvertisementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MeetupInformationAddresses]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetupInformationAddresses](
	[MeetupInformationId] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_MeetupInformationAddresses] PRIMARY KEY CLUSTERED 
(
	[MeetupInformationId] ASC,
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfileAddresses]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfileAddresses](
	[ProfileId] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ProfileAddresses] PRIMARY KEY CLUSTERED 
(
	[ProfileId] ASC,
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profiles]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profiles](
	[ProfileId] [uniqueidentifier] NOT NULL,
	[ContactInformationId] [uniqueidentifier] NOT NULL,
	[Salutation] [tinyint] NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Birthdate] [date] NULL,
	[Sex] [tinyint] NULL,
	[CivilStatus] [tinyint] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_Profiles] PRIMARY KEY CLUSTERED 
(
	[ProfileId] ASC,
	[ContactInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SellingInformation]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SellingInformation](
	[SellingInformationId] [uniqueidentifier] NOT NULL,
	[Currency] [tinyint] NULL,
	[Price] [money] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_SellingInformation] PRIMARY KEY CLUSTERED 
(
	[SellingInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TradingInformation]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TradingInformation](
	[TradingInformationId] [uniqueidentifier] NOT NULL,
	[Currency] [tinyint] NULL,
	[IsCredit] [bit] NULL,
	[Price] [money] NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_TradingInformation] PRIMARY KEY CLUSTERED 
(
	[TradingInformationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/9/2016 11:16:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [uniqueidentifier] NOT NULL,
	[ProfileId] [uniqueidentifier] NOT NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[CreatedDate] [datetime2](7) NULL,
	[ModifiedDate] [datetime2](7) NULL,
 CONSTRAINT [PK_temp2] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ProfileId] ASC,
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
