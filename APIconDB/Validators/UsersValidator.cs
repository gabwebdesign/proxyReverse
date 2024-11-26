using APIconDB.Entities;
using FluentValidation;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;

namespace APIconDB.Validators;

public class UsersValidator: AbstractValidator<Users>
{
    public UsersValidator()
    {
        RuleFor(users => users.FirstName).NotEmpty().NotNull().WithName("Name");
        RuleFor(users => users.LastName).NotEmpty().NotNull().WithName("Name");
        RuleFor(users => users.Password).NotEmpty().NotNull().GreaterThan("10");
        RuleFor(users => users.Email).NotEmpty().EmailAddress();
    }
}
