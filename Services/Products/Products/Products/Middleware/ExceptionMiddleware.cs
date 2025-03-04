using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Net;
using System.Text.Json;

namespace Products.Middleware
{
    public class ExceptionMiddleware( IHostEnvironment env,  RequestDelegate next)
    {
        public async Task InvokeAsync( HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch( Exception ex )
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private Task HandleExceptionAsync( HttpContext context , Exception ex, IHostEnvironment env)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiResponseError(context.Response.StatusCode, ex.Message, ex.StackTrace);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var result = JsonSerializer.Serialize(response, options);

            return context.Response.WriteAsync(result);
        }
    }
}
