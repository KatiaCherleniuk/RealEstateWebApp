CREATE PROCEDURE [sp_Properties_Insert]
    @Id INT OUTPUT,
    @CategoryId INT,
    @Type INT,
    @Title NVARCHAR(50)
AS
    INSERT INTO Properties (CategoryId, Type, Title) 
    VALUES (@CategoryId, @Type, @Title);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0