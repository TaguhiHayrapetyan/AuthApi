using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Dtos;
using AuthService.Application.Exceptions;
using AuthService.Application.Validators;
using MediatR;

namespace AuthService.Application.UserCases.Users.Queries;

public record GetUserInfoQuery(int Id) : IRequest<UserDto>;

public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserInfoQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<UserDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        //validate request
        var validator = new GetUserInfoQueryValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if(!validationResult.IsValid)
            ThrowHelper.ThrowBadRequestException(validationResult.Errors.Select(c=>c.ErrorMessage).ToString());

        //try get user
        var user = await _userRepository.GetAsync(request.Id);
        if(user == null)
            ThrowHelper.ThrowNotFoundException($"User with id = {request.Id} not found");
        
        //return user if exists
        return new UserDto
        {
            Name = user.Name,
            Email = user.Email
        };
    }
}