CREATE PROCEDURE [dbo].[fn_Places_RND]
AS
BEGIN
    DECLARE @RET AS INT = 0

    SELECT TOP 1 @RET = Code From dbo.Places ORDER BY newid()

    RETURN @RET
END