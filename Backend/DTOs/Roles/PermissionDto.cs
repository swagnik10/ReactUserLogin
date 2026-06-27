namespace Backend.DTOs.Roles;

public class PermissionDto
{
    public string Name { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public bool Granted { get; set; }
}