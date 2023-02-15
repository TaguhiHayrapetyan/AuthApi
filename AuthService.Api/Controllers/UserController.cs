using System.Security.Claims;
using AuthService.Application.Dtos;
using AuthService.Application.Models;
using AuthService.Application.UserCases.Users.Commands;
using AuthService.Application.UserCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task Register(RegisterCommand command)
    {
        await _mediator.Send(command);
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<Token> Login(LoginCommand command)
    {
        return await _mediator.Send(command);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<UserDto> GetUserInfo()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        var id = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        var query = new GetUserInfoQuery(Convert.ToInt32(id));
        return await _mediator.Send(query);
    }
}