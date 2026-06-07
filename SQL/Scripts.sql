create database UserManagementDB;

 Use UserManagementDB;

create schema auth;

CREATE TABLE auth.Users
(
    UserId INT IDENTITY(1,1) NOT NULL,
    Username VARCHAR(100) NOT NULL,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    PhoneNumber VARCHAR(20),
    Password VARCHAR(255) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT PK_Users PRIMARY KEY (UserId),

    CONSTRAINT UQ_Users_Username UNIQUE (Username),

    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);

CREATE TABLE auth.Roles
(
    RoleId INT IDENTITY(1,1) NOT NULL,
    Name VARCHAR(50) NOT NULL,

    CONSTRAINT PK_Roles PRIMARY KEY (RoleId),

    CONSTRAINT UQ_Roles_Name UNIQUE (Name)
);

CREATE TABLE auth.UserRoles
(
    UserId INT NOT NULL,
    RoleId INT NOT NULL,

    CONSTRAINT PK_UserRoles PRIMARY KEY (UserId, RoleId),

    CONSTRAINT FK_UserRoles_Users FOREIGN KEY (UserId) REFERENCES auth.Users(UserId),

    CONSTRAINT FK_UserRoles_Roles FOREIGN KEY (RoleId) REFERENCES auth.Roles(RoleId)
);


INSERT INTO auth.Roles(Name) VALUES ('Admin'),('User');