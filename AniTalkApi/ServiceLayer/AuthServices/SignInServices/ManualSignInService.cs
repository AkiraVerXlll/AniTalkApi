using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignInServices;

public class ManualSignInService : BaseSignInService
{
    public ManualSignInService(
        UserManager<User> userManager, 
        IOptions<JwtSettings> options, 
        TokenManagerService tokenManager) : base(userManager, options, tokenManager) { }

    public override async Task<TokenModel> SignInAsync(Dictionary<string, string> claims)
    {
        var email = claims["email"];
        var user = await UserManager.FindByEmailAsync(email);

        if (user is null)
            throw new ArgumentException("Bad login data");

        if (!await UserManager.CheckPasswordAsync(user, claims["password"]))
            throw new ArgumentException("Bad login data");

        return await SignInAsyncStrategy(user);
    }
}