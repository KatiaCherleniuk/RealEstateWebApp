CREATE PROCEDURE [sp_Files_GetByRecordId]
    @RecordId INT
AS
   SELECT 
          F.Id,
          F.Name as FileName,
          F.CreatedAt ,
          F.IsDeleted,
          U.UserName AS CreatorName,
          F.IsMain
   FROM [Files] as F 
   INNER JOIN Users U ON U.Id = F.CreatedBy
   WHERE F.[RecordId] = @RecordId
RETURN 0