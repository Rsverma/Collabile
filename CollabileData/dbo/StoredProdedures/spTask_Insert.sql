CREATE PROCEDURE [dbo].[spTask_Insert]
        @Id INT,
        @Title NVARCHAR(500),
        @Description NVARCHAR(MAX),
        @State INT,
        @EstimatedEffort DECIMAL,
        @StartDate DATETIME2,
        @DueDate DATETIME2,
        @CreateDate DATETIME2,
        @Assignee uniqueidentifier,
        @Reporter uniqueidentifier
AS
BEGIN
	SET NOCOUNT ON;
    BEGIN
        INSERT INTO [Task] ([Title],[Description],[State],[EstimatedEffort],
		            [StartDate],[DueDate],[CreateDate],[Assignee],[Reporter])
                    Values (@Title, @Description, @State, @EstimatedEffort, @StartDate,
                            @DueDate, @CreateDate, @Assignee, @Reporter)
        Select @@IdENTITY;
    END
END
