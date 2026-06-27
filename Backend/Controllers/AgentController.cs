using Backend.Application.CommandAndQuery;
using Backend.Authorization;
using Backend.DTOs.Agent;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AgentController> _logger;

    public AgentController(
        IMediator mediator,
        ILogger<AgentController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HasPermission(Permissions.AI.Audit)]
    [HttpPost("plan")]
    public async Task<IActionResult> GeneratePlan(AgentPromptDto request)
    {
        var plan = await _mediator.Send(
            new GeneratePlanRequest(request.Prompt));

        return Ok(plan);
    }
    [HasPermission(Permissions.AI.Execute)]
    [HttpPost("execute")]
    public async Task<IActionResult> ExecutePlan(ExecutePlanDto request)
    {
        var result = await _mediator.Send(new ExecutePlanRequest(request.Plan));

        return Ok(new
        {
            Message = "Plan executed successfully.",
            Result = result
        });
    }
}