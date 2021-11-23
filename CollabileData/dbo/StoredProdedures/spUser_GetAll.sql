CREATE PROCEDURE [dbo].[spUser_GetAll]
AS
Begin
	SELECT [Username],[UserRole]
	from [dbo].[CollabileUser]
RETURN 0
End

