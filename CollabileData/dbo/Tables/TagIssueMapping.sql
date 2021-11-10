CREATE TABLE [dbo].[TagIssueMapping]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Tag] NVARCHAR(20) NOT NULL,
	[Issue] INT NOT NULL,
	[IssueType] INT NOT NULL,
	CONSTRAINT FK_Tag_TagIssueMapping FOREIGN KEY ([Tag]) REFERENCES [Tag] ([Name]),
)
