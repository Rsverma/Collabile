CREATE PROCEDURE [dbo].[spUser_Update]
	@Username NVARCHAR(50),
	@Password NVARCHAR(128),
    @Token NVARCHAR(256),
	@Role NVARCHAR(50)

AS
	Update [User]
	SET [Password]=@Password,
	[UserRole]=@Role
	WHERE Username=@Username
Return 0