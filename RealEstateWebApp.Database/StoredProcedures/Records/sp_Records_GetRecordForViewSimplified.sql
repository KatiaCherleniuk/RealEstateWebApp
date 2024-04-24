CREATE PROCEDURE [dbo].[sp_Records_GetRecordForViewSimplified]
	@RecordId INT
AS
BEGIN
    SELECT R.Id,
           R.CategoryId,
           R.Price,
           R.Square,
           R.Address AS AddressJson,
           R.CreatedAt,
           R.Type
    FROM Records AS R
    WHERE R.Id = @RecordId
END