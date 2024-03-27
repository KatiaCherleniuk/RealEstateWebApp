CREATE PROCEDURE [sp_Records_Insert]
    @Id INT OUTPUT,
    @CategoryId INT,
    @Price FLOAT,
    @Square FLOAT,
    @Address NVARCHAR(255),
    @CreatedAt DATE
AS
    INSERT INTO Records (CategoryId, Price, Square, Address, CreatedAt) 
    VALUES (@CategoryId, @Price, @Square, @Address, @CreatedAt);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0