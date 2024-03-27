﻿CREATE PROCEDURE [sp_Records_GetRecordForViewSimplifiedByIdList]
    @IdList [dbo].[utIntTable] READONLY
AS
    SELECT R.Id,
           R.CategoryId,
           R.Price,
           R.Square,
           R.Address
    FROM @IdList AS I
    INNER JOIN Records AS R ON I.Id = R.Id
RETURN 0