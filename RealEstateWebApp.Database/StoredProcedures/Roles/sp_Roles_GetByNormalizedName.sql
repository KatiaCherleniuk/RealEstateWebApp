CREATE PROCEDURE [sp_Roles_GetByNormalizedName]
    @NormalizedName NVARCHAR(250)
AS
    SELECT R.Id,
           R.Name,
           R.NormalizedName
    FROM Roles AS R
    WHERE R.NormalizedName = @NormalizedName;
RETURN 0