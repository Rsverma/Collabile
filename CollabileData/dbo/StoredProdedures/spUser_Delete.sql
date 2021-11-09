CREATE PROCEDURE [dbo].[spUser_Delete]
	@Id int
AS
	--Delete FROM [Expense] Where UserId = @Id
	Delete FROM [User] Where Id=@Id
