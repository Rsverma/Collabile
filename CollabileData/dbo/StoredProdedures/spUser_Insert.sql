CREATE PROCEDURE [dbo].[spUser_Insert]
        @Id INT,
        @Username NVARCHAR(50),
        @Password NVARCHAR(128),
        @Token NVARCHAR(256),
        @Role NVARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;
	IF NOT(EXISTS(SELECT * FROM [User] WHERE UserName = @Username))
    BEGIN
        INSERT INTO [User] (UserName,[Password],[UserRole])
                    Values (@Username, @Password, @Role)
        Select @@IdENTITY;
    END
    ELSE
        Select -1
END
