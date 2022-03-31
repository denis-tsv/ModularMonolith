using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shop.Framework.UseCases.Interfaces.Exceptions;

namespace Shop.Web.Utils
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException e)//framework exception (common for all modules). also here can be used any exception from any module
            {
                await HandleException(httpContext, e.Message, HttpStatusCode.NotFound);
            }
        }

        private async Task HandleException(HttpContext httpContext, string message, HttpStatusCode code)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            await httpContext.Response.WriteAsync(message);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
