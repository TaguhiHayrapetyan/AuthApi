using System.Reflection;
using AuthService.Application.Abstractions.Services;
using AuthService.Application.Settings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthService.Application.Extensions;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        serviceCollection.AddScoped<IAuthService, Services.AuthService>();
        return serviceCollection;
    }
}