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
    INSERT INTO auth.users
    (
        username,
        first_name,
        last_name,
        email,
        phone_number,
        password,
        is_active,
        created_at
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
    FROM auth.user_roles
    WHERE user_id = 1
      AND role_id = 1
)
BEGIN
    INSERT INTO auth.user_roles
    (
        user_id,
        role_id
    )
    VALUES
    (
        1,
        1
    );
GO