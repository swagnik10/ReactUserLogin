using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.DTOs.AI.Phase1;
using Backend.DTOs.AI.Phase3;
using MediatR;

namespace Backend.Application.AI.Phase3;

public class AskRbacQuestionHandler
    : IRequestHandler<AskRbacQuestion, AskRbacQuestionResponse>
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IAiRbacAssistantService _assistantService;

    public AskRbacQuestionHandler(
        IRolePermissionService rolePermissionService,
        IAiRbacAssistantService assistantService)
    {
        _rolePermissionService = rolePermissionService;
        _assistantService = assistantService;
    }

    public async Task<AskRbacQuestionResponse> Handle(
        AskRbacQuestion request,
        CancellationToken cancellationToken)
    {
        var roles = _rolePermissionService.GetRolesWithPermissions();

        var aiRequest = new RbacQuestion
        {
            Question = request.Question,

            Roles = RoleAnalysisMapper.ToRoleAnalysisRequests(
        _rolePermissionService.GetRolesWithPermissions())
        };

        return await _assistantService.AskQuestionAsync(
            aiRequest,
            cancellationToken);
    }
}