using AuthService.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Api.Extensions;

public static class WebApplicationBuilder
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            dbContext.Database.Migrate();
        }
    }
}