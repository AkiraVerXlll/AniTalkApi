using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignInServices;

public class ModalSignInService : BaseSignInService
{
    public ModalSignInService(
        UserManager<User> userManager, 
        IOptions<JwtSettings> options, 
        ITokenManagerService tokenManager) : base(userManager, options, tokenManager) { }

    public override async Task<TokenModel> SignInAsync(Dictionary<string, string> claims)
    {
        var login = claims["login"];
        var user = login.Contains('@') ?
            await UserManager.FindByEmailAsync(login) :
            await UserManager.FindByNameAsync(login);

        if (user is null)
            throw new ArgumentException("Bad login data");

        if (!await UserManager.CheckPasswordAsync(user, claims["password"]))
            throw new ArgumentException("Bad login data");

        return await SignInAsyncStrategy(user);
    }
}