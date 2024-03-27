CREATE PROCEDURE [sp_Files_Update]
    @Id INT,
    @IsDeleted BIT
AS
    UPDATE Files SET 
        [IsDeleted] = @IsDeleted
        WHERE Id = @Id;
    
RETURN 0