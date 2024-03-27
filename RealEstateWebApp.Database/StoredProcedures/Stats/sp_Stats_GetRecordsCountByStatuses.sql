CREATE PROCEDURE [dbo].[sp_Stats_GetRecordsCountByStatuses]
   @StartDate DATE
AS
BEGIN
    SELECT 
        RSH.Status,
        COUNT(*) AS RecordCount
    FROM 
        RecordStatusesHistory AS RSH
    WHERE 
        UpdatedAt >= @StartDate
    GROUP BY 
        RSH.Status;
END