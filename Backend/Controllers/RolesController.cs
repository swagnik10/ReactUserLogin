using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.Authorization;
using Backend.DTOs.AI.Phase1;
using Backend.DTOs.AI.Phase2;
using Backend.DTOs.AI.Phase3;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;
    private readonly IMediator _mediator;
    private readonly ILogger<RolesController> _logger;

    public RolesController(IRolePermissionService rolePermissionService, IMediator mediator, ILogger<RolesController> logger)
    {
        _rolePermissionService = rolePermissionService;
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet]
    [HasPermission(Permissions.Roles.View)]
    public IActionResult GetRoles()
    {
        return Ok(_rolePermissionService.GetRoles());
    }

    [HttpGet("{roleName}")]
    [HasPermission(Permissions.Roles.View)]
    public IActionResult GetRole(string roleName)
    {
        _logger.LogInformation("Get {RoleName} execution Starts", roleName);

        var role = _rolePermissionService.GetRole(roleName);

        if (role is null)
            return NotFound();

        _logger.LogInformation("Get {RoleName} execution Ends", roleName);

        return Ok(role);
    }

    [HttpGet("{roleName}/analyze")]
    public async Task<ActionResult<RoleAnalysisDto>> AnalyzeRole(string roleName, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AI Analyze of {RoleName} Starts", roleName);

        var result = await _mediator.Send(new AnalyzeRoleRequest(roleName), cancellationToken);

        _logger.LogInformation("AI Analyze of {RoleName} Ends", roleName);

        return Ok(result);
    }

    [HttpPost("compare")]
    public async Task<ActionResult<RoleComparisonDto>> CompareRoles(CompareRolesBody body, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AI Role Comparer API Starts");

        var result = await _mediator.Send(
            new CompareRolesRequest(
                body.RoleA,
                body.RoleB),
            cancellationToken);

        _logger.LogInformation("AI Role Comparer API Ends");

        return Ok(result);
    }

    [HttpPost("audit")]
    public async Task<ActionResult<RbacAuditDto>> Audit(
    CancellationToken cancellationToken)
    {
        _logger.LogInformation("AI Role Audit API Starts");

        var result = await _mediator.Send(
            new AuditRbacRequest(),
            cancellationToken);

        _logger.LogInformation("AI Role Audit API Ends");

        return Ok(result);
    }

    [HttpPost("ask-ai")]
    public async Task<ActionResult<AskRbacQuestionResponse>> AskAi(
    [FromBody] AskRbacQuestionRequest request,
    CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ask AI API Starts");

        var result = await _mediator.Send(
            new AskRbacQuestion(request.Question),
            cancellationToken);

        _logger.LogInformation("Ask AI API Ends");

        return Ok(result);
    }
}