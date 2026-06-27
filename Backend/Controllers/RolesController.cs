using Backend.Application.CommandAndQuery;
using Backend.Application.Service;
using Backend.Authorization;
using Backend.DTOs.AI;
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

    public RolesController(IRolePermissionService rolePermissionService, IMediator mediator)
    {
        _rolePermissionService = rolePermissionService;
        _mediator = mediator;
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
        var role = _rolePermissionService.GetRole(roleName);

        if (role is null)
            return NotFound();

        return Ok(role);
    }

    [HttpGet("{roleName}/analyze")]
    public async Task<ActionResult<RoleAnalysisDto>> AnalyzeRole(string roleName, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new AnalyzeRoleRequest(roleName), cancellationToken);

        return Ok(result);
    }
}