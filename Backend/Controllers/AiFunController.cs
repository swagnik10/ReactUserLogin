using System.Security.Claims;
using Backend.Application.CommandAndQuery;
using Backend.Authorization;
using Backend.DTOs.AI.Ai_Fun;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/ai-fun")]
[HasPermission(Permissions.AI.AiFun)]
public class AiFunController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AiFunController> _logger;

    public AiFunController(IMediator mediator, ILogger<AiFunController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("nickname")]
    public async Task<ActionResult<NicknameResponse>> GenerateNickname([FromBody] UserFunRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ai fun for Nick Name Started");
        request.Role = User.FindFirstValue(ClaimTypes.Role)!;

        var response = await _mediator.Send(
            new GenerateNicknameRequest(request),
            cancellationToken);

        _logger.LogInformation("Ai fun for Nick Name Ended");

        return Ok(response);
    }

    [HttpPost("roast")]
    public async Task<ActionResult<RoastResponse>> GenerateRoast([FromBody] UserFunRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ai fun for Roast Started");
        request.Role = User.FindFirstValue(ClaimTypes.Role)!;

        var response = await _mediator.Send(
            new GenerateRoastRequest(request),
            cancellationToken);
        _logger.LogInformation("Ai fun for Roast Ended");

        return Ok(response);
    }

    [HttpPost("fortune")]
    public async Task<ActionResult<FortuneResponse>> GenerateFortune([FromBody] UserFunRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ai fun for fortune Started");
        request.Role = User.FindFirstValue(ClaimTypes.Role)!;

        var response = await _mediator.Send(
            new GenerateFortuneRequest(request),
            cancellationToken);
        _logger.LogInformation("Ai fun for fortune ended");
        return Ok(response);
    }

    [HttpPost("achievement")]
    public async Task<ActionResult<AchievementResponse>> GenerateAchievement([FromBody] UserFunRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Ai fun for achievment Started");
        request.Role = User.FindFirstValue(ClaimTypes.Role)!;

        var response = await _mediator.Send(
            new GenerateAchievementRequest(request),
            cancellationToken);
        _logger.LogInformation("Ai fun for achievment ended");

        return Ok(response);
    }

}
