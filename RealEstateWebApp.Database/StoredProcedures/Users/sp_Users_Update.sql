CREATE PROCEDURE [sp_Users_Update]
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
    UPDATE Users SET
        [UserName] = @UserName,
        [NormalizedUserName] = @NormalizedUserName,
        [Email] = @Email,
        [NormalizedEmail] = @NormalizedEmail,
        [EmailConfirmed] = @EmailConfirmed,
        [PasswordHash] = @PasswordHash,
        [RoleId] = @RoleId,
        [LockoutEnabled] = @LockoutEnabled,
        [LockoutEnd] = @LockoutEnd,
        [AccessFailedCount] = @AccessFailedCount,
        [IsBlocked] = @IsBlocked
    WHERE Id = @Id;
RETURN 0