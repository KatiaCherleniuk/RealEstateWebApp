CREATE PROCEDURE [sp_Records_GetRecordForViewSimplifiedByIdList]
    @IdList [dbo].[utIntTable] READONLY
AS
BEGIN
    SELECT R.Id,
           R.CategoryId,
           R.Price,
           R.Square,
           R.Address AS AddressJson,
           R.CreatedAt
    FROM @IdList AS I
    INNER JOIN Records AS R ON I.Id = R.Id
END