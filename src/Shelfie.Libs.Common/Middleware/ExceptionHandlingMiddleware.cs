using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Shelfie.Api.Users.Middleware;

namespace Shelfie.Libs.Common.Middleware;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception exception)
    {
        var (statusCode, title) = GetExceptionInfo(exception);
        var response = new ExceptionResult
        {
            Title = title,
            Code = statusCode,
            Source = exception.Source,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static (int Code, string Title) GetExceptionInfo(Exception exception) => exception switch
    {
        ApplicationException => (StatusCodes.Status400BadRequest, "Ошибка"),
        ValidationException => (StatusCodes.Status400BadRequest, "Ошибка валидации"),
        _ => (StatusCodes.Status500InternalServerError, "Ошибка сервера")
    };
}
