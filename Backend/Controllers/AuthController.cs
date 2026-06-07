using Backend.DTOs.Auth;
using Backend.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;

    public AuthController(ILogger<AuthController> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterUserRequest request)
    {
        try
        {
            _logger.LogInformation("Registering user with username: {Username}", request.Username);
            await _userService.RegisterAsync(
                request);

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
    public async Task<IActionResult> Login(
    LoginRequest request)
    {
        try
        {
            _logger.LogInformation("Attempting login for email: {Email} and Password: {Password}", request.Email, request.Password);
            var response =
                await _userService.LoginAsync(
                    request);

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

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var result =
            await _userService
                .GetCurrentUserAsync(User);

        return Ok(result);
    }
}