using System.Net;
using AuthService.Application.Exceptions;
using Newtonsoft.Json;

namespace AuthService.Api.Middleware;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (EntityNotFoundException entityNotFound)
        {
            await Response(context, HttpStatusCode.NotFound, entityNotFound.Message);
        }
        catch (BadRequestException badRequest)
        {
            await Response(context, HttpStatusCode.BadRequest, badRequest.Message);
        }
        catch (Exception e)
        {
            await Response(context, HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private static async Task Response(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
    }
}