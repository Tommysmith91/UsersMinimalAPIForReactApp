using FluentValidation;
using Microsoft.IdentityModel.Tokens;
using UsersMinimalAPI.Entities;

namespace UsersMinimalAPI
{
    public class UserValidator : AbstractValidator<UsersDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email).EmailAddress().WithMessage("Invalid E-mail Address Format").NotEmpty().WithMessage("E-mail Address Is Required");
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password Is Required")
                .MinimumLength(8).WithMessage("Password Must Be More Than 8 Characters Long")
                .Matches("[0-9]").WithMessage("Password Must Contain At Least 1 Digit");
            RuleFor(user => user.CompanyName)
                .NotEmpty().WithMessage("Company Name Required");

        }
    }
}
