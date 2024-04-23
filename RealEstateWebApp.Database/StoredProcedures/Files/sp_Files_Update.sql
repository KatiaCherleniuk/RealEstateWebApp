CREATE PROCEDURE [sp_Files_Update]
    @Id INT,
    @IsDeleted BIT,
    @IsMain BIT
AS
    UPDATE Files SET 
        [IsDeleted] = @IsDeleted,
        [IsMain] = @IsMain
        WHERE Id = @Id;
    
RETURN 0