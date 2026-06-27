namespace Backend.Authorization;

public static class RoleMetadata
{
    public static readonly Dictionary<string, string> Descriptions =
        new()
        {
            [Roles.User] =
                "Standard user with access to personal data and self-service features.",

            [Roles.PowerUser] =
                "Advanced user with additional operational capabilities beyond a standard user.",

            [Roles.Admin] =
                "Responsible for managing users, assigning roles, and performing administrative operations.",

            [Roles.SystemAdministrator] =
                "Full platform administrator with unrestricted access to users, roles, settings, and AI features."
        };
}