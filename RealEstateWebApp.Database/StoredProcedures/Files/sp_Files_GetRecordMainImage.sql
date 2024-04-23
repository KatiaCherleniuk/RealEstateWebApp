CREATE PROCEDURE [dbo].[sp_Files_GetRecordMainImage]
    @RecordId INT
AS
   SELECT  F.FilePath
   FROM [Files] as F 
   WHERE F.[RecordId] = @RecordId AND F.IsMain = 1
RETURN 0
