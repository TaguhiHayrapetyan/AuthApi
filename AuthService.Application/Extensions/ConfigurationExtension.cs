using AuthService.Application.Settings;
using Microsoft.Extensions.Configuration;

namespace AuthService.Application.Extensions;

public static class ConfigurationExtension
{
    public static IConfiguration? GetJwtSettings(this IConfiguration configuration)
    {
        return configuration.GetSection(JwtOptions.SectionKey);
    }
}