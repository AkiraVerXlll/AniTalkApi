using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignInServices;

public class OAuthSignService : BaseSignInService
{
    public OAuthSignService(
        UserManager<User> userManager, 
        IOptions<JwtSettings> options, 
        ITokenManagerService tokenManager) : base(userManager, options, tokenManager) { }

    protected override async Task<User> SignInTemplate<T>(T signInData)
    {
        if (signInData is not Dictionary<string, string> claims)
            throw new Exception("Invalid sign in data");

        var email = claims["email"];

        return await UserManager.FindByEmailAsync(email) ?? 
               await CreateOAuthUserAsync(claims);
    }
}
