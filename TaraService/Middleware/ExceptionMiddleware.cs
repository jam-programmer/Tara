using Application.Common;
using Newtonsoft.Json;
using System.Configuration;
using TaraService.Models.WebService;

namespace TaraService.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
         
            _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
            context.Response.StatusCode = 500; 
            await context.Response.WriteAsync("An internal server error occurred.");
        }
    }

  
}
public static class MyCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}