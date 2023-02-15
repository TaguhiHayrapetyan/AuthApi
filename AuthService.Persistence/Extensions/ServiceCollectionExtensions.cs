using AuthService.Application.Abstractions.Repositories;
using AuthService.Persistence.DatabaseContext;
using AuthService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AuthDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("AuthDb"));
        });
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        return serviceCollection;
    }
}