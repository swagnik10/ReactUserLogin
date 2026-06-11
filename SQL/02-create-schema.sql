USE UserManagementDB;
GO

IF NOT EXISTS (
    SELECT *
    FROM sys.schemas
    WHERE name = 'auth'
)
BEGIN
    EXEC('CREATE SCHEMA auth');
END
GO