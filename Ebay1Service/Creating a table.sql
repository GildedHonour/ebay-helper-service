USE [Ebay]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Listings](
	[ListingID] [varchar](50) NOT NULL,
	[MPN] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[SellingPrice] [numeric](18, 2) NOT NULL,
	[IsUsed] [bit] NOT NULL,
 CONSTRAINT [PK_Listings] PRIMARY KEY CLUSTERED 
(
	[ListingID] ASC,
	[MPN] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Listings] ADD  CONSTRAINT [DF_Listings_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO

ALTER TABLE [dbo].[Listings] ADD  CONSTRAINT [DF_Listings_SellingPrice]  DEFAULT ((9999.99)) FOR [SellingPrice]
GO

ALTER TABLE [dbo].[Listings] ADD  DEFAULT ((0)) FOR [IsUsed]
GO

