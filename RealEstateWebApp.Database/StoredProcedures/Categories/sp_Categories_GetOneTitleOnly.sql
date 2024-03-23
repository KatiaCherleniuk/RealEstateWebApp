CREATE PROCEDURE [sp_Categories_GetOneTitleOnly]
    @Id INT
AS
    
    SELECT C.Id,
           C.Title
    FROM Categories AS C
    WHERE C.Id = @Id
    
RETURN 0