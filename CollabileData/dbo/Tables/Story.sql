CREATE TABLE [dbo].[Story]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentFeature] INT NULL,
	[Sprint] INT NULL,
	[Priority] INT NOT NULL,
	[AcceptanceCriteria] NVARCHAR(MAX) NOT NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[Assignee] INT NOT NULL,
	[Reporter] INT NOT NULL,
	CONSTRAINT FK_User_Story_Assignee FOREIGN KEY (Assignee) REFERENCES [User] (ID)
        ON UPDATE CASCADE,
	CONSTRAINT FK_User_Story_Reporter FOREIGN KEY (Reporter) REFERENCES [User] (ID)
        ON UPDATE CASCADE
)

