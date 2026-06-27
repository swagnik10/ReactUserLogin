namespace Backend.Authorization;

public static class RolePermissions
{
    public static readonly Dictionary<string, HashSet<string>> Map =
        new()
        {
            ["User"] =
            [
                Permissions.Users.View
            ],

            ["PowerUser"] =
            [
                Permissions.Users.View,
                Permissions.Users.Edit
            ],

            ["Admin"] =
            [
                Permissions.Users.View,
                Permissions.Users.Edit,
                Permissions.Users.Delete,
                Permissions.Users.AssignRole,
                Permissions.Roles.View,
                Permissions.AI.Execute
            ],

            ["SystemAdministrator"] =
            [
                Permissions.Users.View,
                Permissions.Users.Edit,
                Permissions.Users.Delete,
                Permissions.Users.AssignRole,
                Permissions.Users.Create,
                Permissions.Roles.View,
                Permissions.Roles.Edit,
                Permissions.AI.Execute,
                Permissions.AI.Audit,
                Permissions.Settings.View,
                Permissions.Settings.Edit
            ]
        };
}
