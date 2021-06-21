using Clinic.Core.CustomExceptions;
using Clinic.Infrastructure.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Clinic.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusisnessException)
            {
                var exception = context.Exception as BusisnessException;

                var response = new BadRequestResponse
                {
                    Message = exception.Message,
                };

                context.Result = new BadRequestObjectResult(response);
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else if (context.Exception is NotFoundBusisnessException)
            {

                context.Result = new NotFoundObjectResult(new NotFoundResponse());
                context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
        }
    }
}
