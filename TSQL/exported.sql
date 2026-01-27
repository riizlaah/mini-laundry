IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Categories] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
);

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [PhoneNum] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);

CREATE TABLE [Jobs] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id])
);

CREATE TABLE [Packages] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Duration] int NOT NULL,
    CONSTRAINT [PK_Packages] PRIMARY KEY ([Id])
);

CREATE TABLE [Units] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Units] PRIMARY KEY ([Id])
);

CREATE TABLE [Employees] (
    [Id] int NOT NULL IDENTITY,
    [JobId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [PhoneNum] nvarchar(max) NOT NULL,
    [Address] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Salary] numeric(18,0) NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Employees_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Services] (
    [Id] int NOT NULL IDENTITY,
    [CategoryId] int NOT NULL,
    [UnitId] int NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [Price] int NOT NULL,
    [EstimationDuration] int NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Services_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Services_Units_UnitId] FOREIGN KEY ([UnitId]) REFERENCES [Units] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [HeaderTransactions] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [EmployeeId] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [CompleteEstDate] datetime2 NOT NULL,
    CONSTRAINT [PK_HeaderTransactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_HeaderTransactions_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HeaderTransactions_Employees_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [DetailPackages] (
    [Id] int NOT NULL IDENTITY,
    [PackageId] int NOT NULL,
    [ServiceId] int NOT NULL,
    [TotalUnitService] int NOT NULL,
    CONSTRAINT [PK_DetailPackages] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetailPackages_Packages_PackageId] FOREIGN KEY ([PackageId]) REFERENCES [Packages] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DetailPackages_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [DetailTransactions] (
    [Id] int NOT NULL IDENTITY,
    [ServiceId] int NULL,
    [HeaderTransactionId] int NOT NULL,
    [PackageId] int NULL,
    [Price] int NOT NULL,
    [TotalUnit] real NOT NULL,
    [CompletedAt] datetime2 NULL,
    CONSTRAINT [PK_DetailTransactions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_DetailTransactions_HeaderTransactions_HeaderTransactionId] FOREIGN KEY ([HeaderTransactionId]) REFERENCES [HeaderTransactions] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_DetailTransactions_Packages_PackageId] FOREIGN KEY ([PackageId]) REFERENCES [Packages] ([Id]),
    CONSTRAINT [FK_DetailTransactions_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([Id])
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] ON;
INSERT INTO [Categories] ([Id], [Name])
VALUES (1, N'Kiloan'),
(2, N'Meteran');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Categories]'))
    SET IDENTITY_INSERT [Categories] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'PhoneNum') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] ON;
INSERT INTO [Customers] ([Id], [Address], [Name], [PhoneNum])
VALUES (1, N'Batang', N'Alok', N'+6256488976532'),
(2, N'Pekalongan', N'Bowo', N'+6287655387653'),
(3, N'Tegal', N'Budiono', N'+6280766498076');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Name', N'PhoneNum') AND [object_id] = OBJECT_ID(N'[Customers]'))
    SET IDENTITY_INSERT [Customers] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Jobs]'))
    SET IDENTITY_INSERT [Jobs] ON;
INSERT INTO [Jobs] ([Id], [Name])
VALUES (1, N'Administrator'),
(2, N'Pencuci'),
(3, N'Penyetrika'),
(4, N'Kurir');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Jobs]'))
    SET IDENTITY_INSERT [Jobs] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Duration', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Packages]'))
    SET IDENTITY_INSERT [Packages] ON;
INSERT INTO [Packages] ([Id], [Description], [Duration], [Name], [Price])
VALUES (1, N'Paket untuk hari raya', 14, N'Paket Hari Raya', 100000),
(2, N'Paket yang mendekati kecepatan cahaya', 6, N'Paket Kilat', 150000);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Duration', N'Name', N'Price') AND [object_id] = OBJECT_ID(N'[Packages]'))
    SET IDENTITY_INSERT [Packages] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] ON;
INSERT INTO [Units] ([Id], [Name])
VALUES (1, N'Kg'),
(2, N'm');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[Units]'))
    SET IDENTITY_INSERT [Units] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'DateOfBirth', N'Email', N'JobId', N'Name', N'Password', N'PhoneNum', N'Salary') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] ON;
