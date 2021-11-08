CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentStory] INT NULL,
	[EstimatedEffort] DECIMAL NOT NULL,
	[StartDate] DATETIME2 NOT NULL,
	[DueDate] DATETIME2 NOT NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[Assignee] INT NULL,
	[Reporter] INT NOT NULL,
	CONSTRAINT FK_User_Task_Assignee FOREIGN KEY (Assignee) REFERENCES [User] (ID),
	CONSTRAINT FK_User_Task_Reporter FOREIGN KEY (Reporter) REFERENCES [User] (ID)
)
