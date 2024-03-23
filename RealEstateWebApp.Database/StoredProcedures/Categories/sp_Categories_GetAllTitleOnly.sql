CREATE PROCEDURE [sp_Categories_GetAllTitleOnly]
AS
    
    SELECT C.Id,
           C.Title
    FROM Categories AS C
    
RETURN 0