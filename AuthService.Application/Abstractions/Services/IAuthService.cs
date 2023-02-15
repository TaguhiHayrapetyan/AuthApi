using AuthService.Application.Models;
using AuthService.Domain.Entities;

namespace AuthService.Application.Abstractions.Services;

public interface IAuthService
{
    public Token GenerateToken(User user);
}