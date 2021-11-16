CREATE TABLE [dbo].[Catalog]
(
	[Id] BIGINT NOT NULL IDENTITY (1,1), 
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Price] DECIMAL(20, 5) NOT NULL, 
    [PictureFileName] NVARCHAR(MAX) NULL, 
    [CatalogTypeId] INT NOT NULL, 
    [CatalogBrandId] INT NOT NULL,
    [AvailableStock] INT NOT NULL, 
    [MaxStockThreshold] INT NULL, 
    [IsOnReorder] BIT NOT NULL,
    [RestockThreshold] INT NULL
    CONSTRAINT [PK_Catalog_Id] PRIMARY KEY CLUSTERED (Id ASC),
    CONSTRAINT [FK_Catalog_CatalogType_CatalogTypeId] FOREIGN KEY ([CatalogTypeId]) 
    REFERENCES [dbo].[CatalogType] ([Id]) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE,
    CONSTRAINT [FK_Catalog_CatalogBrand_CatalogBrandId] FOREIGN KEY ([CatalogBrandId]) 
    REFERENCES [dbo].[CatalogBrand] ([Id]) 
    ON DELETE CASCADE 
    ON UPDATE CASCADE
)
