using FluentValidation;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Application.Validators.AuthValidator;

public class LogInUserValidator : AbstractValidator<LoginRequest>
{
    public LogInUserValidator()
    {
        RuleFor(x => x.LoginBody.Email)
           .NotEmpty()
           .EmailAddress();

        RuleFor(x => x.LoginBody.Password).NotEmpty();
    }

}
