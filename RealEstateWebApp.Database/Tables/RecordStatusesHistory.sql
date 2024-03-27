CREATE TABLE [dbo].[RecordStatusesHistory]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[RecordId] INT NOT NULL,
	[Status] INT NOT NULL,
    [UpdatedAt] DATE NOT NULL,
    CONSTRAINT [FK_RecordStatusesHistory_Records] FOREIGN KEY ([RecordId]) REFERENCES [Records]([Id]) 
)
