using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Backend.Authorization;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        var role = context.User.FindFirst(ClaimTypes.Role)?.Value;

        if (string.IsNullOrWhiteSpace(role))
            return Task.CompletedTask;

        if (!RolePermissions.Map.TryGetValue(role, out var permissions))
            return Task.CompletedTask;

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
}