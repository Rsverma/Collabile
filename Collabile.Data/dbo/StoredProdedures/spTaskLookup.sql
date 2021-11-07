CREATE PROCEDURE [dbo].[spTaskLookup]
	@UserName NVARCHAR(50)
AS
Begin
	set nocount on;

	SELECT TOP 5 [Id],[Title],[Description],[State],[EstimatedEffort],
		[StartDate],[DueDate],[CreateDate],[Assignee],[Reporter]
	FROM [dbo].[Task]
End
