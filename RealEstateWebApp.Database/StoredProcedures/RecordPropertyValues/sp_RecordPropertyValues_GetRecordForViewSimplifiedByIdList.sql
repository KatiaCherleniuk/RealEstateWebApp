CREATE PROCEDURE [sp_RecordPropertyValues_GetRecordForViewSimplifiedByIdList]
    @RecordIdList [dbo].[utIntTable] READONLY
AS
    
    SELECT
       RPV.RecordId,
       RPV.PropertyId, 
       RPV.ValueNumber,
       RPV.ValueString,
       RPV.ValueId,
       RPV.ValueList
    FROM RecordPropertyValues AS RPV
    JOIN Records AS R ON RecordId = R.Id
    WHERE RPV.RecordId IN (SELECT Id FROM @RecordIdList)
    
RETURN 0