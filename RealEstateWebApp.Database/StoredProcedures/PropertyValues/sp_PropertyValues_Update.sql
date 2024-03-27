CREATE PROCEDURE [sp_PropertyValues_Update]
    @Id INT,
    @PropertyId INT,
    @Title NVARCHAR(50)
AS
    UPDATE [PropertyValues]
    SET Title = @Title,
        PropertyId = @PropertyId
    WHERE Id = @Id;
    
RETURN 0