INSERT INTO [Employees] ([Id], [Address], [DateOfBirth], [Email], [JobId], [Name], [Password], [PhoneNum], [Salary])
VALUES (1, N'Bumi', '2000-01-01T00:00:00.0000000', N'admin@penatu.id', 1, N'Admin', N'p4s?', N'+6289988776655', 3000000.0),
(2, N'Mars', '2003-08-07T00:00:00.0000000', N'hartono09@gmail.com', 2, N'Hartono', N'p4s?', N'+62876533876532', 2500000.0),
(3, N'Ds. Klewer, Kec. Tulis', '2002-05-17T00:00:00.0000000', N'ubed25@gmail.com', 3, N'Ubed', N'p4s?', N'+6286544987320', 2500000.0),
(4, N'Ds. Sengon, Kec. Subah', '2003-08-07T00:00:00.0000000', N'komar@gmail.com', 4, N'Komar', N'p4s?', N'+6289267530098', 2500000.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'DateOfBirth', N'Email', N'JobId', N'Name', N'Password', N'PhoneNum', N'Salary') AND [object_id] = OBJECT_ID(N'[Employees]'))
    SET IDENTITY_INSERT [Employees] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'EstimationDuration', N'Name', N'Price', N'UnitId') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] ON;
INSERT INTO [Services] ([Id], [CategoryId], [EstimationDuration], [Name], [Price], [UnitId])
VALUES (1, 1, 4, N'Cuci Kiloan', 20000, 1),
(2, 1, 6, N'Cuci Setrika', 30000, 1),
(3, 1, 1, N'Cuci Kilat', 30000, 1),
(4, 1, 1, N'Setrika Kilat', 20000, 1),
(5, 2, 2, N'Cuci Korden', 10000, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CategoryId', N'EstimationDuration', N'Name', N'Price', N'UnitId') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PackageId', N'ServiceId', N'TotalUnitService') AND [object_id] = OBJECT_ID(N'[DetailPackages]'))
    SET IDENTITY_INSERT [DetailPackages] ON;
INSERT INTO [DetailPackages] ([Id], [PackageId], [ServiceId], [TotalUnitService])
VALUES (1, 1, 1, 1),
(2, 1, 2, 2),
(3, 1, 5, 2),
(4, 2, 3, 3),
(5, 2, 4, 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'PackageId', N'ServiceId', N'TotalUnitService') AND [object_id] = OBJECT_ID(N'[DetailPackages]'))
    SET IDENTITY_INSERT [DetailPackages] OFF;

CREATE INDEX [IX_DetailPackages_PackageId] ON [DetailPackages] ([PackageId]);

CREATE INDEX [IX_DetailPackages_ServiceId] ON [DetailPackages] ([ServiceId]);

CREATE INDEX [IX_DetailTransactions_HeaderTransactionId] ON [DetailTransactions] ([HeaderTransactionId]);

CREATE INDEX [IX_DetailTransactions_PackageId] ON [DetailTransactions] ([PackageId]);

CREATE INDEX [IX_DetailTransactions_ServiceId] ON [DetailTransactions] ([ServiceId]);

CREATE INDEX [IX_Employees_JobId] ON [Employees] ([JobId]);

CREATE INDEX [IX_HeaderTransactions_CustomerId] ON [HeaderTransactions] ([CustomerId]);

CREATE INDEX [IX_HeaderTransactions_EmployeeId] ON [HeaderTransactions] ([EmployeeId]);

CREATE INDEX [IX_Services_CategoryId] ON [Services] ([CategoryId]);

CREATE INDEX [IX_Services_UnitId] ON [Services] ([UnitId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260127004610_InitialCreate', N'10.0.2');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [HeaderTransactions] ADD [DiscountToken] nvarchar(450) NULL;

CREATE TABLE [Discount] (
    [Token] nvarchar(450) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [From] datetime2 NOT NULL,
    [To] datetime2 NOT NULL,
    CONSTRAINT [PK_Discount] PRIMARY KEY ([Token])
);

CREATE INDEX [IX_HeaderTransactions_DiscountToken] ON [HeaderTransactions] ([DiscountToken]);

ALTER TABLE [HeaderTransactions] ADD CONSTRAINT [FK_HeaderTransactions_Discount_DiscountToken] FOREIGN KEY ([DiscountToken]) REFERENCES [Discount] ([Token]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260127065636_tryDiscount', N'10.0.2');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [Discount] ADD [Value] real NOT NULL DEFAULT CAST(0 AS real);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260127065941_tryDiscount2', N'10.0.2');

COMMIT;
GO

