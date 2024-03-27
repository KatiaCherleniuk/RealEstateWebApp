CREATE PROCEDURE [dbo].[sp_Stats_GetUsersRegisteredAfterDate]
    @IntervalType NVARCHAR(50),
    @IntervalValue INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATE;

    IF @IntervalType = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, -@IntervalValue, GETDATE());
    END
    ELSE IF @IntervalType = 'Month'
    BEGIN
        SET @StartDate = DATEADD(MONTH, -@IntervalValue, GETDATE());
    END
    ELSE
    BEGIN
        SELECT NULL AS RegistrationDate, 0 AS UserCount;
        RETURN;
    END

    SELECT 
        CASE 
            WHEN @IntervalType = 'Week' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, [CreatedAt]), 0)
            WHEN @IntervalType = 'Month' THEN DATEFROMPARTS(YEAR([CreatedAt]), MONTH([CreatedAt]), 1)
        END AS RegistrationDate,
        COUNT(*) AS UserCount
    FROM 
        [Users]
    WHERE 
        [RoleId] = (SELECT [Id] FROM [Roles] WHERE [Name] = 'User')
        AND [CreatedAt] > @StartDate
    GROUP BY 
        CASE 
            WHEN @IntervalType = 'Week' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, [CreatedAt]), 0)
            WHEN @IntervalType = 'Month' THEN DATEFROMPARTS(YEAR([CreatedAt]), MONTH([CreatedAt]), 1)
        END
    ORDER BY 
        RegistrationDate ASC;
END;
