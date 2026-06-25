using AutoMapper;
using Backend.DbConnection;
using Backend.Domain;
using Backend.DTOs.Auth;
using Backend.Repositories;
using Backend.Secutity;
using FluentValidation;
using MediatR;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Application.Handlers.AuthHandler;

public class RegisterHandler : IRequestHandler<RegisterUserRequest, CreatedUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<RegisterUserRequest> _validator;
    private readonly ILogger<RegisterHandler> _logger;
    private readonly IRoleRepository _roleRepository;
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IMapper _mapper;
    private readonly IUserRoleRepository _userRoleRepository;

    public RegisterHandler(IUserRepository userRepository,
                              ILogger<RegisterHandler> logger,
                              IValidator<RegisterUserRequest> validator,
                              IRoleRepository roleRepository,
                              IUnitOfWorkFactory uowFactory,
                              IMapper mapper,
                              IUserRoleRepository userRoleRepository)
    {
        _userRepository = userRepository;
        _logger = logger;
        _validator = validator;
        _roleRepository = roleRepository;
        _uowFactory = uowFactory;
        _mapper = mapper;
        _userRoleRepository = userRoleRepository;
    }

    public async Task<CreatedUserDto> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        using var uow = _uowFactory.Create();

        var ownsTransaction = !uow.HasActiveTransaction();

        if (ownsTransaction)
        {
            uow.BeginTransaction();
        }

        try
        {
            var existingUsername = await _userRepository.GetByUsernameAsync(request.RegisterUserBody.Username);

            if (existingUsername != null)
            {
                throw new Exception("Username already exists.");
            }

            var existingEmail = await _userRepository.GetByEmailAsync(request.RegisterUserBody.Email);

            if (existingEmail != null)
            {
                throw new Exception("Email already exists.");
            }

            var user = _mapper.Map<User>(request.RegisterUserBody);

            user.IsActive = true;
            user.CreatedAt = DateTime.Now;

            await _userRepository.SaveAsync(user);

            var userRole = await _roleRepository.GetByNameAsync("User");

            if (userRole == null)
            {
                throw new Exception(
                    "Default role not found.");
            }

            await _userRoleRepository.SaveAsync(
                new UserRole
                {
                    UserId = user.UserId,
                    RoleId = userRole.RoleId
                });

            if (ownsTransaction)
            {
                await uow.CommitAsync();
            }

            return new CreatedUserDto
            {
                UserId = user.UserId,
                Username = user.Username
            };
        }
        catch
        {
            if(ownsTransaction)
            {
                await uow.RollbackAsync();
            }
            throw new InvalidOperationException("User creation failed.");
        }
    }
    private async Task Validate(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating LoginRequest");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

    }
}
