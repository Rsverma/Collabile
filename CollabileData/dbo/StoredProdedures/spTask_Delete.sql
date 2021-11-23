CREATE PROCEDURE [dbo].[spTask_Delete]
	@Id uniqueidentifier
AS
	
	Delete FROM [Task] Where Id=@Id
