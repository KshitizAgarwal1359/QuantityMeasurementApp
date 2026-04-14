using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using BusinessLogic.API.Services;
namespace BusinessLogic.API.Filters
{
    // UC21: Global exception handler for BusinessLogic.API controllers.
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is QuantityMeasurementException qmEx)
            {
                var errorResponse = new
                {
                    timestamp = DateTime.UtcNow,
                    status = 400,
                    error = "Quantity Measurement Error",
                    message = qmEx.Message,
                    path = context.HttpContext.Request.Path.Value
                };
                context.Result = new ObjectResult(errorResponse) { StatusCode = 400 };
                context.ExceptionHandled = true;
            }
            else
            {
                var errorResponse = new
                {
                    timestamp = DateTime.UtcNow,
                    status = 500,
                    error = "Internal Server Error",
                    message = context.Exception.Message,
                    path = context.HttpContext.Request.Path.Value
                };
                context.Result = new ObjectResult(errorResponse) { StatusCode = 500 };
                context.ExceptionHandled = true;
            }
        }
    }
}
