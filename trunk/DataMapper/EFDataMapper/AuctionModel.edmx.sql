
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/27/2014 11:11:41
-- Generated from EDMX file: D:\Facultate\Enterprise .Net\Auction\DataMapper\EFDataMapper\AuctionModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Auction];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_RoleUser_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleUser] DROP CONSTRAINT [FK_RoleUser_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RoleUser] DROP CONSTRAINT [FK_RoleUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_CategoryCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_CategoryCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_AuctionProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_RatingUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Ratings] DROP CONSTRAINT [FK_RatingUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_AuctionUser];
GO
IF OBJECT_ID(N'[dbo].[FK_AuctionCurrency]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Auctions] DROP CONSTRAINT [FK_AuctionCurrency];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCategory_Product]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCategory] DROP CONSTRAINT [FK_ProductCategory_Product];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductCategory_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductCategory] DROP CONSTRAINT [FK_ProductCategory_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductActionUser_ProductAction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductActionUser] DROP CONSTRAINT [FK_ProductActionUser_ProductAction];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductActionUser_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductActionUser] DROP CONSTRAINT [FK_ProductActionUser_User];
GO
IF OBJECT_ID(N'[dbo].[FK_ProductActionAuction]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProductActions] DROP CONSTRAINT [FK_ProductActionAuction];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Configurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configurations];
GO
IF OBJECT_ID(N'[dbo].[Currencies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Currencies];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Ratings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ratings];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[Auctions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Auctions];
GO
IF OBJECT_ID(N'[dbo].[ProductActions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductActions];
GO
IF OBJECT_ID(N'[dbo].[RoleUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RoleUser];
GO
IF OBJECT_ID(N'[dbo].[ProductCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCategory];
GO
IF OBJECT_ID(N'[dbo].[ProductActionUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductActionUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [IdRole] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [IdUser] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NULL,
    [LastName] nvarchar(max)  NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Configurations'
CREATE TABLE [dbo].[Configurations] (
    [IdConfiguration] int IDENTITY(1,1) NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Currencies'
CREATE TABLE [dbo].[Currencies] (
    [IdCurrency] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [IdCategory] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(200)  NULL,
    [IdParentCategory] int  NULL
);
GO

-- Creating table 'Ratings'
CREATE TABLE [dbo].[Ratings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Grade] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [UserIdUser] int  NOT NULL,
    [UserIdUser1] int  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [IdProduct] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(30)  NOT NULL,
    [Description] nvarchar(300)  NOT NULL
);
GO

-- Creating table 'Auctions'
CREATE TABLE [dbo].[Auctions] (
    [IdAuction] int IDENTITY(1,1) NOT NULL,
    [BeginDate] tinyint  NOT NULL,
    [EndDate] datetime  NOT NULL,
    [StartPrice] decimal(18,0)  NOT NULL,
    [Finished] bit  NOT NULL,
    [UserIdUser] int  NOT NULL,
    [CurrencyIdCurrency] int  NOT NULL,
    [Product_IdProduct] int  NOT NULL
);
GO

-- Creating table 'ProductActions'
CREATE TABLE [dbo].[ProductActions] (
    [IdProductAction] int IDENTITY(1,1) NOT NULL,
    [Price] float  NOT NULL,
    [Date] datetime  NOT NULL,
    [AuctionIdAuction] int  NOT NULL
);
GO

-- Creating table 'RoleUser'
CREATE TABLE [dbo].[RoleUser] (
    [Roles_IdRole] int  NOT NULL,
    [Users_IdUser] int  NOT NULL
);
GO

-- Creating table 'ProductCategory'
CREATE TABLE [dbo].[ProductCategory] (
    [Products_IdProduct] int  NOT NULL,
    [Categories_IdCategory] int  NOT NULL
);
GO

-- Creating table 'ProductActionUser'
CREATE TABLE [dbo].[ProductActionUser] (
    [ProductActions_IdProductAction] int  NOT NULL,
    [Users_IdUser] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IdRole] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([IdRole] ASC);
GO

-- Creating primary key on [IdUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([IdUser] ASC);
GO

-- Creating primary key on [IdConfiguration] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [PK_Configurations]
    PRIMARY KEY CLUSTERED ([IdConfiguration] ASC);
GO

-- Creating primary key on [IdCurrency] in table 'Currencies'
ALTER TABLE [dbo].[Currencies]
ADD CONSTRAINT [PK_Currencies]
    PRIMARY KEY CLUSTERED ([IdCurrency] ASC);
GO

-- Creating primary key on [IdCategory] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([IdCategory] ASC);
GO

-- Creating primary key on [Id] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [PK_Ratings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [IdProduct] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([IdProduct] ASC);
GO

-- Creating primary key on [IdAuction] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [PK_Auctions]
    PRIMARY KEY CLUSTERED ([IdAuction] ASC);
GO

-- Creating primary key on [IdProductAction] in table 'ProductActions'
ALTER TABLE [dbo].[ProductActions]
ADD CONSTRAINT [PK_ProductActions]
    PRIMARY KEY CLUSTERED ([IdProductAction] ASC);
GO

-- Creating primary key on [Roles_IdRole], [Users_IdUser] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [PK_RoleUser]
    PRIMARY KEY CLUSTERED ([Roles_IdRole], [Users_IdUser] ASC);
GO

-- Creating primary key on [Products_IdProduct], [Categories_IdCategory] in table 'ProductCategory'
ALTER TABLE [dbo].[ProductCategory]
ADD CONSTRAINT [PK_ProductCategory]
    PRIMARY KEY CLUSTERED ([Products_IdProduct], [Categories_IdCategory] ASC);
GO

-- Creating primary key on [ProductActions_IdProductAction], [Users_IdUser] in table 'ProductActionUser'
ALTER TABLE [dbo].[ProductActionUser]
ADD CONSTRAINT [PK_ProductActionUser]
    PRIMARY KEY CLUSTERED ([ProductActions_IdProductAction], [Users_IdUser] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Roles_IdRole] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [FK_RoleUser_Role]
    FOREIGN KEY ([Roles_IdRole])
    REFERENCES [dbo].[Roles]
        ([IdRole])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_IdUser] in table 'RoleUser'
ALTER TABLE [dbo].[RoleUser]
ADD CONSTRAINT [FK_RoleUser_User]
    FOREIGN KEY ([Users_IdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleUser_User'
CREATE INDEX [IX_FK_RoleUser_User]
ON [dbo].[RoleUser]
    ([Users_IdUser]);
GO

-- Creating foreign key on [IdParentCategory] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_CategoryCategory]
    FOREIGN KEY ([IdParentCategory])
    REFERENCES [dbo].[Categories]
        ([IdCategory])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryCategory'
CREATE INDEX [IX_FK_CategoryCategory]
ON [dbo].[Categories]
    ([IdParentCategory]);
GO

-- Creating foreign key on [Product_IdProduct] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_AuctionProduct]
    FOREIGN KEY ([Product_IdProduct])
    REFERENCES [dbo].[Products]
        ([IdProduct])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionProduct'
CREATE INDEX [IX_FK_AuctionProduct]
ON [dbo].[Auctions]
    ([Product_IdProduct]);
GO

-- Creating foreign key on [UserIdUser1] in table 'Ratings'
ALTER TABLE [dbo].[Ratings]
ADD CONSTRAINT [FK_RatingUser]
    FOREIGN KEY ([UserIdUser1])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RatingUser'
CREATE INDEX [IX_FK_RatingUser]
ON [dbo].[Ratings]
    ([UserIdUser1]);
GO

-- Creating foreign key on [UserIdUser] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_AuctionUser]
    FOREIGN KEY ([UserIdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionUser'
CREATE INDEX [IX_FK_AuctionUser]
ON [dbo].[Auctions]
    ([UserIdUser]);
GO

-- Creating foreign key on [CurrencyIdCurrency] in table 'Auctions'
ALTER TABLE [dbo].[Auctions]
ADD CONSTRAINT [FK_AuctionCurrency]
    FOREIGN KEY ([CurrencyIdCurrency])
    REFERENCES [dbo].[Currencies]
        ([IdCurrency])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AuctionCurrency'
CREATE INDEX [IX_FK_AuctionCurrency]
ON [dbo].[Auctions]
    ([CurrencyIdCurrency]);
GO

-- Creating foreign key on [Products_IdProduct] in table 'ProductCategory'
ALTER TABLE [dbo].[ProductCategory]
ADD CONSTRAINT [FK_ProductCategory_Product]
    FOREIGN KEY ([Products_IdProduct])
    REFERENCES [dbo].[Products]
        ([IdProduct])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Categories_IdCategory] in table 'ProductCategory'
ALTER TABLE [dbo].[ProductCategory]
ADD CONSTRAINT [FK_ProductCategory_Category]
    FOREIGN KEY ([Categories_IdCategory])
    REFERENCES [dbo].[Categories]
        ([IdCategory])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductCategory_Category'
CREATE INDEX [IX_FK_ProductCategory_Category]
ON [dbo].[ProductCategory]
    ([Categories_IdCategory]);
GO

-- Creating foreign key on [ProductActions_IdProductAction] in table 'ProductActionUser'
ALTER TABLE [dbo].[ProductActionUser]
ADD CONSTRAINT [FK_ProductActionUser_ProductAction]
    FOREIGN KEY ([ProductActions_IdProductAction])
    REFERENCES [dbo].[ProductActions]
        ([IdProductAction])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_IdUser] in table 'ProductActionUser'
ALTER TABLE [dbo].[ProductActionUser]
ADD CONSTRAINT [FK_ProductActionUser_User]
    FOREIGN KEY ([Users_IdUser])
    REFERENCES [dbo].[Users]
        ([IdUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductActionUser_User'
CREATE INDEX [IX_FK_ProductActionUser_User]
ON [dbo].[ProductActionUser]
    ([Users_IdUser]);
GO

-- Creating foreign key on [AuctionIdAuction] in table 'ProductActions'
ALTER TABLE [dbo].[ProductActions]
ADD CONSTRAINT [FK_ProductActionAuction]
    FOREIGN KEY ([AuctionIdAuction])
    REFERENCES [dbo].[Auctions]
        ([IdAuction])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProductActionAuction'
CREATE INDEX [IX_FK_ProductActionAuction]
ON [dbo].[ProductActions]
    ([AuctionIdAuction]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------