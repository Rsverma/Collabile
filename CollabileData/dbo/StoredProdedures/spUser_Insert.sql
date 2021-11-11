CREATE PROCEDURE [dbo].[spUser_Insert]
        @Username NVARCHAR(50),
        @Password NVARCHAR(128),
        @UserRole NVARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT(EXISTS(SELECT * FROM [User] WHERE Username = @Username))
    BEGIN
        INSERT INTO [User] (Username,[Password],[UserRole])
                    Values (@Username, @Password, @UserRole)
        Select @@IdENTITY;
    END
    ELSE
        Select -1
END
