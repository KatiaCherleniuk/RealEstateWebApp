CREATE PROCEDURE [sp_Records_Insert]
    @Id INT OUTPUT,
    @CategoryId INT,
    @Price FLOAT,
    @Square FLOAT,
    @Address NVARCHAR(255),
    @CreatedAt DATE,
    @Type INT
AS
    INSERT INTO Records (CategoryId, Price, Square, Address, CreatedAt, Type) 
    VALUES (@CategoryId, @Price, @Square, @Address, @CreatedAt, @Type);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0