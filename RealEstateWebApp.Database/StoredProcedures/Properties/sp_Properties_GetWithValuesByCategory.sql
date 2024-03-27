CREATE PROCEDURE [sp_Properties_GetWithValuesByCategory]
    @CategoryId INT
AS
    
    SELECT P.Id,
           P.Title,
           P.Type,
           PV.Id AS ValueId,
           PV.Title AS ValueTitle
    FROM Properties AS P
    LEFT JOIN PropertyValues PV on P.Id = PV.PropertyId
    WHERE P.CategoryId = @CategoryId    
    
RETURN 0