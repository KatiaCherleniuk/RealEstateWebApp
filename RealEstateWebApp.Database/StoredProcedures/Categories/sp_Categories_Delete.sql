CREATE PROCEDURE [sp_Categories_Delete]
    @Id INT
AS
    DELETE FROM [Categories] WHERE Id = @Id;
RETURN 0