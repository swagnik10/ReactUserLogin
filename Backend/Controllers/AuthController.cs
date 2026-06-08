using Backend.DTOs.Auth;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IMediator _mediator;

    public AuthController(ILogger<AuthController> logger,
                         IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserBody request)
    {
        try
        {
            _logger.LogInformation("Registering user with username: {Username}", request.Username);
            await _mediator.Send(new RegisterUserRequest(request));

            return Ok(new
            {
                Message =
                    "User registered successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                Message = ex.Message
            });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginBody request)
    {
        try
        {
            _logger.LogInformation("Attempting login for email: {Email} and Password: {Password}", request.Email, request.Password);
            var response = await _mediator.Send(new LoginRequest(request));

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                Message = ex.Message
            });
        }
    }

}