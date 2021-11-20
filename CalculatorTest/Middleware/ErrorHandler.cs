using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CalculatorTest.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ErrorHandler
    {
        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            string sResult = "";
            int responseStatusCode = (int)GetHttpStatusCode(exception);

            sResult = ErrorResponse(exception);
            context.Response.Headers["Content-Encoding"] = "UTF-8";
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = responseStatusCode;

            return context.Response.WriteAsync(sResult);
        }

        private string ErrorResponse(Exception ex)
        {
            var text = ex.Message;
            var result = new Response<string>(null,text);
            return (JsonConvert.SerializeObject(result));
        }

        private HttpStatusCode GetHttpStatusCode(Exception exception)
        {
            if (exception is ArgumentNullException || exception is HttpRequestException || exception is JsonException || exception is NotSupportedException)
                return HttpStatusCode.BadRequest;
            if (exception is UnauthorizedAccessException)
                return HttpStatusCode.Unauthorized;
            return HttpStatusCode.InternalServerError;

        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandler>();
        }
    }
}
