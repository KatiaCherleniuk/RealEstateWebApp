CREATE PROCEDURE [sp_Categories_GetAllWithRecordsCount]
AS
    
    SELECT C.Id,
           C.Title,
           Count(R2.Id) AS RecordsCount
    FROM Categories AS C
    LEFT JOIN Records R2 on C.Id = R2.CategoryId
    GROUP BY C.Id, C.Title
    
RETURN 0