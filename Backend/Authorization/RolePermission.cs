namespace Backend.Authorization;

public static class RolePermissions
{
    public static readonly Dictionary<string, HashSet<string>> Map =
        new()
        {
            [Roles.User] =
            [
                Permissions.Users.View
            ],

            [Roles.PowerUser] =
            [
                Permissions.Users.View,
                Permissions.Users.Edit
            ],

            [Roles.Admin] =
            [
                Permissions.Users.View,
                Permissions.Users.Edit,
                Permissions.Users.Delete,
                Permissions.Users.AssignRole,
                Permissions.Roles.View,
                Permissions.AI.Execute
            ],

            [Roles.SystemAdministrator] =
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
