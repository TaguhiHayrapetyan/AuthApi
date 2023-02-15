using AuthService.Application.Abstractions.Repositories;
using AuthService.Application.Abstractions.Repositories.Common;
using AuthService.Domain.Entities.Common;
using AuthService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Persistence.Repositories.Common;

public class GenericRepository<T> : IGenericRepository<T> where T: Entity
{
    protected readonly AuthDbContext _dbContext;

    protected GenericRepository(AuthDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<T?> GetAsync(long id)
    {
        return await _dbContext
            .Set<T>()
            .AsNoTracking()
            .FirstAsync(e=>e.Id==id);
    }

    public async Task<IEnumerable<T>> GetListAsync()
    {
        return await _dbContext
            .Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateAsync(T create)
    {
        await _dbContext
            .Set<T>()
            .AddAsync(create);
        await _dbContext
            .SaveChangesAsync();
    }

    public async Task UpdateAsync(T update)
    {
        var entry = _dbContext.Entry(update);
        entry.State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entry = await _dbContext.Set<T>().FindAsync(id);
        _dbContext.Remove(entry);
        await _dbContext.SaveChangesAsync();
    }
}