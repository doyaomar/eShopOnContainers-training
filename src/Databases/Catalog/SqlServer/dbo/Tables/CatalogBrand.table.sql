﻿CREATE TABLE [dbo].[CatalogBrand]
(
	[Id] INT NOT NULL IDENTITY (1,1),
	[Name] NVARCHAR(100) NOT NULL,
	CONSTRAINT [PK_CatalogBrand_Id] PRIMARY KEY CLUSTERED ([Id] ASC)
)