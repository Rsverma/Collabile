CREATE TABLE [dbo].[Task]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentStory] INT NULL,
	[EstimatedEffort] DECIMAL NOT NULL,
	[StartDate] DATETIME2 NULL,
	[DueDate] DATETIME2 NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[Assignee] NVARCHAR(50 )NULL,
	[Reporter] NVARCHAR(50) NOT NULL,
	CONSTRAINT FK_User_Task_Assignee FOREIGN KEY (Assignee) REFERENCES [User] (Username),
	CONSTRAINT FK_User_Task_Reporter FOREIGN KEY (Reporter) REFERENCES [User] (Username)
)
