CREATE PROCEDURE [sp_PropertyValues_GetAllTitleOnly]
    @PropertyId INT
AS
    
    SELECT PV.Id,
           PV.Title
    FROM PropertyValues AS PV
    WHERE PV.PropertyId = @PropertyId
    
RETURN 0