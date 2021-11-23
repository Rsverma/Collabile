CREATE PROCEDURE [dbo].[spUser_GetById]
	@Username NVARCHAR(50)
AS
Begin
	set nocount on;
	
	SELECT [Username],[UserRole]
	from [dbo].[CollabileUser] where Username = @Username
	
	SELECT [Key]
	from [dbo].[Project] where [Owner] = @Username

	SELECT [Team],[Member],[TeamRole]
	from [dbo].[TeamMember] where Member = @Username

End
