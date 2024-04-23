CREATE TABLE [dbo].[Files]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RecordId] INT NOT NULL,
    [FilePath] NVARCHAR(MAX) NOT NULL,
    [Name] NVARCHAR(250) NOT NULL,
    [IsDeleted] BIT NOT NULL ,
    [ContentType] NVARCHAR(250),
    [CreatedAt] DATETIME,
    [CreatedBy] INT NOT NULL,
    [IsMain] BIT NOT NULL DEFAULT 0
    CONSTRAINT [FK_Files_Records] FOREIGN KEY ([RecordId]) REFERENCES [Records]([Id]),
    CONSTRAINT [FK_Files_Users] FOREIGN KEY ([CreatedBy]) REFERENCES [Users]([Id])
)
