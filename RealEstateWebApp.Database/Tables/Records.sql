CREATE TABLE [dbo].[Records]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryId] INT NOT NULL, 
    CONSTRAINT [FK_Records_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
)
