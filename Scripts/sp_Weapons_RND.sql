CREATE PROCEDURE [dbo].[sp_Weapons_RND]
AS
BEGIN
    DECLARE @RET AS INT = 0

    SELECT TOP 1 @RET = Code From dbo.Weapons ORDER BY newid()

    RETURN @RET
END