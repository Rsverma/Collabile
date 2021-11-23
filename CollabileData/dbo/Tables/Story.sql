CREATE TABLE [dbo].[Story]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(),
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[Type] INT NOT NULL,
	[State] INT NOT NULL,
	[ParentFeature] UNIQUEIDENTIFIER NULL,
	[Sprint] INT NULL,
	[Priority] INT NOT NULL,
	[AcceptanceCriteria] NVARCHAR(MAX) NOT NULL,
	[CreateDate] DATETIME2 NOT NULL,
	[Assignee] VARCHAR(36) NOT NULL,
	[Reporter] VARCHAR(36) NOT NULL,
	CONSTRAINT FK_Feature_Story FOREIGN KEY (ParentFeature) REFERENCES [Feature] (Id),
	CONSTRAINT FK_User_Story_Assignee FOREIGN KEY (Assignee) REFERENCES [CollabileUser] (Id),
	CONSTRAINT FK_User_Story_Reporter FOREIGN KEY (Reporter) REFERENCES [CollabileUser] (Id),
	CONSTRAINT FK_Sprint_Story FOREIGN KEY (Sprint) REFERENCES Sprint (Id)
)

