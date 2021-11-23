CREATE TABLE [dbo].[Team]
(
    [Name] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [Description] NVARCHAR(500) NOT NULL,
	[Owner] VARCHAR(36) NOT NULL,
	CONSTRAINT FK_User_Team_Owner FOREIGN KEY ([Owner]) REFERENCES [CollabileUser] (Id)
)
