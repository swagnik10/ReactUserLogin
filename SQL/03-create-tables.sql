USE UserManagementDB;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Users'
      AND s.name = 'auth'
)
BEGIN
    CREATE TABLE auth.users
    (
        user_id INT IDENTITY(1,1) NOT NULL,
        username VARCHAR(100) NOT NULL,
        first_name VARCHAR(100) NOT NULL,
        last_name VARCHAR(100) NOT NULL,
        email VARCHAR(255) NOT NULL,
        phone_number VARCHAR(20),
        password VARCHAR(255) NOT NULL,
        is_active BIT NOT NULL DEFAULT 1,
        created_at DATETIME NOT NULL DEFAULT GETDATE(),

        CONSTRAINT pk_users PRIMARY KEY (user_id),
        CONSTRAINT uq_users_username UNIQUE (username),
        CONSTRAINT uq_users_email UNIQUE (email)
    );
END
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'Roles'
      AND s.name = 'auth'
)
BEGIN
    CREATE TABLE auth.roles
    (
        role_id INT IDENTITY(1,1) NOT NULL,
        name VARCHAR(50) NOT NULL,

        CONSTRAINT pk_roles PRIMARY KEY (role_id),
        CONSTRAINT uq_roles_name UNIQUE (name)
    );
END
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.tables t
    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
    WHERE t.name = 'UserRoles'
      AND s.name = 'auth'
)
BEGIN
    CREATE TABLE auth.user_roles
    (
        user_id INT NOT NULL,
        role_id INT NOT NULL,

        CONSTRAINT pk_user_roles PRIMARY KEY (user_id, role_id),

        CONSTRAINT fk_user_roles_users
            FOREIGN KEY (user_id)
            REFERENCES auth.users(user_id),

        CONSTRAINT fk_user_roles_roles
            FOREIGN KEY (role_id)
            REFERENCES auth.roles(role_id)
    );
END
GO