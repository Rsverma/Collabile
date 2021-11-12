﻿CREATE TABLE [dbo].[WorkLog]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Task] INT NOT NULL,
	[User] NVARCHAR(50) NOT NULL,
	[Effort] DECIMAL NOT NULL,
	[StartDate] DATETIME2 NOT NULL,
	[CreatedAt] DATETIME2 NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_Task_WorkLog FOREIGN KEY ([Task]) REFERENCES [Task] ([Id]),
	CONSTRAINT FK_User_WorkLog FOREIGN KEY ([User]) REFERENCES [User] (Username)
)