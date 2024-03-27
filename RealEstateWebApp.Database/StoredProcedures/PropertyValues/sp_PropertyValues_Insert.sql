CREATE PROCEDURE [sp_PropertyValues_Insert]
    @Id INT OUTPUT,
    @PropertyId INT,
    @Title NVARCHAR(50)
AS
    INSERT INTO PropertyValues (PropertyId, Title) 
    VALUES (@PropertyId, @Title);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0