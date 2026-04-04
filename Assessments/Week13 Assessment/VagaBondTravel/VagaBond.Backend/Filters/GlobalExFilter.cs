using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using VagaBond.Backend.Exceptions;

namespace VagaBond.Backend.Filters
{
    public class GlobalExFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DestinationNotFoundException ex)
            {
                context.Result = new NotFoundObjectResult(new
                {
                    StatusCode = 404,
                    Message = ex.Message
                });
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    StatusCode = 500,
                    Message = "Internal Server Error"
                })
                {
                    StatusCode = 500
                };
            }

            context.ExceptionHandled = true;
        }
        
    }
    [Serializable]
    internal class Invalidrating : Exception
    {
        public Invalidrating()
        {
        }

        public Invalidrating(string? message) : base(message)
        {
        }

        public Invalidrating(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
