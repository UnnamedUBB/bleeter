using Bleeter.Shared.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Bleeter.Shared.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidatorException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(e.Errors);
        }
        catch (DomainException e)
        {
            context.Response.StatusCode = (int)e.StatusCode;
            await context.Response.WriteAsync(e.Description);
        }
    }
}