/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

USE [CatalogDb]
GO

SET IDENTITY_INSERT [dbo].[CatalogType] ON

INSERT INTO [dbo].[CatalogType]
      ([Id], [Name])
VALUES
      (1, 'Mug')
		  ,
      (2, 'T-Shirt')
		  ,
      (3, 'Pin')

SET IDENTITY_INSERT [dbo].[CatalogType] OFF
GO

SET IDENTITY_INSERT [dbo].[CatalogBrand] ON

INSERT INTO [dbo].[CatalogBrand]
      ([Id], [Name])
VALUES
      (1, '.NET')
		  ,
      (2, 'Other')

SET IDENTITY_INSERT [dbo].[CatalogBrand] OFF
GO

SET IDENTITY_INSERT [dbo].[Catalog] ON

INSERT INTO [dbo].[Catalog]
      ([Id]
      ,[CatalogTypeId]
      ,[CatalogBrandId]
      ,[Description]
      ,[Name]
      ,[Price]
      ,[PictureFileName]
      ,[AvailableStock]
      ,[IsOnReorder])
VALUES
      (1, 2, 1, '.NET Bot Black Hoodie, and more', '.NET Bot Black Hoodie', 19.5, '1.png', 100, 0)
,
      (2, 1, 1, '.NET Black & White Mug', '.NET Black & White Mug', 8.50, '2.png', 89, 1)
,
      (3, 2, 2, 'Prism White T-Shirt', 'Prism White T-Shirt', 12, '3.png', 56, 0)
,
      (4, 2, 1, '.NET Foundation T-shirt', '.NET Foundation T-shirt', 12, '4.png', 120, 0)
,
      (5, 3, 2, 'Roslyn Red Pin', 'Roslyn Red Pin', 8.5, '5.png', 55, 0)
,
      (6, 2, 1, '.NET Blue Hoodie', '.NET Blue Hoodie', 12, '6.png', 17, 0)
,
      (7, 2, 2, 'Roslyn Red T-Shirt', 'Roslyn Red T-Shirt', 12, '7.png', 8, 0)
,
      (8, 2, 2, 'Kudu Purple Hoodie', 'Kudu Purple Hoodie', 8.5, '8.png', 34, 0)
,
      (9, 1, 2, 'Cup<T> White Mug', 'Cup<T> White Mug', 12, '9.png', 76, 0)
,
      (10, 3, 1, '.NET Foundation Pin', '.NET Foundation Pin', 12, '10.png', 11, 0)
,
      (11, 3, 1, 'Cup<T> Pin', 'Cup<T> Pin', 8.5, '11.png', 3, 0)
,
      (12, 2, 2, 'Prism White TShirt', 'Prism White TShirt', 12, '12.png', 0, 0)
,
      (13, 1, 1, 'Modern .NET Black & White Mug', 'Modern .NET Black & White Mug', 8.50, '13.png', 89, 1)
,
      (14, 1, 2, 'Modern Cup<T> White Mug', 'Modern Cup<T> White Mug', 12, '14.png', 76, 0)

SET IDENTITY_INSERT [dbo].[Catalog] OFF
GO

-- CatalogTypeId,CatalogBrandId,Description,Name,Price,PictureFileName,AvailableStock,IsOnReorder
-- 1,2,1,'.NET Bot Black Hoodie, and more','.NET Bot Black Hoodie',19.5,'1.png',100,0
-- 2,1,1,'.NET Black & White Mug','.NET Black & White Mug',8.50,'2.png',89,1
-- 3,2,2,'Prism White T-Shirt','Prism White T-Shirt',12,'3.png',56,0
-- 4,2,1,'.NET Foundation T-shirt','.NET Foundation T-shirt',12,'4.png',120,0
-- 5,3,2,'Roslyn Red Pin','Roslyn Red Pin',8.5,'5.png',55,0
-- 6,2,1,'.NET Blue Hoodie','.NET Blue Hoodie',12,'6.png',17,0
-- 7,2,2,'Roslyn Red T-Shirt','Roslyn Red T-Shirt',12,'7.png',8,0
-- 8,2,2,'Kudu Purple Hoodie','Kudu Purple Hoodie',8.5,'8.png',34,0
-- 9,1,2,'Cup<T> White Mug','Cup<T> White Mug',12,'9.png',76,0
-- 10,3,1,'.NET Foundation Pin','.NET Foundation Pin',12,'10.png',11,0
-- 11,3,1,'Cup<T> Pin','Cup<T> Pin',8.5,'11.png',3,0
-- 12,2,2,'Prism White TShirt','Prism White TShirt',12,'12.png',0,0
-- 13,1,1,'Modern .NET Black & White Mug','Modern .NET Black & White Mug',8.50,'13.png',89,1
-- 14,1,2,'Modern Cup<T> White Mug','Modern Cup<T> White Mug',12,'14.png',76,0