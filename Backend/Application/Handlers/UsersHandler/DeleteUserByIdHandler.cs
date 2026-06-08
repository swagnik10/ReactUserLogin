using Backend.Application.CommandAndQuery;
using Backend.DbConnection;
using Backend.Repositories;
using FluentValidation;
using MediatR;

namespace Backend.Application.Handlers.UsersHandler;
public class DeleteUserByIdHandler : IRequestHandler<DeleteUserRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IValidator<DeleteUserRequest> _validator;
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly ILogger<DeleteUserByIdHandler> _logger;

    public DeleteUserByIdHandler(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        ILogger<DeleteUserByIdHandler> logger,
        IValidator<DeleteUserRequest> validator,
        IUnitOfWorkFactory uowFactory)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _logger = logger;
        _validator = validator;
        _uowFactory = uowFactory;
    }

    public async Task Handle(
        DeleteUserRequest request,
        CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        _logger.LogInformation(
            "Updating user with ID: {UserId}",
            request.UserId);

        using var uow = _uowFactory.Create();

        uow.BeginTransaction();

        try
        {
            var user = await _userRepository.GetByUseIdAsync(request.UserId);

            if (user == null)
            {
                throw new Exception(
                    "User not found.");
            }
            await _userRoleRepository.DeleteByUserIdAsync(request.UserId);
            await _userRepository.DeleteAsync(user);

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    private async Task Validate(
        DeleteUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating DeleteUserRequest");

        await _validator.ValidateAndThrowAsync(
            request,
            cancellationToken);
    }
}