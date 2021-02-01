using System;
using Company.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.WebApi.Filters
{
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is RequestedResourceHasConflictException)
            {
                context.Result = new ObjectResult("Resource Has Conflict")
                {
                    StatusCode = 409,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is RequestedResourceNotFoundException)
            {
                context.Result = new ObjectResult("Resource Not Found")
                {
                    StatusCode = 404
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is Exception)
            {
                context.Result = new ObjectResult("Internal Server Error")
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
