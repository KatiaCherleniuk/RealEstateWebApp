CREATE PROCEDURE [dbo].[sp_Users_GetAllUsersForEdit]
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
           U.IsBlocked,
           U.CreatedAt,
           R.Id AS RoleId, 
           R.Name AS RoleName
    FROM Users AS U
    INNER JOIN Roles R on R.Id = U.RoleId;
RETURN 0