IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Admins] (
        [AdminID] int NOT NULL IDENTITY,
        [AdminName] nvarchar(max) NULL,
        [AdminUsername] nvarchar(max) NULL,
        [AdminPassword] nvarchar(max) NULL,
        CONSTRAINT [PK_Admins] PRIMARY KEY ([AdminID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Categories] (
        [CategoryID] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Users] (
        [UserID] int NOT NULL IDENTITY,
        [UserName] nvarchar(max) NOT NULL,
        [UserNickName] nvarchar(max) NOT NULL,
        [UserSurname] nvarchar(max) NOT NULL,
        [UserEmail] nvarchar(max) NOT NULL,
        [UserPassword] nvarchar(max) NOT NULL,
        [isAdmin] bit NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserID])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Posts] (
        [PostID] int NOT NULL IDENTITY,
        [PostTitle] nvarchar(max) NOT NULL,
        [PostThumbnailUrl] nvarchar(max) NOT NULL,
        [PostContent] nvarchar(60) NOT NULL,
        [PostLikeCount] int NOT NULL,
        [PostCommentCount] int NOT NULL,
        [UserID] int NULL,
        [CategoryID] int NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([PostID]),
        CONSTRAINT [FK_Posts_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Posts_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Comments] (
        [CommentID] int NOT NULL IDENTITY,
        [CommentContent] nvarchar(max) NULL,
        [PostID] int NULL,
        [UserID] int NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentID]),
        CONSTRAINT [FK_Comments_Posts_PostID] FOREIGN KEY ([PostID]) REFERENCES [Posts] ([PostID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Comments_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE TABLE [Likes] (
        [LikeID] int NOT NULL IDENTITY,
        [UserID] int NULL,
        [PostID] int NULL,
        CONSTRAINT [PK_Likes] PRIMARY KEY ([LikeID]),
        CONSTRAINT [FK_Likes_Posts_PostID] FOREIGN KEY ([PostID]) REFERENCES [Posts] ([PostID]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Likes_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UserID]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Comments_PostID] ON [Comments] ([PostID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Comments_UserID] ON [Comments] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Likes_PostID] ON [Likes] ([PostID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Likes_UserID] ON [Likes] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Posts_CategoryID] ON [Posts] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    CREATE INDEX [IX_Posts_UserID] ON [Posts] ([UserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191221171032_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191221171032_InitialCreate', N'3.0.1');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    ALTER TABLE [Posts] DROP CONSTRAINT [FK_Posts_Categories_CategoryID];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    DROP INDEX [IX_Posts_CategoryID] ON [Posts];
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Posts]') AND [c].[name] = N'CategoryID');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Posts] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Posts] ALTER COLUMN [CategoryID] int NOT NULL;
    CREATE INDEX [IX_Posts_CategoryID] ON [Posts] ([CategoryID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    ALTER TABLE [Admins] ADD [UsersUserID] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    CREATE INDEX [IX_Admins_UsersUserID] ON [Admins] ([UsersUserID]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    ALTER TABLE [Admins] ADD CONSTRAINT [FK_Admins_Users_UsersUserID] FOREIGN KEY ([UsersUserID]) REFERENCES [Users] ([UserID]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    ALTER TABLE [Posts] ADD CONSTRAINT [FK_Posts_Categories_CategoryID] FOREIGN KEY ([CategoryID]) REFERENCES [Categories] ([CategoryID]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20191222123011_InitialCreate2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20191222123011_InitialCreate2', N'3.0.1');
END;

GO

