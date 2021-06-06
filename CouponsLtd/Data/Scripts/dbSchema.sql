USE [lcl-coupons]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[User](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [varbinary](1024) NOT NULL,
	[PasswordSalt] [varbinary](1024) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[Coupon](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[Code] [nvarchar](max) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[UserCoupon](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[UserId] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [User](Id),
	[CouponId] [uniqueidentifier] NOT NULL FOREIGN KEY REFERENCES [Coupon](Id),
	[IsActive] [bit] NOT NULL,
	[Activated] [datetimeoffset](7) NOT NULL
) ON [PRIMARY]
GO