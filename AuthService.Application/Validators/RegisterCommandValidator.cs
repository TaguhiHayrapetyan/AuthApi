using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Exceptions;
using AuthService.Application.UserCases.Users.Commands;
using FluentValidation;

namespace AuthService.Application.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator(IUserRepository userRepository)
    {
        RuleFor(m => m.Email)
            .EmailAddress();

        RuleFor(m => m.Name)
            .MinimumLength(2)
            .MaximumLength(10)
            .WithMessage("Name should be more than 2 letters and less then 10 letters");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
    }
}   