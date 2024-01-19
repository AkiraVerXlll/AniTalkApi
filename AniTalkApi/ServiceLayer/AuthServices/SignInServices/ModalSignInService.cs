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

    protected override async Task<User> SignInTemplate<T>(T signInData)
    {
        if (signInData is not LoginFormModel loginFormModel)
            throw new Exception("Invalid sign in data");

        var login = loginFormModel.Login!;
        var user = login.Contains('@') ?
            await UserManager.FindByEmailAsync(login) :
            await UserManager.FindByNameAsync(login);

        if (user is null)
            throw new ArgumentException("Bad login data");

        if (!await UserManager.CheckPasswordAsync(user, loginFormModel.Password!))
            throw new ArgumentException("Bad login data");

        return user;
    }
}