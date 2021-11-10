CREATE TABLE [dbo].[User]
(
    [Username] NVARCHAR(50) NOT NULL PRIMARY KEY, 
    [Password] NVARCHAR(256) NOT NULL,
    [UserRole] NVARCHAR(10) DEFAULT 'User',
    [CreatedDate] DATETIME2 NOT NULL default getutcdate()
)
