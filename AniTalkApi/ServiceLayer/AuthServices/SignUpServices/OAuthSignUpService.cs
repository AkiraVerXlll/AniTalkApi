using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public class OAuthSignUpService : BaseSignUpService
{
    private readonly ImageCrud _imageCrud;

    public OAuthSignUpService(
        UserManager<User> userManager, 
        ImageCrud imageCrud,
        UserCrud userCrud) : base(userManager, userCrud)
    {
        _imageCrud = imageCrud;
    }

    public override async Task<User> SignUpAsync(Dictionary<string, string> claims)
    {
        var avatarExternalUrl = claims["picture"];
        var avatar = await _imageCrud
            .CreateAvatarAsync(avatarExternalUrl);

        var username = claims["name"];
        if (await UserManager.FindByNameAsync(username) is not null)
            username = $"user-7200{UserManager.Users.Count() + 1}";

        var user = UserCrud.CreateUser(claims["email"], username, avatar);
        user.EmailConfirmed = true;

        await UserManager.CreateAsync(user);
        return user;
    }
}
