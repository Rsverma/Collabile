﻿CREATE TABLE [dbo].[StoryLink]
(
	[Id] INT NOT NULL PRIMARY KEY IdENTITY,
	[Story1] UNIQUEIDENTIFIER NOT NULL,
	[Story2] UNIQUEIDENTIFIER NOT NULL,
	CONSTRAINT FK_Story1_StoryLink FOREIGN KEY ([Story1]) REFERENCES [Story] ([Id]),
	CONSTRAINT FK_Story2_StoryLink FOREIGN KEY ([Story2]) REFERENCES [Story] (Id)
)
