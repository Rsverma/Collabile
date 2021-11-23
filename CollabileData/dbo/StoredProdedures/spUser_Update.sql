CREATE PROCEDURE [dbo].[spUser_Update]
	@Username NVARCHAR(50),
	@Password NVARCHAR(256),
	@UserRole NVARCHAR(10)

AS
	IF @Password IS NOT NULL AND @Password <>''
		Update [CollabileUser]
		SET [PasswordHash]=@Password
		WHERE Username=@Username

	Update [CollabileUser]
	SET [UserRole]=@UserRole
	WHERE Username=@Username
Return 0