CREATE TABLE [dbo].[Task]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentStory] UNIQUEIDENTIFIER NULL,
	[EstimatedEffort] DECIMAL NOT NULL,
	[StartDate] DATETIME2 NULL,
	[DueDate] DATETIME2 NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[Assignee] VARCHAR(36) NULL,
	[Reporter] VARCHAR(36) NOT NULL,
	CONSTRAINT FK_Story_Task FOREIGN KEY (ParentStory) REFERENCES [Story] (Id),
	CONSTRAINT FK_User_Task_Assignee FOREIGN KEY (Assignee) REFERENCES [CollabileUser] (Id),
	CONSTRAINT FK_User_Task_Reporter FOREIGN KEY (Reporter) REFERENCES [CollabileUser] (Id)
)
