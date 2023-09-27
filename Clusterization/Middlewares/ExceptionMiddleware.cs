using Domain.Exceptions;
using System.Net;

namespace Clusterization.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate _next)
        {
            this._next = _next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpException ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message, ex.StatusCode);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, string msg = "Server Error", HttpStatusCode code = HttpStatusCode.InternalServerError)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                Message = msg,
                StatusCode = context.Response.StatusCode
            }.ToString());
        }
    }
}
