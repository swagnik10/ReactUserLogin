USE UserManagementDB;
GO

IF NOT EXISTS (
    SELECT 1
    FROM auth.Roles
    WHERE Name = 'Admin'
)
BEGIN
    INSERT INTO auth.Roles(Name)
    VALUES ('Admin');
END

IF NOT EXISTS (
    SELECT 1
    FROM auth.Roles
    WHERE Name = 'User'
)
BEGIN
    INSERT INTO auth.Roles(Name)
    VALUES ('User');
END
IF NOT EXISTS
(
    SELECT 1
    FROM auth.Users
    WHERE Username = 'admin'
       OR Email = 'admin@test.com'
)
BEGIN
    INSERT INTO auth.Users
    (
        Username,
        FirstName,
        LastName,
        Email,
        PhoneNumber,
        Password,
        IsActive,
        CreatedAt
    )
    VALUES
    (
        'admin',
        'System',
        'Administrator',
        'admin@test.com',
        '9999999999',
        'Admin123',
        1,
        GETDATE()
    );
END
IF NOT EXISTS
(
    SELECT 1
    FROM auth.UserRoles
    WHERE UserId = 1
      AND RoleId = 1
)
BEGIN
    INSERT INTO auth.UserRoles
    (
        UserId,
        RoleId
    )
    VALUES
    (
        1,
        1
    );
END
GO