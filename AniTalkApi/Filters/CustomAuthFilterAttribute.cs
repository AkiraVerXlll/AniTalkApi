using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.ServiceLayer.AuthServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AniTalkApi.Filters;

public class CustomAuthFilterAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (token?.Split(' ')[0] != "Bearer")
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        token = token.Split(' ')[1];
        var tokenManager = context.HttpContext.RequestServices.GetService<TokenManagerService>();
        if (tokenManager is null)
            throw new Exception("TokenManagerService is not registered in DI container");
        var name = tokenManager.GetPrincipalFromToken(token)?.Identity?.Name;
        if (name == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        var userManager = context.HttpContext.RequestServices.GetService<UserManager<User>>();
        if (userManager is null)
            throw new Exception("UserManager<User> is not registered in DI container");
        var user = await userManager.FindByNameAsync(name);
        if (user == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        if (!user.EmailConfirmed)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        context.HttpContext.Items["User"] = user;
    }
}
