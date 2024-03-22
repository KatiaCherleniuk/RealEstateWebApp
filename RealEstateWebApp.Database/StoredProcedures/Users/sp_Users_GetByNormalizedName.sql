CREATE PROCEDURE [sp_Users_GetByNormalizedName]
    @NormalizedName NVARCHAR(250)
AS
    SELECT U.Id,
           U.UserName,
           U.NormalizedUserName,
           U.Email,
           U.NormalizedEmail,
           U.EmailConfirmed,
           U.PasswordHash,
           U.RoleId,
           U.LockoutEnabled,
           U.LockoutEnd,
           U.AccessFailedCount,
           U.IsBlocked
    FROM Users AS U
    WHERE U.NormalizedUserName = @NormalizedName;
RETURN 0