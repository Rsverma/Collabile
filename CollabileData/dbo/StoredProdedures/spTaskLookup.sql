CREATE PROCEDURE [dbo].[spTaskLookup]
	@ProjectKey NVARCHAR(20),
	@Id INT
AS
Begin
	set nocount on;

	SELECT [Id],[Title],[Description],[State],[EstimatedEffort],
		[StartDate],[DueDate],[CreateDate],[Assignee],[Reporter]
	FROM [dbo].[Task]
End
