CREATE PROCEDURE [dbo].[spUserLookup]
	@Username NVARCHAR(50)
AS
Begin
	set nocount on;

	SELECT [PasswordHash], [UserRole]
	from [dbo].[CollabileUser]
	where Username = @Username
End
