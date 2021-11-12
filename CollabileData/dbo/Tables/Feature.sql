﻿CREATE TABLE [dbo].[Feature]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentEpic] INT NULL,
	[Release] INT NULL,
	[Priority] INT NOT NULL,
	[BusinessValue] NVARCHAR(20) NOT NULL,
	[Assignee] NVARCHAR(50) NOT NULL,
	CONSTRAINT FK_User_Feature_Assignee FOREIGN KEY (Assignee) REFERENCES [User] (Username),
	CONSTRAINT FK_BusinessValue_Feature FOREIGN KEY (BusinessValue) REFERENCES [BusinessValue] ([Name])
)