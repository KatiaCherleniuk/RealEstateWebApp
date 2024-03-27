CREATE PROCEDURE [dbo].[sp_Records_Delete]
    @Id INT
AS
    DELETE FROM RecordPropertyValues
    WHERE [RecordId] = @Id;
    
    DELETE FROM Files
    WHERE [RecordId] = @Id;

    DELETE FROM RecordStatusesHistory
    WHERE [RecordId] = @Id;

    DELETE FROM Records
    WHERE [Id] = @Id;

RETURN 0