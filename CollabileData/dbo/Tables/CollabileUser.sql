CREATE TABLE [dbo].[CollabileUser]
(
    [Id] VARCHAR(36) PRIMARY KEY default NEWID(),
    [Username] NVARCHAR(50) NOT NULL UNIQUE, 
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [PasswordHash] NVARCHAR(255) NOT NULL,
    [FirstName] NVARCHAR(30) NOT NULL,
    [LastName] NVARCHAR(30) NOT NULL,
    EmailConfirmed BIT NOT NULL default 1,
    PhoneNumber VARCHAR(15) NULL,
    CreatedBy VARCHAR(36) NOT NULL,
    AccessFailedCount TINYINT NOT NULL default 0,
    [CreatedOn] DATETIME2 NOT NULL default getutcdate(),
    IsDeleted BIT NOT NULL default 0,
    [DeletedOn] DATETIME2 NULL,
    LastModifiedBy VARCHAR(36) NOT NULL,
    LastModifiedOn DATETIME2 NOT NULL default getutcdate(),
    PhoneNumberConfirmed BIT NOT NULL default 0,
    UserRole VARCHAR(15) NOT NULL default 'Basic',
    Notes NVARCHAR(MAX)  NOT NULL default ''
)
