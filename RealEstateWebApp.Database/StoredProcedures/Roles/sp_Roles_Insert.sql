CREATE PROCEDURE [sp_Roles_Insert]
    @Id INT OUTPUT,
    @Name NVARCHAR(250),
    @NormalizedName NVARCHAR(250)
AS
    INSERT INTO Roles (Name, NormalizedName) 
    VALUES (@Name, @NormalizedName);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0