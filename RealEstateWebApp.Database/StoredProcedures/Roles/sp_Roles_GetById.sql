CREATE PROCEDURE [sp_Roles_GetById]
    @Id INT
AS
    SELECT R.Id,
           R.Name,
           R.NormalizedName
    FROM Roles AS R
    WHERE R.Id = @Id;
RETURN 0