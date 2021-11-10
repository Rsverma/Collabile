CREATE TABLE [dbo].[Epic]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Title] NVARCHAR(500) NOT NULL,
	[Description] NVARCHAR(MAX) NOT NULL,
	[State] INT NOT NULL,
	[Priority] INT NOT NULL,
	[ProjectArea] NVARCHAR(20) NOT NULL,
	CONSTRAINT FK_ProjectArea_Epic FOREIGN KEY (ProjectArea) REFERENCES [ProjectArea] ([Name])
)
