CREATE TABLE [dbo].[ProjectShareholder]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Project] NVARCHAR(20) NOT NULL,
	[Shareholder] INT NOT NULL,
	CONSTRAINT FK_Project_ProjectShareholder FOREIGN KEY ([Project]) REFERENCES [Project] ([Key]),
	CONSTRAINT FK_User_ProjectShareholder FOREIGN KEY ([Shareholder]) REFERENCES [User] (ID)
)
