using Backend.DTOs.Roles;

namespace Backend.Application.Service;

public interface IRolePermissionService
{
    IEnumerable<RoleDto> GetRoles();

    RoleDto? GetRole(string roleName);
}
