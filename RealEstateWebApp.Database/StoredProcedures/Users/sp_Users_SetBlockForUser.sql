CREATE PROCEDURE [sp_Users_SetBlockForUser]
    @UserId INT OUTPUT,
    @IsBlocked BIT
AS
    UPDATE Users SET
        [IsBlocked] = @IsBlocked
    WHERE Id = @UserId;
RETURN 0