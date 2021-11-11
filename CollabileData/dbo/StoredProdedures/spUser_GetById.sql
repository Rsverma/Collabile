CREATE PROCEDURE [dbo].[spUser_GetById]
	@Username NVARCHAR(50)
AS
Begin
	set nocount on;

	SELECT [Username],[UserRole]
	from [dbo].[User]
	where Username = @Username
End
