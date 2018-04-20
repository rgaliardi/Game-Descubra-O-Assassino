CREATE PROCEDURE [dbo].[sp_Random]
AS
BEGIN
    DECLARE @RET_Suspects   AS INT = 0
    DECLARE @RET_Places     AS INT = 0
    DECLARE @RET_Weapons    AS INT = 0

    SELECT TOP 1 @RET_Suspects  = Code From dbo.Suspects    ORDER BY newid()
    SELECT TOP 1 @RET_Places    = Code From dbo.Places      ORDER BY newid()
    SELECT TOP 1 @RET_Weapons   = Code From dbo.Weapons     ORDER BY newid()

    SELECT @RET_Suspects AS Suspects, @RET_Places as Places, @RET_Weapons as Weapons
END