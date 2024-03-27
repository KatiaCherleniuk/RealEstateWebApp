CREATE PROCEDURE [sp_Records_Update]
    @Id INT,
    @CategoryId INT,
    @Price FLOAT,
    @Square FLOAT,
    @Address NVARCHAR(255),
    @CreatedAt DATE
AS
    UPDATE Records SET
       CategoryId = @CategoryId,
       Price = @Price,
       Square = @Square,
       Address = @Address,
       CreatedAt = @CreatedAt
    WHERE Id = @Id;
RETURN 0