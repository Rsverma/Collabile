﻿CREATE TABLE [dbo].[Sprint]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Index] INT NOT NULL,
	[Release] INT NOT NULL,
	CONSTRAINT FK_Release_Sprint FOREIGN KEY ([Release]) REFERENCES [Release] (ID)
)
