using AuthService.Api.Extensions;
using AuthService.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.ApplyMigrations();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();