CREATE PROCEDURE [dbo].[sp_UserComments_Delete]
    @Id INT
AS
    DELETE FROM UserComments
    WHERE [Id] = @Id;

RETURN 0