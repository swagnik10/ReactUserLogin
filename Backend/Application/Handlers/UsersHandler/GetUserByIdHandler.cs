using Backend.Application.CommandAndQuery;
using Backend.DTOs.Users;
using Backend.Repositories;
using FluentValidation;
using MediatR;

namespace Backend.Application.Handlers.UsersHandler;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdRequest, UserDetailsDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetUserByIdRequest> _validator;
    private readonly ILogger<GetUserByIdHandler> _logger;

    public GetUserByIdHandler(IUserRepository userRepository,
                              ILogger<GetUserByIdHandler> logger,
                              IValidator<GetUserByIdRequest> validator)
    {
        _userRepository = userRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<UserDetailsDto> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        _logger.LogInformation("Fetching user with ID: {UserId}", request.UserId);

        var user = await _userRepository.GetByUseIdAsync(request.UserId);

        if (user == null)
        {
            _logger.LogWarning("User not found. UserId: {UserId}", request.UserId);

            throw new Exception("User not found.");
        }

        var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

        return new UserDetailsDto
        {
            UserId = user.UserId,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            Role = role ?? ""
        };
    }
    private async Task Validate(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating GetUserByIdRequest");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

    }
}
