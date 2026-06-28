using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.DTOs.AI.Phase1;
using Backend.DTOs.AI.Phase3;
using MediatR;

namespace Backend.Application.AI.Phase3;

public class AuditRbacHandler : IRequestHandler<AuditRbacRequest, RbacAuditDto>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IAiRbacAuditService _auditService;

    public AuditRbacHandler(
        IRolePermissionService rolePermissionService,
        IAiRbacAuditService auditService)
    {
        _rolePermissionService = rolePermissionService;
        _auditService = auditService;
    }

    public async Task<RbacAuditDto> Handle(
        AuditRbacRequest request,
        CancellationToken cancellationToken)
    {
        var roles = _rolePermissionService.GetRolesWithPermissions();

        var auditRequest =
            new RbacAuditRequest
            {
                Roles = RoleAnalysisMapper.ToRoleAnalysisRequests(
        _rolePermissionService.GetRolesWithPermissions())
            };

        return await _auditService.AuditAsync(
            auditRequest,
            cancellationToken);
    }
}