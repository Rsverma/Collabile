CREATE TABLE [dbo].[ProjectShareholder]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Project] NVARCHAR(20) NOT NULL,
	[Shareholder] VARCHAR(36) NOT NULL,
	CONSTRAINT FK_Project_ProjectShareholder FOREIGN KEY ([Project]) REFERENCES [Project] ([Key]),
	CONSTRAINT FK_User_ProjectShareholder FOREIGN KEY ([Shareholder]) REFERENCES [CollabileUser] (Id)
)
