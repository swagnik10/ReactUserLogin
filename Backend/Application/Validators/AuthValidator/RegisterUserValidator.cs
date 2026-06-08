using FluentValidation;
using static Backend.Application.CommandAndQuery.AuthRequests;

namespace Backend.Application.Validators.AuthValidator;

public class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.RegisterUserBody.Email)
           .NotEmpty()
           .EmailAddress();

        RuleFor(x => x.RegisterUserBody.Password).NotEmpty();

        RuleFor(x => x.RegisterUserBody.FirstName).NotEmpty();

        RuleFor(x => x.RegisterUserBody.LastName).NotEmpty();

        RuleFor(x => x.RegisterUserBody.PhoneNumber).NotEmpty();
    }

}
