CREATE TABLE [dbo].[PropertyValues]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PropertyId] INT NOT NULL, 
    [Title] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_PropertyValues_Properties] FOREIGN KEY ([PropertyId]) REFERENCES [Properties]([Id])
)
