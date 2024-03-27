CREATE PROCEDURE [dbo].[sp_Stats_GetRecordsByCategories]
	@StartDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.[Title],
        COUNT(r.[Id]) AS RecordCount
    FROM 
        [Records] r
    INNER JOIN 
        [Categories] c ON r.[CategoryId] = c.[Id]
    WHERE 
        r.[CreatedAt] > @StartDate
    GROUP BY 
        c.[Title];
END;