CREATE PROCEDURE [dbo].[sp_Stats_GetAveragePriceByCategory]
    @IntervalType NVARCHAR(50),
    @IntervalValue INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StartDate DATE;
    DECLARE @EndDate DATE = GETDATE(); -- Поточна дата

    -- Обчислити початкову дату відповідно до вказаного інтервалу
    IF @IntervalType = 'Week'
    BEGIN
        SET @StartDate = DATEADD(WEEK, -@IntervalValue, @EndDate);
    END
    ELSE IF @IntervalType = 'Month'
    BEGIN
        SET @StartDate = DATEADD(MONTH, -@IntervalValue, @EndDate);
    END
    ELSE
    BEGIN
        -- Якщо невірно вказаний тип інтервалу, повернути порожній набір даних
        SELECT 'Invalid Interval Type' AS Error;
        RETURN;
    END

    -- Тимчасова таблиця для збереження результатів
    CREATE TABLE #TempStatsAveragePrices (
        Title NVARCHAR(100),
        PeriodStart DATE,
        AveragePrice DECIMAL(18,2)
    );

    -- Запит для обчислення середньої ціни за категорією та періодом
    INSERT INTO #TempStatsAveragePrices (Title, PeriodStart, AveragePrice)
    SELECT 
        c.[Title],
        CASE 
            WHEN @IntervalType = 'Week' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, [CreatedAt]), 0)
            WHEN @IntervalType = 'Month' THEN DATEFROMPARTS(YEAR([CreatedAt]), MONTH([CreatedAt]), 1)
        END AS Period,
        AVG(r.[Price]) AS AveragePrice
    FROM 
        [Records] r
    INNER JOIN 
        [Categories] c ON r.[CategoryId] = c.[Id]
    WHERE 
        r.[CreatedAt] BETWEEN @StartDate AND @EndDate
    GROUP BY 
        c.[Title], 
        CASE 
            WHEN @IntervalType = 'Week' THEN DATEADD(WEEK, DATEDIFF(WEEK, 0, [CreatedAt]), 0)
            WHEN @IntervalType = 'Month' THEN DATEFROMPARTS(YEAR([CreatedAt]), MONTH([CreatedAt]), 1)
        END;

    -- Повернення результату у вигляді списку
    SELECT 
        Title,
        CAST(PeriodStart AS DATETIME) AS PeriodStart,
        AveragePrice
    FROM 
        #TempStatsAveragePrices
    ORDER BY 
        Title, 
        PeriodStart;

    -- Видалення тимчасової таблиці
    DROP TABLE #TempStatsAveragePrices;
END;
