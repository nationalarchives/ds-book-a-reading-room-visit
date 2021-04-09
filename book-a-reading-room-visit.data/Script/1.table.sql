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

CREATE TABLE [BookingStatus] (
    [Id] int NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_BookingStatus] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [BookingType] (
    [Id] int NOT NULL,
    [Description] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_BookingType] PRIMARY KEY ([Id])
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
    [SeatTypeId] int NOT NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Seats_SeatType_SeatTypeId] FOREIGN KEY ([SeatTypeId]) REFERENCES [SeatType] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Bookings] (
    [Id] int NOT NULL IDENTITY,
    [CreatedDate] datetime2 NOT NULL,
    [CompleteByDate] datetime2 NULL,
    [BookingReference] nvarchar(50) NOT NULL,
    [BookingTypeId] int NOT NULL,
    [IsAcceptTsAndCs] bit NOT NULL,
    [IsAcceptCovidCharter] bit NOT NULL,
    [IsNoFaceCovering] bit NOT NULL,
    [IsNoShow] bit NOT NULL,
    [SeatId] int NOT NULL,
    [BookingStatusId] int NOT NULL,
    [Comments] nvarchar(max) NULL,
    [VisitStartDate] datetime2 NOT NULL,
    [VisitEndDate] datetime2 NOT NULL,
    [ReaderTicket] int NULL,
    [AdditionalRequirements] nvarchar(max) NULL,
    [Email] nvarchar(100) NULL,
    [Phone] nvarchar(50) NULL,
    [FirstName] nvarchar(50) NULL,
    [LastName] nvarchar(50) NULL,
    [LastModifiedBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Bookings_BookingStatus_BookingStatusId] FOREIGN KEY ([BookingStatusId]) REFERENCES [BookingStatus] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_BookingType_BookingTypeId] FOREIGN KEY ([BookingTypeId]) REFERENCES [BookingType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Seats_SeatId] FOREIGN KEY ([SeatId]) REFERENCES [Seats] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderDocuments] (
    [Id] int NOT NULL IDENTITY,
    [DocumentReference] nvarchar(50) NOT NULL,
    [Description] nvarchar(255) NULL,
    [BookingId] int NOT NULL,
    [LetterCode] nvarchar(20) NULL,
    [ClassNumber] int NOT NULL,
    [PieceId] int NOT NULL,
    [PieceReference] nvarchar(20) NULL,
    [SubClassNumber] int NULL,
    [ItemReference] nvarchar(20) NULL,
    [Site] nvarchar(20) NULL,
    [IsReserve] bit NOT NULL,
    [Requisitioned] bit NULL,
    CONSTRAINT [PK_OrderDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderDocuments_Bookings_BookingId] FOREIGN KEY ([BookingId]) REFERENCES [Bookings] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Bookings_BookingStatusId] ON [Bookings] ([BookingStatusId]);
GO

CREATE INDEX [IX_Bookings_BookingTypeId] ON [Bookings] ([BookingTypeId]);
GO

CREATE INDEX [IX_Bookings_SeatId] ON [Bookings] ([SeatId]);
GO

CREATE INDEX [IX_OrderDocuments_BookingId] ON [OrderDocuments] ([BookingId]);
GO

CREATE INDEX [IX_Seats_SeatTypeId] ON [Seats] ([SeatTypeId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210402090035_InitialCreate', N'5.0.3');
GO

COMMIT;
GO