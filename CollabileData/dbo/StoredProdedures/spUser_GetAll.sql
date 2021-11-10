CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
Begin
	SELECT Username,[Password],[UserRole]
	FROM [dbo].[User]
RETURN 0
End

