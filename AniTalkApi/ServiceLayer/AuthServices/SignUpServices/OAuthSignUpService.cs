using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public class OAuthSignUpService : BaseSignUpService
{
    private readonly ImageCrud _imageCrud;

    public OAuthSignUpService(
        UserManager<User> userManager, 
        IOptions<AvatarSettings> avatarOptions, 
        IOptions<JwtSettings> jwtOptions,
        ImageCrud imageCrud) : base(userManager, avatarOptions, jwtOptions)
    {
        _imageCrud = imageCrud;
    }

    public override async Task<User> SignUpAsync<T>(T signUpData)
    {
        if(signUpData is not Dictionary<string, string> claims)
            throw new ArgumentException("Invalid sign up data type!");

        var avatarExternalUrl = claims["picture"];
        var avatar = await _imageCrud
            .CreateAvatarAsync(avatarExternalUrl);

        var username = claims["name"];
        if (await UserManager.FindByNameAsync(username) is not null)
            username = $"user-7200{UserManager.Users.Count() + 1}";

        var user = CreateUserStrategy(claims["email"], username, avatar);
        user.EmailConfirmed = true;

        await UserManager.CreateAsync(user);
        return user;
    }
}
