CREATE PROCEDURE [sp_Records_GetById]
    @Id INT
AS
    SELECT R.Id,
           R.CategoryId,
           R.Price,
           R.Square,
           R.Address,
           R.CreatedAt
    FROM Records AS R
    WHERE R.Id = @Id;
RETURN 0