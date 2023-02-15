using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.Exceptions;
using AuthService.Application.Models;
using AuthService.Application.Services;
using AuthService.Application.Validators;
using MediatR;

namespace AuthService.Application.UserCases.Users.Commands;

public record LoginCommand(string UserName, string Password) : IRequest<Token>;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Token>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IUserRepository userRepository, IAuthService authService)
    {
        _userRepository = userRepository;
        _authService = authService;
    }
    public async Task<Token> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        //validate request
        var validator = new LoginCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            ThrowHelper.ThrowBadRequestException(validationResult.Errors);

        //check if user with username and password exist,
        var user = await _userRepository.GetAsync(request.UserName);
        if (user == null)
            ThrowHelper.ThrowNotFoundException($"User with {request.UserName} not found.");
            
        if (user.PasswordHash != Md5HashGenerator.Generate(request.Password))
            ThrowHelper.ThrowBadRequestException("Incorrect password.");

        //generate token
        var token = _authService.GenerateToken(user);
        return token;
    }
}