CREATE PROCEDURE [dbo].[spUser_Update]
	@Username NVARCHAR(50),
	@Password NVARCHAR(256),
	@UserRole NVARCHAR(10)

AS
	IF @Password IS NOT NULL AND @Password <>''
		Update [User]
		SET [Password]=@Password
		WHERE Username=@Username

	Update [User]
	SET [UserRole]=@UserRole
	WHERE Username=@Username
Return 0