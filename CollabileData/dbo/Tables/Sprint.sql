CREATE TABLE [dbo].[Sprint]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Index] INT NOT NULL,
	[Release] INT NOT NULL,
	CONSTRAINT FK_Release_Sprint FOREIGN KEY ([Release]) REFERENCES [Release] (Id)
)
