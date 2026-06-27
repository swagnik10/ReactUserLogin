using Backend.Application.Service;
using Backend.Authorization;
using Backend.DTOs.Roles;

namespace Backend.Application.Services;

public class RolePermissionService : IRolePermissionService
{
    public IEnumerable<RoleSummaryDto> GetRoles()
    {
        return RolePermissions.Map.Select(role => new RoleSummaryDto
        {
            Name = role.Key,

            PermissionCount = role.Value.Count
        });
    }

    public RoleDto? GetRole(string roleName)
    {
        if (!RolePermissions.Map.TryGetValue(roleName, out var grantedPermissions))
            return null;

        return CreateRoleDto(roleName);
    }

    private static RoleDto CreateRoleDto(string roleName)
    {
        var grantedPermissions = RolePermissions.Map[roleName];

        return new RoleDto
        {
            Name = roleName,

            Permissions = Permissions.All
            .OrderBy(p => p)
            .Select(permission => CreatePermissionDto(permission, grantedPermissions))
            .ToList(),

            Description = RoleMetadata.Descriptions.GetValueOrDefault(roleName, string.Empty)
        };
    }

    private static PermissionDto CreatePermissionDto(string permission, HashSet<string> grantedPermissions)
    {
        return new PermissionDto
        {
            Name = permission,
            Category = permission.Split('.')[0],
            Granted = grantedPermissions.Contains(permission)
        };
    }
}