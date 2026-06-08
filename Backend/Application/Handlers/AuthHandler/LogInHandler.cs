using Backend.DTOs.Auth;
using Backend.Repositories;
using Backend.Secutity;
using FluentValidation;
using MediatR;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Application.Handlers.AuthHandler;

public class LogInHandler : IRequestHandler<LoginRequest, LoginDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<LoginRequest> _validator;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ILogger<LogInHandler> _logger;

    public LogInHandler(IUserRepository userRepository,
                              ILogger<LogInHandler> logger,
                              IValidator<LoginRequest> validator,
                              IJwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _logger = logger;
        _validator = validator;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginDto> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        _logger.LogInformation("Logging user with Email: {UserId}", request.LoginBody.Email);

        var user = await _userRepository.GetByEmailAsync(request.LoginBody.Email);

        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        if (user.Password != request.LoginBody.Password)
        {
            throw new Exception("Invalid credentials.");
        }

        if (!user.IsActive)
        {
            throw new Exception("User is inactive.");
        }

        var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

        var token = _jwtTokenService.GenerateToken(
                                    user.UserId,
                                    user.Username,
                                    role ?? "User");

        return new LoginDto
        {
            Token = token,
            UserId = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Role = role ?? "User"
        };
    }
    private async Task Validate(LoginRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating LoginRequest");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

    }
}
