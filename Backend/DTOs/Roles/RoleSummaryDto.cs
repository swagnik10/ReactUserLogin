namespace Backend.DTOs.Roles;

public class RoleSummaryDto
{
    public string Name { get; set; } = string.Empty;

    public int PermissionCount { get; set; }

    public int UserCount { get; set; }

    public string Description { get; set; } = string.Empty;
}