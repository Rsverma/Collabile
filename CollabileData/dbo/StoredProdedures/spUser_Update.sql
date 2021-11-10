CREATE PROCEDURE [dbo].[spUser_Update]
	@Id int,
	@UserName NVARCHAR(50),
	@Password NVARCHAR(128),
    @Token NVARCHAR(256),
	@Role NVARCHAR(50)

AS
	Update [User]
	SET UserName=@UserName,
	[Password]=@Password,
	[UserRole]=@Role
	WHERE Id=@Id
Return 0