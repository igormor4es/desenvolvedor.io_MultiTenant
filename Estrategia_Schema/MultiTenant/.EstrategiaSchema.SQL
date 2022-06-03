
CREATE SCHEMA [tenant-1];

BEGIN TRANSACTION;
GO

CREATE TABLE [tenant-1].[People] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [TenantId] nvarchar(max) NULL,
    CONSTRAINT [PK_People] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [tenant-1].[Products] (
    [Id] int NOT NULL IDENTITY,
    [Description] nvarchar(max) NULL,
    [TenantId] nvarchar(max) NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'TenantId') AND [object_id] = OBJECT_ID(N'[tenant-1].[People]'))
    SET IDENTITY_INSERT [tenant-1].[People] ON;
INSERT INTO [tenant-1].[People] ([Id], [Name], [TenantId])
VALUES (1, N'Person 1', N'tenant-1'),
(2, N'Person 2', N'tenant-2'),
(3, N'Person 3', N'tenant-2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'TenantId') AND [object_id] = OBJECT_ID(N'[tenant-1].[People]'))
    SET IDENTITY_INSERT [tenant-1].[People] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'TenantId') AND [object_id] = OBJECT_ID(N'[tenant-1].[Products]'))
    SET IDENTITY_INSERT [tenant-1].[Products] ON;
INSERT INTO [tenant-1].[Products] ([Id], [Description], [TenantId])
VALUES (1, N'Description 1', N'tenant-1'),
(2, N'Description 2', N'tenant-2'),
(3, N'Description 3', N'tenant-2');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'TenantId') AND [object_id] = OBJECT_ID(N'[tenant-1].[Products]'))
    SET IDENTITY_INSERT [tenant-1].[Products] OFF;
GO

COMMIT;
GO

