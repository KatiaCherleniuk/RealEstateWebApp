CREATE PROCEDURE [sp_Categories_Update]
    @Id INT,
	@Title NVARCHAR(50)
AS
    UPDATE [Categories]
    SET Title = @Title
    WHERE Id = @Id;
    