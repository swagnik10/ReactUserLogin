namespace Backend.DTOs.Roles;

public class RoleDto
{
    public string Name { get; set; } = string.Empty;

    public List<PermissionDto> Permissions { get; set; } = [];
}
