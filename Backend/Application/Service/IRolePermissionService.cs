using Backend.DTOs.Roles;

namespace Backend.Application.Service;

public interface IRolePermissionService
{
    IEnumerable<RoleSummaryDto> GetRoles();

    RoleDto? GetRole(string roleName);
}
