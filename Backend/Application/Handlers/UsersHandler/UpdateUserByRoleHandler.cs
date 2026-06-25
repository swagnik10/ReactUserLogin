using Backend.Application.CommandAndQuery;
using Backend.DbConnection;
using Backend.Domain;
using Backend.Repositories;
using FluentValidation;
using MediatR;

namespace Backend.Application.Handlers.UsersHandler;

public class UpdateUserByRoleHandler : IRequestHandler<UpdateRoleRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IValidator<UpdateRoleRequest> _validator;
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly ILogger<UpdateUserByRoleHandler> _logger;

    public UpdateUserByRoleHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserRoleRepository userRoleRepository,
        ILogger<UpdateUserByRoleHandler> logger,
        IValidator<UpdateRoleRequest> validator,
        IUnitOfWorkFactory uowFactory)
    {
        _userRepository = userRepository;
        _logger = logger;
        _validator = validator;
        _uowFactory = uowFactory;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task Handle(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        _logger.LogInformation(
            "Updating user with ID: {UserId}",
            request.UserId);

        using var uow = _uowFactory.Create();

        var ownsTransaction = !uow.HasActiveTransaction();

        if (ownsTransaction)
        {
            uow.BeginTransaction();
        }

        try
        {
            var user = await _userRepository.GetByUseIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(
                    "User not found.");
            }

            var role = await _roleRepository.GetByNameAsync(request.RoleBody.RoleName);

            if (role == null)
            {
                throw new Exception(
                    "Role not found.");
            }

            var userRole = await _userRoleRepository.GetByUserIdAsync(request.UserId);

            if (userRole == null)
            {
                throw new Exception(
                    "User role not found.");
            }

            await _userRoleRepository.DeleteAsync(userRole);

            await _userRoleRepository.SaveAsync(
                new UserRole
                {
                    UserId = request.UserId,
                    RoleId = role.RoleId
                });

            if (ownsTransaction)
            {
                await uow.CommitAsync();
            }
        }
        catch
        {
            if(ownsTransaction)
            {
                await uow.RollbackAsync();
            }
            
            throw;
        }
    }

    private async Task Validate(UpdateRoleRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating UpdateRoleRequest");

        await _validator.ValidateAndThrowAsync(
            request,
            cancellationToken);
    }
}