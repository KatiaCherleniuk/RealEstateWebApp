CREATE TABLE [dbo].[RecordPropertyValues]
(
    [RecordId] INT NOT NULL, 
    [PropertyId] INT NOT NULL,
    [ValueNumber] DECIMAL NULL,
    [ValueString] NVARCHAR(MAX) NULL, 
    [ValueId] INT NULL, 
    [ValueList] NVARCHAR(MAX) NULL, 
    CONSTRAINT [FK_RecordPropertyValues_Records] FOREIGN KEY ([RecordId]) REFERENCES [Records]([Id]), 
    CONSTRAINT [FK_RecordPropertyValues_Properties] FOREIGN KEY ([PropertyId]) REFERENCES [Properties]([Id]), 
    CONSTRAINT [FK_RecordPropertyValues_PropertyValues] FOREIGN KEY ([ValueId]) REFERENCES [PropertyValues]([Id]), 
    CONSTRAINT [PK_RecordPropertyValues] PRIMARY KEY ([RecordId], [PropertyId])
)
