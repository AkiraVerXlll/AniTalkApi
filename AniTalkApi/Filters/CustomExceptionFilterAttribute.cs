using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AniTalkApi.Filters;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = 
            context.Exception is ArgumentException ? 400 : 500;

        context.Result = new JsonResult(new
        {
            error = context.Exception.Message
        });
    }
}