CREATE PROCEDURE [dbo].[spUser_Insert]
        @Username NVARCHAR(50),
        @Password NVARCHAR(128),
        @UserRole NVARCHAR(10)
AS
BEGIN
	IF NOT(EXISTS(SELECT * FROM [User] WHERE Username = @Username))
    BEGIN
        INSERT INTO [User] (Username,[Password],[UserRole])
                    Values (@Username, @Password, @UserRole)
    END
END
