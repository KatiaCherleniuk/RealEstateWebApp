CREATE PROCEDURE [sp_Properties_Update]
    @Id INT,
    @CategoryId INT,
    @Type INT,
    @Title NVARCHAR(50)
AS
    UPDATE [Properties]
    SET Title = @Title,
        CategoryId = @CategoryId,
        Type = @Type
    WHERE Id = @Id;
    
RETURN 0