using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Exceptions;
using AuthService.Application.UserCases.Users.Commands;
using AuthService.Application.UserCases.Users.Queries;
using FluentValidation;

namespace AuthService.Application.Validators;

public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
{
    public GetUserInfoQueryValidator()
    {
        RuleFor(m => m.Id)
            .GreaterThan(0)
            .WithMessage("Id can't be less or equal to zero.");
    }
}   