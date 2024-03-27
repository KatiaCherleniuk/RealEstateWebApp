CREATE PROCEDURE [sp_Files_GetByIdAndRecordId]
    @Id INT,
    @RecordId INT
AS
   SELECT  F.FilePath
   FROM [Files] as F 
   WHERE F.[RecordId] = @RecordId AND F.Id = @Id
RETURN 0