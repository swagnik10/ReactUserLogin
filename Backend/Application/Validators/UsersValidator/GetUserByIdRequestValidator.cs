using Backend.Application.CommandAndQuery;
using FluentValidation;

namespace Backend.Application.Validators.UsersValidator;

public class GetUserByIdRequestValidator : AbstractValidator<GetUserByIdRequest>
{
    public GetUserByIdRequestValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage(
                "User ID must be greater than zero.");
    }
}
