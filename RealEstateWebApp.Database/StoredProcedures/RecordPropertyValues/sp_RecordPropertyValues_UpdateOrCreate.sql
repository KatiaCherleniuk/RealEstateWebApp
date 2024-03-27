CREATE PROCEDURE [sp_RecordPropertyValues_UpdateOrCreate]
    @RecordId INT,
    @PropertyId INT,
    @ValueNumber DECIMAL,
    @ValueString NVARCHAR(MAX),
    @ValueId INT,
    @ValueList NVARCHAR(MAX)
AS
    IF EXISTS (SELECT 1 FROM RecordPropertyValues WHERE RecordId = @RecordId AND PropertyId = @PropertyId)
    BEGIN
        UPDATE RecordPropertyValues SET
            ValueNumber = @ValueNumber,
            ValueString = @ValueString,
            ValueId     = @ValueId,
            ValueList   = @ValueList
        WHERE RecordId = @RecordId AND PropertyId = @PropertyId
    END
    ELSE
    BEGIN
        INSERT INTO RecordPropertyValues (RecordId, PropertyId, ValueNumber, ValueString, ValueId, ValueList)
        VALUES (@RecordId, @PropertyId, @ValueNumber, @ValueString, @ValueId, @ValueList);
    END
RETURN 0