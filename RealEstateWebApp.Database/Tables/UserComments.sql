CREATE TABLE [dbo].[UserComments]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [RecordId] INT NOT NULL,
    [PhoneNumber] NVARCHAR(50) NOT NULL, 
    [Comment] NVARCHAR(MAX) NULL, 
    [CreatedAt] DATE NOT NULL, 
    CONSTRAINT [FK_UserComments_Users] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_UserComments_Records] FOREIGN KEY ([RecordId]) REFERENCES [Records]([Id])
)
