﻿using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shop.Utils.Exceptions;

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
            catch (EntityNotFoundException e)
            {
                await HandleException(httpContext, e);
            }
        }

        private async Task HandleException(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            switch (exception)
            {
                case EntityNotFoundException notFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            await httpContext.Response.WriteAsync(result);
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
