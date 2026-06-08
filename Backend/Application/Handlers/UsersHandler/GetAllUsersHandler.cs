using Backend.Application.CommandAndQuery;
using Backend.DTOs.Users;
using Backend.Repositories;
using FluentValidation;
using MediatR;

namespace Backend.Application.Handlers.UsersHandler;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, List<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<GetAllUsersRequest> _validator;
    private readonly ILogger<GetAllUsersHandler> _logger;

    public GetAllUsersHandler(
        IUserRepository userRepository,
        IValidator<GetAllUsersRequest> validator,
        ILogger<GetAllUsersHandler> logger)
    {
        _userRepository = userRepository;
        _validator = validator;
        _logger = logger;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        _logger.LogInformation(
            "Fetching all users.");

        var users = await _userRepository.GetAllAsync();

        var result = new List<UserDto>();

        foreach (var user in users)
        {
            var role = await _userRepository.GetRoleNameByUserIdAsync(user.UserId);

            result.Add(
                new UserDto
                {
                    UserId = user.UserId,
                    Username = user.Username,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    Role = role ?? ""
                });
        }

        _logger.LogInformation(
            "Retrieved {Count} users.",
            result.Count);

        return result;
    }

    private async Task Validate(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating GetAllUsersRequest");

        await _validator.ValidateAndThrowAsync(
            request,
            cancellationToken);
    }
}