CREATE PROCEDURE [sp_RecordPropertyValues_GetRecordForViewSimplifiedByIdList]
    @RecordIdList [dbo].[utIntTable] READONLY
AS
    
    SELECT
       RPV.RecordId,
       RPV.PropertyId, 
       RPV.ValueNumber,
       RPV.ValueString,
       RPV.ValueId,
       RPV.ValueList,
       RPV.ValueAddress
    FROM RecordPropertyValues AS RPV
    WHERE RPV.RecordId IN (SELECT Id FROM @RecordIdList)
    
RETURN 0