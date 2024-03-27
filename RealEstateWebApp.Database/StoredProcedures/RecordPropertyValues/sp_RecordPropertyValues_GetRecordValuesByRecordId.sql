CREATE PROCEDURE [sp_RecordPropertyValues_GetRecordValuesByRecordId]
    @RecordId INT
AS
    
    SELECT
       RPV.RecordId,
       RPV.PropertyId, 
       RPV.ValueNumber,
       RPV.ValueString,
       RPV.ValueId,
       RPV.ValueList
    FROM RecordPropertyValues AS RPV 
    WHERE RPV.RecordId = @RecordId
    
RETURN 0