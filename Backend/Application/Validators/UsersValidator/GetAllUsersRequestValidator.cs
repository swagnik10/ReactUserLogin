using Backend.Application.CommandAndQuery;
using FluentValidation;

namespace Backend.Application.Validators.UsersValidator;

public class GetAllUsersRequestValidator : AbstractValidator<GetAllUsersRequest>
{
    public GetAllUsersRequestValidator()
    {
        // No validation currently required.
    }
}
