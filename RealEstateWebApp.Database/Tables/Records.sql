﻿CREATE TABLE [dbo].[Records]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CategoryId] INT NOT NULL, 
    [Price] FLOAT NULL,
    [Square] FLOAT NULL,
    [Address] NVARCHAR(255) NULL,
    [CreatedAt] DATE NOT NULL DEFAULT GETDATE(),
    [Type] INT NOT NULL DEFAULT 0
    CONSTRAINT [FK_Records_Categories] FOREIGN KEY ([CategoryId]) REFERENCES [Categories]([Id])
)
