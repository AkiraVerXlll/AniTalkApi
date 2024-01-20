using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public class ManualSignUpService : BaseSignUpService
{
    private readonly EmailVerificationService _emailVerificationService;

    public ManualSignUpService(
        UserManager<User> userManager,
        EmailVerificationService emailVerificationService,
        UserCrud userCrud) : base(userManager, userCrud)
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

        var user = UserCrud.CreateUser(email, username);

        var result = await UserManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new ArgumentException($"User creation failed! {result.Errors.First().Description}");

        await UserManager.AddToRoleAsync(user, UserRoles.User);
        await _emailVerificationService.SendVerificationLink(user.Email!);
        return user;
    }
}
