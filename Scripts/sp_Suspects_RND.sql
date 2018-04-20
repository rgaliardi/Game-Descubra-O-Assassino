CREATE PROCEDURE [dbo].[sp_Suspects_RND]
AS
BEGIN
    DECLARE @RET AS INT = 0

    SELECT TOP 1 @RET = Code From dbo.Suspects ORDER BY newid()

    RETURN @RET
END