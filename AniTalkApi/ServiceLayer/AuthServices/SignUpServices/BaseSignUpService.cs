using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignUpServices;

public abstract class BaseSignUpService
{
    protected readonly UserManager<User> UserManager;

    private readonly AvatarSettings _avatarSettings;

    private readonly JwtSettings _jwtSettings;

    protected BaseSignUpService(
        UserManager<User> userManager,
        IOptions<AvatarSettings> avatarOptions,
        IOptions<JwtSettings> jwtOptions)
    {
        UserManager = userManager;
        _avatarSettings = avatarOptions.Value;
        _jwtSettings = jwtOptions.Value;
    }

    public abstract Task<User> SignUpAsync<T>(T signUpData);

    protected User CreateUserStrategy(string email, string username)
    {
        var user = new User()
        {
            Email = email,
            UserName = username,
            DateOfRegistration = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = UserStatus.Online,
            PersonalInformation = new PersonalInformation()
            {
                AvatarId = _avatarSettings.DefaultAvatarId
            },
            RefreshTokenExpiryTime =
                DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays).ToUniversalTime()
        };
        return user;
    }
}
