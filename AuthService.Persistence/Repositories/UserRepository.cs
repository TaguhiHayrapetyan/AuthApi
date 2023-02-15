using AuthService.Application.Abstractions.Repositories;
using AuthService.Domain.Entities;
using AuthService.Persistence.DatabaseContext;
using AuthService.Persistence.Repositories.Common;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(AuthDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsExists(string email)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return false;
        return true;
    }

    public async Task<User?> GetAsync(string email)
    {
        return await _dbContext.Set<User>().SingleOrDefaultAsync(u => u.Email == email);
    }
}