using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public class ModalSignUpService : BaseSignUpService
{
    private readonly EmailVerificationService _emailVerificationService;

    public ModalSignUpService(
        UserManager<User> userManager,
        IOptions<AvatarSettings> avatarOptions,
        IOptions<JwtSettings> jwtOptions,
        EmailVerificationService emailVerificationService) : base(userManager, avatarOptions, jwtOptions)
    {
        _emailVerificationService = emailVerificationService;
    }

    public override async Task<User> SignUpAsync(Dictionary<string, string> claims)
    {
        var email = claims["email"];
        var username = claims["username"];
        var password = claims["password"];

        if (await UserManager.FindByEmailAsync(email) is not null)
            throw new ArgumentException("User with this email already exists!");

        var normalizedName = UserManager.KeyNormalizer.NormalizeName(username);

        if (await UserManager.FindByNameAsync(username) is not null ||
            normalizedName.StartsWith("USER-"))
            throw new ArgumentException("User with this username already exists!");

        var user = CreateUserStrategy(email, username);

        var result = await UserManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new ArgumentException($"User creation failed! {result.Errors.First().Description}");

        await UserManager.AddToRoleAsync(user, UserRoles.User);
        await _emailVerificationService.SendVerificationLink(user.Email!);
        return user;
    }
}
