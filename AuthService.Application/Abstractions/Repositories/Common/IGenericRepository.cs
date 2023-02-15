namespace AuthService.Application.Abstractions.Repositories.Common;

public interface IGenericRepository<T>
{
    Task<T?> GetAsync(long id);
    Task<IEnumerable<T>> GetListAsync();
    Task CreateAsync(T create);
    Task UpdateAsync(T update);
    Task DeleteAsync(long id);
}