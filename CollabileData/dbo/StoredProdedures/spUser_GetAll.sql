CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
Begin
	SELECT [Username],[UserRole]
	from [dbo].[User]
RETURN 0
End

