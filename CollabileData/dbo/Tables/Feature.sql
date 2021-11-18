CREATE TABLE [dbo].[Feature]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentEpic] UNIQUEIDENTIFIER NULL,
	[Release] INT NULL,
	[Priority] INT NOT NULL,
	[BusinessValue] NVARCHAR(20) NOT NULL,
	[Assignee] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT FK_Epic_Feature FOREIGN KEY (ParentEpic) REFERENCES [Epic] (Id),
	CONSTRAINT FK_User_Feature_Assignee FOREIGN KEY (Assignee) REFERENCES [CollabileUser] (Id),
	CONSTRAINT FK_BusinessValue_Feature FOREIGN KEY (BusinessValue) REFERENCES [BusinessValue] ([Name])
)
