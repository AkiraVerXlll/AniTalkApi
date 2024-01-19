using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public class ModalSignUpService : BaseSignUpService
{
    public ModalSignUpService(
        UserManager<User> userManager, 
        IOptions<AvatarSettings> avatarOptions, 
        IOptions<JwtSettings> jwtOptions) : base(userManager, avatarOptions, jwtOptions) {}

    public override async Task<User> SignUpAsync<T>(T signUpData)
    {
        if (signUpData is not SignUpFormModel modelData)
            throw new ArgumentException("Invalid sign up data type!");

        if (await UserManager.FindByEmailAsync(modelData.Email!) is not null)
            throw new ArgumentException("User with this email already exists!");

        var username = modelData.Username!;
        var normalizedName = UserManager.KeyNormalizer.NormalizeName(username);

        if (await UserManager.FindByNameAsync(modelData.Username!) is not null || 
            normalizedName.StartsWith("USER-"))
            throw new ArgumentException("User with this username already exists!");

        var user = CreateUserStrategy(modelData.Email!, modelData.Username!);

        var result = await UserManager.CreateAsync(user, modelData.Password!);
        if (!result.Succeeded)
            throw new ArgumentException($"User creation failed! {result.Errors.First().Description}");

        await UserManager.AddToRoleAsync(user, UserRoles.User);
        await SendVerificationLink(user.Email!);
        return user;
    }
}
