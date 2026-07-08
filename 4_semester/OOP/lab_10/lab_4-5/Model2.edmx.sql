
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/28/2026 13:11:17
-- Generated from EDMX file: D:\BSTU\2_course\4_semester\OOP\lab_9\lab_4-5\Model2.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PubgMarketDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CategoriesSet'
CREATE TABLE [dbo].[CategoriesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'SkinsSet'
CREATE TABLE [dbo].[SkinsSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ShortName] nvarchar(max)  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [Quantity] int  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Rarity] nvarchar(max)  NOT NULL,
    [ImagePath] nvarchar(max)  NOT NULL,
    [SoldCount] int  NOT NULL
);
GO

-- Creating table 'UsersSet'
CREATE TABLE [dbo].[UsersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CategoriesSet'
ALTER TABLE [dbo].[CategoriesSet]
ADD CONSTRAINT [PK_CategoriesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SkinsSet'
ALTER TABLE [dbo].[SkinsSet]
ADD CONSTRAINT [PK_SkinsSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsersSet'
ALTER TABLE [dbo].[UsersSet]
ADD CONSTRAINT [PK_UsersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------