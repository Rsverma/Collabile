CREATE TABLE [dbo].[Project]
(
	[Key] NVARCHAR(20) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(50) NOT NULL UNIQUE,
	[IsPublic] BIT NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[StartDate] DATETIME2 NOT NULL,
	[SprintDays] INT NOT NULL,
	[Owner] VARCHAR(36) NOT NULL,
	CONSTRAINT FK_User_Project_Owner FOREIGN KEY ([Owner]) REFERENCES [CollabileUser] (Id)

)
