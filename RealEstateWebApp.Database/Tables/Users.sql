CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(250) NOT NULL,
    [NormalizedUserName] NVARCHAR(250) NOT NULL,
    [Email] NVARCHAR(250) NOT NULL,
    [NormalizedEmail] NVARCHAR(250) NOT NULL,
    [EmailConfirmed] BIT NOT NULL,
    [PasswordHash] NVARCHAR(MAX),
    [RoleId] INT NOT NULL, 
    [LockoutEnd] DATETIME, 
    [LockoutEnabled] BIT NOT NULL, 
    [AccessFailedCount] INT NOT NULL, 
    [IsBlocked] BIT NOT NULL, 
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles]([Id])
)
