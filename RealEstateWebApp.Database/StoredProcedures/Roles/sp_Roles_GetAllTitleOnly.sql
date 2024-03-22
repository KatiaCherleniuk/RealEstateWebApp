CREATE PROCEDURE [sp_Roles_GetAllTitleOnly]
AS
    SELECT R.Id,
           R.Name AS Title
    FROM Roles AS R;
RETURN 0