using Backend.Application.CommandAndQuery;
using FluentValidation;

namespace Backend.Application.Validators;

public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
{
    public DeleteUserRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);
    }
}
