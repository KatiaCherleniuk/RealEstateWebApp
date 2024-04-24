CREATE PROCEDURE [dbo].[sp_UserComments_Insert]
	@Id INT OUTPUT,
	@UserId INT OUTPUT,
    @RecordId INT,
    @PhoneNumber NVARCHAR(50),
    @Comment NVARCHAR(MAX),
    @UserName NVARCHAR(50),
    @CreatedAt DATE
AS
    INSERT INTO UserComments(UserId, RecordId, PhoneNumber, Comment, CreatedAt) 
    VALUES (@UserId, @RecordId, @PhoneNumber, @Comment, @CreatedAt);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0