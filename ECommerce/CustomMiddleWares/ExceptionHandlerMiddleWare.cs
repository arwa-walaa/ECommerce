using Microsoft.AspNetCore.Mvc;

namespace ECommerce.CustomMiddleWares
{
    public class ExceptionHandlerMiddleWare
    {
        private readonly RequestDelegate _Next;
        private readonly ILogger<ExceptionHandlerMiddleWare> _logger;

        public ExceptionHandlerMiddleWare(RequestDelegate Next, ILogger<ExceptionHandlerMiddleWare> logger) {
            _Next = Next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _Next.Invoke(context);
                //404 not found handling
                await HandleNotFoundException(context);
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "Something Went wrong");
              
                var Problem = new ProblemDetails()
                {
                    Title = "Internal Server Error",
                  
                    Detail = ex.Message,
                    Instance = context.Request.Path,
                    Status = ex switch
                    {
                        KeyNotFoundException => StatusCodes.Status404NotFound,
                      
                        _ => StatusCodes.Status500InternalServerError
                    }
                };
                context.Response.StatusCode = Problem.Status.Value;
                await context.Response.WriteAsJsonAsync(Problem);


            }
        }

        private static async Task HandleNotFoundException(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                var Problem = new ProblemDetails()
                {
                    Title = "Resource Not Found",
                    Status = StatusCodes.Status404NotFound,
                    Detail = $"The requested resource {context.Request.Path} was not found on this server.",
                    Instance = context.Request.Path
                };
                await context.Response.WriteAsJsonAsync(Problem);
            }
        }
    }
}
