using Backend.Application.AI;
using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.DTOs.AI;
using MediatR;

namespace Backend.Application.Handlers.RoleHandler;

public class AnalyzeRoleHandler : IRequestHandler<AnalyzeRoleRequest, RoleAnalysisDto>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IAiRoleAnalyzerService _roleAnalyzer;
    private readonly ILogger<AnalyzeRoleHandler> _logger;

    public AnalyzeRoleHandler(
        IRolePermissionService rolePermissionService,
        IAiRoleAnalyzerService roleAnalyzer,
        ILogger<AnalyzeRoleHandler> logger)
    {
        _rolePermissionService = rolePermissionService;
        _roleAnalyzer = roleAnalyzer;
        _logger = logger;
    }

    public async Task<RoleAnalysisDto> Handle(AnalyzeRoleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Analyzing role {RoleName}", request.RoleName);

        var role = _rolePermissionService.GetRole(request.RoleName);

        if (role == null)
        {
            throw new Exception("Role not found.");
        }

        var aiRequest = new RoleAnalysisRequest
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

        };

        var analysis =
            await _roleAnalyzer.AnalyzeRoleAsync(
                aiRequest,
                cancellationToken);

        _logger.LogInformation(
            "Completed AI analysis for role {RoleName}",
            request.RoleName);

        return analysis;
    }
}