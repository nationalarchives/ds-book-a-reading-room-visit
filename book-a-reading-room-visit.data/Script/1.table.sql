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
GO

CREATE TABLE [OrderStatus] (
    [Id] int NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [SeatType] (
    [Id] int NOT NULL,
    [Description] nvarchar(150) NOT NULL,
    CONSTRAINT [PK_SeatType] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Seats] (
    [Id] int NOT NULL,
    [Number] nvarchar(10) NOT NULL,
    [SeatTypeId] int NULL,
    [Description] nvarchar(50) NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Seats_SeatType_SeatTypeId] FOREIGN KEY ([SeatTypeId]) REFERENCES [SeatType] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CompletedDate] datetime2 NOT NULL,
    [OrderReference] nvarchar(50) NOT NULL,
    [IsStandardVisit] bit NOT NULL,
    [SeatId] int NULL,
    [OrderStatusId] int NULL,
    [StartDate] datetime2 NOT NULL,
    [EndDate] datetime2 NOT NULL,
    [ReaderTicket] int NOT NULL,
    [Email] nvarchar(100) NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_OrderStatus_OrderStatusId] FOREIGN KEY ([OrderStatusId]) REFERENCES [OrderStatus] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Orders_Seats_SeatId] FOREIGN KEY ([SeatId]) REFERENCES [Seats] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OrderDocuments] (
    [Id] int NOT NULL IDENTITY,
    [DocumentReference] nvarchar(50) NOT NULL,
    [OrderId] int NULL,
    [LetterCode] nvarchar(20) NULL,
    [ClassNumber] int NOT NULL,
    [PieceId] int NOT NULL,
    [PieceReference] nvarchar(20) NULL,
    [SubClassNumber] int NOT NULL,
    [ItemReference] nvarchar(20) NULL,
    [Site] nvarchar(20) NULL,
    [IsReserve] bit NOT NULL,
    CONSTRAINT [PK_OrderDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDocuments_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[OrderStatus]'))
    SET IDENTITY_INSERT [OrderStatus] ON;
INSERT INTO [OrderStatus] ([Id], [Description])
VALUES (1, N'Created'),
(2, N'Submitted'),
(3, N'Cancelled');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[OrderStatus]'))
    SET IDENTITY_INSERT [OrderStatus] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[SeatType]'))
    SET IDENTITY_INSERT [SeatType] ON;
INSERT INTO [SeatType] ([Id], [Description])
VALUES (1, N'Standard reading room seat'),
(2, N'Standard reading room seat with camera stand'),
(3, N'Map and large document room seat'),
(4, N'Bulk document order seat'),
(5, N'Not available');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description') AND [object_id] = OBJECT_ID(N'[SeatType]'))
    SET IDENTITY_INSERT [SeatType] OFF;
GO

CREATE INDEX [IX_OrderDocuments_OrderId] ON [OrderDocuments] ([OrderId]);
GO

CREATE INDEX [IX_Orders_OrderStatusId] ON [Orders] ([OrderStatusId]);
GO

CREATE INDEX [IX_Orders_SeatId] ON [Orders] ([SeatId]);
GO

CREATE INDEX [IX_Seats_SeatTypeId] ON [Seats] ([SeatTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210218150657_init', N'5.0.3');
GO

COMMIT;
GO

