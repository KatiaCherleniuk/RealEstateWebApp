CREATE PROCEDURE [sp_Roles_Update]
    @Id INT,
    @Name NVARCHAR(250),
    @NormalizedName NVARCHAR(250)
AS
    UPDATE Roles SET
        [Name] = @Name,
        NormalizedName = @NormalizedName
    WHERE Id = @Id;
RETURN 0