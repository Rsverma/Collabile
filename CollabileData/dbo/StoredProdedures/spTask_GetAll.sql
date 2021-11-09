CREATE PROCEDURE [dbo].[spTask_GetAll]
AS
Begin
	SELECT [Id],[Title],[Description],[State],[EstimatedEffort],
		[StartDate],[DueDate],[CreateDate],[Assignee],[Reporter]
	FROM [dbo].[Task]
RETURN 0
End

