CREATE PROCEDURE [dbo].[spUser_Delete]
	@Username NVARCHAR(50)
AS
	--Delete FROM [Expense] Where UserId = @Id
	Delete FROM [CollabileUser] Where Username = @Username
