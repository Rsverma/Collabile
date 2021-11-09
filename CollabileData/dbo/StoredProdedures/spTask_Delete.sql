CREATE PROCEDURE [dbo].[spTask_Delete]
	@Id int
AS
	
	Delete FROM [Task] Where Id=@Id
