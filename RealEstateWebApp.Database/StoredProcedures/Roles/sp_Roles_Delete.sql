CREATE PROCEDURE [sp_Roles_Delete]
    @Id INT
AS
    DELETE FROM [Roles] WHERE Id = @Id;
RETURN 0