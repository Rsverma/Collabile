CREATE TABLE [dbo].[TeamMember]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Team] INT NOT NULL,
	[Member] INT NOT NULL,
	[TeamRole] INT NOT NULL,
	CONSTRAINT FK_Team_TeamMember FOREIGN KEY ([Team]) REFERENCES [Team] ([Id]),
	CONSTRAINT FK_Member_TeamMember FOREIGN KEY ([Member]) REFERENCES [User] (Id)
)
