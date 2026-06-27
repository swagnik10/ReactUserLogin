using Backend.DTOs.AI.Phase1;
using Backend.DTOs.Roles;

public static class RoleAnalysisMapper
{
    public static List<RoleAnalysisRequest> ToRoleAnalysisRequests(
        IEnumerable<RoleDto> roles)
    {
        return roles.Select(role => new RoleAnalysisRequest
        {
            Name = role.Name,
            Description = role.Description,

            GrantedPermissions = role.Permissions
                .Where(p => p.Granted)
                .Select(p => p.Name)
                .ToList(),

            DeniedPermissions = role.Permissions
                .Where(p => !p.Granted)
                .Select(p => p.Name)
                .ToList()
        })
        .ToList();
    }
}