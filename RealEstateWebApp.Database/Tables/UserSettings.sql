CREATE TABLE [dbo].[UserSettings]
(
    [UserId] INT NOT NULL,
    [Key] NVARCHAR(250),
    [Value] NVARCHAR(MAX)
    CONSTRAINT [FK_UserSettings_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id])
    CONSTRAINT [PK_UserSettings] PRIMARY KEY ([UserId], [Key])
)
