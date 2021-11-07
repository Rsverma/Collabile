CREATE PROCEDURE [dbo].[spUserLookup]
	@UserName NVARCHAR(50)
AS
Begin
	set nocount on;

	SELECT Id,UserName,[Password],[Role]
	from [dbo].[User]
	where UserName = @UserName
End
