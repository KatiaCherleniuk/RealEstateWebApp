CREATE PROCEDURE [sp_Users_Delete]
    @Id INT
AS
    DELETE FROM [Users] WHERE Id = @Id;
RETURN 0