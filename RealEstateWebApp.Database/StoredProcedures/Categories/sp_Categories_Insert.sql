CREATE PROCEDURE [sp_Categories_Insert]
	@Title NVARCHAR(50),
	@Id INT OUTPUT
AS
    INSERT INTO [Categories] (Title)
    VALUES (@Title);

    SELECT @Id = SCOPE_IDENTITY();
    
RETURN 0