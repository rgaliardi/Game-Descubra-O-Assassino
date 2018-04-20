CREATE TABLE [dbo].[Suspects] (
    [Code]          INT        NOT NULL IDENTITY,
    [Name]          NCHAR (25) NOT NULL,
    [Description]   NVARCHAR (250) NULL,
    [Enabled]       BIT        DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Code] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Suspects_Name]
    ON [dbo].[Suspects]([Name] ASC);

