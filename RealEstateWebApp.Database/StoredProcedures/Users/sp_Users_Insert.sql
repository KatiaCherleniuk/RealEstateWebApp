CREATE PROCEDURE [sp_Users_Insert]
    @Id INT OUTPUT,
    @UserName NVARCHAR(250),
    @NormalizedUserName NVARCHAR(250),
    @Email NVARCHAR(250),
    @NormalizedEmail NVARCHAR(250),
    @EmailConfirmed BIT,
    @PasswordHash NVARCHAR(MAX),
    @RoleId INT,
    @LockoutEnabled BIT,
    @LockoutEnd DATETIME,
    @AccessFailedCount INT,
    @IsBlocked BIT
AS
    INSERT INTO Users (UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, RoleId, AccessFailedCount, LockoutEnd, LockoutEnabled, IsBlocked) 
    VALUES (@UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @RoleId, @AccessFailedCount, @LockoutEnd, @LockoutEnabled, @IsBlocked);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0