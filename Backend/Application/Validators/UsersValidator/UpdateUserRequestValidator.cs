using Backend.Application.CommandAndQuery;
using FluentValidation;

namespace Backend.Application.Validators.UsersValidator;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.UserBody.FirstName)
            .NotEmpty();

        RuleFor(x => x.UserBody.LastName)
            .NotEmpty();

        RuleFor(x => x.UserBody.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
