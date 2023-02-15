using System.Text;
using AuthService.Application.Extensions;
using AuthService.Application.Settings;
using AuthService.Persistence.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.Api.Extensions;

public static class ServiceCollectionsExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection serviceCollection, 
        IConfiguration configuration)
    {
        var jwtSection = configuration.GetJwtSettings();
     
        serviceCollection.Configure<JwtOptions>(jwtSection);
        serviceCollection.AddControllers();
        serviceCollection.AddEndpointsApiExplorer();
        serviceCollection.AddSwaggerGen();
        var jwt = new JwtOptions();
        jwtSection.Bind(jwt);
        serviceCollection
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key))
            };
        });
        serviceCollection.AddApplicationServices(configuration);
        serviceCollection.AddPersistenceServices(configuration);
        return serviceCollection;
    }
}