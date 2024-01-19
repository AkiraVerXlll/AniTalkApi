using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.AuthServices.SignUpServices;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignInServices;

public class OAuthSignInService : BaseSignInService
{
    private readonly OAuthSignUpService _oAuthSignUp;

    public OAuthSignInService(
        UserManager<User> userManager,
        IOptions<JwtSettings> options,
        ITokenManagerService tokenManager, 
        OAuthSignUpService oAuthSignUp) : base(userManager, options, tokenManager)
    {
        _oAuthSignUp = oAuthSignUp;
    }

    public override async Task<TokenModel> SignInAsync(Dictionary<string, string> claims)
    {
        var email = claims["email"];

        var user = await UserManager.FindByEmailAsync(email) ?? 
               await _oAuthSignUp.SignUpAsync(claims);
        
        return await SignInAsyncStrategy(user);
    }
}
