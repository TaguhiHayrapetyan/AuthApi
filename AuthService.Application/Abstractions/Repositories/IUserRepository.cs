using AuthService.Application.Abstractions.Repositories.Common;
using AuthService.Domain.Entities;

namespace AuthService.Application.Abstractions.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsExists(string email);
    Task<User?> GetAsync(string email);
}

