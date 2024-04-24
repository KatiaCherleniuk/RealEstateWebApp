CREATE PROCEDURE [dbo].[sp_UserComments_GetUsersCommentByRecordId]
	@RecordId INT
AS
	SELECT US.Id,
		US.RecordId,
		US.PhoneNumber,
		US.Comment,
		US.CreatedAt,
		Users.UserName AS UserName
	FROM [dbo].UserComments AS US
	JOIN Users ON [dbo].Users.Id = US.UserId
	WHERE US.RecordId = @RecordId
RETURN 0