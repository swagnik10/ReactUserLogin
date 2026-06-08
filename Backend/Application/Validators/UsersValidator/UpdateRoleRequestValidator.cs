using Backend.Application.CommandAndQuery;
using FluentValidation;

namespace Backend.Application.Validators.UsersValidator;

public class UpdateUserRoleValidator : AbstractValidator<UpdateRoleRequest>
{
    public UpdateUserRoleValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0);

        RuleFor(x => x.RoleBody.RoleName)
            .NotEmpty();

    }
}
