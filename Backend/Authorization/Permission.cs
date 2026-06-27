namespace Backend.Authorization;

public static class Permissions
{
    public static class Users
    {
        public const string View = "Users.View";
        public const string Create = "Users.Create";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";
        public const string AssignRole = "Users.AssignRole";
    }

    public static class Roles
    {
        public const string View = "Roles.View";
        public const string Edit = "Roles.Edit";
    }

    public static class Settings
    {
        public const string View = "Settings.View";
        public const string Edit = "Settings.Edit";
    }

    public static class AI
    {
        public const string Execute = "AI.Execute";
        public const string Audit = "AI.Audit";
    }

    public static IEnumerable<string> All =>
    [
        Users.View,
        Users.Create,
        Users.Edit,
        Users.Delete,
        Users.AssignRole,

        Roles.View,
        Roles.Edit,

        Settings.View,
        Settings.Edit,

        AI.Execute,
        AI.Audit
    ];
}