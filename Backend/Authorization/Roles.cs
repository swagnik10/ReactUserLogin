namespace Backend.Authorization;

public static class Roles
{
    public const string User = "User";
    public const string PowerUser = "PowerUser";
    public const string Admin = "Admin";
    public const string SystemAdministrator = "SystemAdministrator";

    public static IEnumerable<string> All =>
    [
        User,
        PowerUser,
        Admin,
        SystemAdministrator
    ];
}
