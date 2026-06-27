using Backend.Application.Service;
using Backend.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;

    public RolesController(IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
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
}