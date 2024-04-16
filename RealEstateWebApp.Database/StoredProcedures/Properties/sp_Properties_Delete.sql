CREATE PROCEDURE [sp_Properties_Delete]
    @Id INT
AS
    DELETE FROM Properties WHERE Id = @Id
    
RETURN 0