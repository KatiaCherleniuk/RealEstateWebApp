CREATE PROCEDURE [sp_Records_Update]
    @Id INT,
    @CategoryId INT,
    @Price FLOAT,
    @Square FLOAT,
    @Address NVARCHAR(255),
    @CreatedAt DATE,
    @Type INT
AS
    UPDATE Records SET
       CategoryId = @CategoryId,
       Price = @Price,
       Square = @Square,
       Address = @Address,
       CreatedAt = @CreatedAt,
       Type = @Type
    WHERE Id = @Id;
RETURN 0