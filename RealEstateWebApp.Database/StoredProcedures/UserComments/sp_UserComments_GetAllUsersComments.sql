CREATE PROCEDURE [dbo].[sp_UserComments_GetAllUsersComments]
AS
	SELECT US.Id,
		US.RecordId,
		US.PhoneNumber,
		US.Comment,
		US.CreatedAt,
		Users.UserName AS UserName
	FROM [dbo].UserComments AS US
	JOIN Users ON [dbo].Users.Id = US.UserId
RETURN 0
