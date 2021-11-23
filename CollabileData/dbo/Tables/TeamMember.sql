CREATE TABLE [dbo].[TeamMember]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Team] NVARCHAR(50) NOT NULL,
	[Member] VARCHAR(36) NOT NULL,
	[TeamRole] INT NOT NULL,
	CONSTRAINT FK_Team_TeamMember FOREIGN KEY ([Team]) REFERENCES [Team] ([Name]),
	CONSTRAINT FK_Member_TeamMember FOREIGN KEY ([Member]) REFERENCES [CollabileUser] (Id)
)
