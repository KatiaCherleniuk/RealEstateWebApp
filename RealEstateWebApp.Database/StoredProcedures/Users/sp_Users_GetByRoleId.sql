CREATE PROCEDURE [sp_Users_GetByRoleId]
    @RoleId INT
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
    WHERE U.RoleId = @RoleId;
RETURN 0