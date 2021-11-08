CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [UserName] NVARCHAR(50) NOT NULL UNIQUE, 
    [Password] NVARCHAR(128) NOT NULL,
    [UserRole] INT NOT NULL DEFAULT 0,
    [CreatedDate] DATETIME2 NOT NULL default getutcdate()
)
