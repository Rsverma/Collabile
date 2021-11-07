CREATE PROCEDURE [dbo].[spTask_Update]
	@Id INT,
    @Title NVARCHAR(500),
    @Description NVARCHAR(MAX),
    @State INT,
    @EstimatedEffort DECIMAL,
    @StartDate DATETIME2,
    @DueDate DATETIME2,
    @CreateDate DATETIME2,
    @Assignee INT,
    @Reporter INT
AS
	Update [Task]
	SET [Title] = @Title,
	[Description]=@Description,
	[State]=@State,
	EstimatedEffort=@EstimatedEffort,
	StartDate=@StartDate,
	DueDate=@DueDate,
	CreateDate=@CreateDate,
	Assignee=@Assignee,
	Reporter=@Reporter
	WHERE Id=@Id
Return 0