CREATE TABLE [dbo].[Properties]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryId] INT NOT NULL, 
    [Type] INT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Properties_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
)
