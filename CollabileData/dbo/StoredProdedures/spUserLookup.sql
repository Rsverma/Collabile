CREATE PROCEDURE [dbo].[spUserLookup]
	@Username NVARCHAR(50)
AS
Begin
	set nocount on;

	SELECT Username,[Password],[UserRole]
	from [dbo].[User]
	where Username = @Username
End
