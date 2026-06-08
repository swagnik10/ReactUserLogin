using Backend.Application.CommandAndQuery;
using Backend.DbConnection;
using Backend.Repositories;
using FluentValidation;
using MediatR;

public class UpdateUserByIdHandler : IRequestHandler<UpdateUserRequest>
{
    private readonly IUserRepository _userRepository;
    private readonly IValidator<UpdateUserRequest> _validator;
    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly ILogger<UpdateUserByIdHandler> _logger;

    public UpdateUserByIdHandler(
        IUserRepository userRepository,
        ILogger<UpdateUserByIdHandler> logger,
        IValidator<UpdateUserRequest> validator,
        IUnitOfWorkFactory uowFactory)
    {
        _userRepository = userRepository;
        _logger = logger;
        _validator = validator;
        _uowFactory = uowFactory;
    }

    public async Task Handle(
        UpdateUserRequest request,
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
                throw new Exception("User not found.");
            }

            user.FirstName = request.UserBody.FirstName;
            user.LastName = request.UserBody.LastName;
            user.Email = request.UserBody.Email;
            user.PhoneNumber = request.UserBody.PhoneNumber;
            user.IsActive = request.UserBody.IsActive;

            await _userRepository.UpdateAsync(user);

            await uow.CommitAsync();
        }
        catch
        {
            await uow.RollbackAsync();
            throw;
        }
    }

    private async Task Validate(
        UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating UpdateUserRequest");

        await _validator.ValidateAndThrowAsync(
            request,
            cancellationToken);
    }
}