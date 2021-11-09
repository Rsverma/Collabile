CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
Begin
	SELECT Id,UserName,[Password],[Role]
	FROM [dbo].[User]
RETURN 0
End

