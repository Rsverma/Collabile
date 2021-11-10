CREATE TABLE [dbo].[Story]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
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
	CONSTRAINT FK_User_Story_Assignee FOREIGN KEY (Assignee) REFERENCES [User] (Id)
        ON UPDATE CASCADE,
	CONSTRAINT FK_User_Story_Reporter FOREIGN KEY (Reporter) REFERENCES [User] (Id)
        ON UPDATE CASCADE
)

