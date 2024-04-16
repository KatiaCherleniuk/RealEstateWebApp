CREATE PROCEDURE [sp_Properties_GetAllWithValuesCount]
    @CategoryId INT
AS
    
    SELECT P.Id,
           P.CategoryId,
           P.Title,
           P.Type,
           Count(RPV.RecordId) AS RecordsCount
    FROM Properties AS P
    LEFT JOIN RecordPropertyValues RPV on P.Id = RPV.PropertyId
    WHERE P.CategoryId = @CategoryId
    GROUP BY P.Id, P.CategoryId, P.Title, P.Type
    
RETURN 0