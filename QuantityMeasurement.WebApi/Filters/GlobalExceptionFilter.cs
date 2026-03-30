using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QuantityMeasurement.Service;
namespace QuantityMeasurement.WebApi.Filters
{
    // UC17: Global exception handler for all REST controllers.
    public class GlobalExceptionFilter : IExceptionFilter
    {
        // Maps exceptions to appropriate HTTP error responses.
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
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 400
                };
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
                context.Result = new ObjectResult(errorResponse)
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }
}
