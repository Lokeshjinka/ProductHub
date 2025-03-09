using BAL.Constants;

namespace ProductAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            try
            {
                _next(context).Wait();
            }
            catch (Exception ex)
            {
                HandleException(context, ex);
            }
            return Task.CompletedTask;
        }

        private void HandleException(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var errorResponse = new
            {
                message = string.Format(ApiMessages.API021, exception.Message) 
            };
            context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse)).Wait();
        }
    }
}