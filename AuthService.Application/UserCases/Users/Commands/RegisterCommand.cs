using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Exceptions;
using AuthService.Application.Services;
using AuthService.Application.Validators;
using AuthService.Domain.Entities;
using MediatR;

namespace AuthService.Application.UserCases.Users.Commands;

public record RegisterCommand(string Email, string Password, string Name) : IRequest<Unit>;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        //validate request
        var validator = new RegisterCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            ThrowHelper.ThrowBadRequestException(validationResult.Errors);
        
        //check if user exists
        if (await _userRepository.IsExists(request.Email))
            ThrowHelper.ThrowBadRequestException("User with the same email already exists");
       
        //hash password
        var passwordHash = Md5HashGenerator.Generate(request.Password);
        
        //create domain object
        var user = new User
        {
            Email = request.Email,
            PasswordHash = passwordHash,
            Name = request.Name
        };
        //save to db
        await _userRepository.CreateAsync(user);
        return Unit.Value;
    }
}