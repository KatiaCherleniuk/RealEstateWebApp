CREATE PROCEDURE [sp_Files_Insert]
    @Id INT OUTPUT,
    @RecordId INT,
    @FilePath NVARCHAR(MAX),
    @FileName NVARCHAR(250),
    @ContentType NVARCHAR(250),
    @IsDeleted BIT,
    @CreatedAt DATETIME,
    @CreatedBy INT,
    @IsMain BIT
AS
    INSERT INTO Files(RecordId, FilePath, Name, IsDeleted,CreatedAt, CreatedBy, ContentType, IsMain) 
    VALUES (@RecordId, @FilePath, @FileName, 0, @CreatedAt, @CreatedBy, @ContentType, @IsMain);

    SELECT @Id = SCOPE_IDENTITY();
RETURN 0