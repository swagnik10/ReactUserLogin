using Backend.Application.AI;
using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.DTOs.AI;
using MediatR;

namespace Backend.Application.Handlers.AgentHandler;

public class CompareRolesHandler : IRequestHandler<CompareRolesRequest, RoleComparisonDto>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IAiRoleComparerService _roleComparer;
    private readonly ILogger<CompareRolesHandler> _logger;

    public CompareRolesHandler(
        IRolePermissionService rolePermissionService,
        IAiRoleComparerService roleComparer,
        ILogger<CompareRolesHandler> logger)
    {
        _rolePermissionService = rolePermissionService;
        _roleComparer = roleComparer;
        _logger = logger;
    }

    public async Task<RoleComparisonDto> Handle(
    CompareRolesRequest request,
    CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Comparing roles {RoleA} and {RoleB}",
            request.RoleA,
            request.RoleB);

        var roleA = _rolePermissionService.GetRole(request.RoleA);

        var roleB = _rolePermissionService.GetRole(request.RoleB);

        if (roleA == null)
            throw new Exception($"Role '{request.RoleA}' not found.");

        if (roleB == null)
            throw new Exception($"Role '{request.RoleB}' not found.");

        var aiRequest = new RoleComparisonRequest
        {
            RoleA = new RoleAnalysisRequest
            {
                Name = roleA.Name,
                Description = roleA.Description,
                GrantedPermissions = roleA.Permissions
                    .Where(p => p.Granted)
                    .Select(p => p.Name)
                    .ToList(),

                DeniedPermissions = roleA.Permissions
                    .Where(p => !p.Granted)
                    .Select(p => p.Name)
                    .ToList()
            },

            RoleB = new RoleAnalysisRequest
            {
                Name = roleB.Name,
                Description = roleB.Description,
                GrantedPermissions = roleB.Permissions
                    .Where(p => p.Granted)
                    .Select(p => p.Name)
                    .ToList(),

                DeniedPermissions = roleB.Permissions
                    .Where(p => !p.Granted)
                    .Select(p => p.Name)
                    .ToList()
            }
        };

        return await _roleComparer.CompareRolesAsync(
            aiRequest,
            cancellationToken);
    }

}
