using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;

namespace AniTalkApi.CRUD;

public class UserCrud
{
    private readonly AvatarSettings _avatarSettings;

    private readonly JwtSettings _jwtSettings;

    public UserCrud(
        IOptions<AvatarSettings> avatarOptions,
        IOptions<JwtSettings> jwtOptions)
    {
        _avatarSettings = avatarOptions.Value;
        _jwtSettings = jwtOptions.Value;
    }

    public User CreateUser(
        string email,
        string username,
        string? avatarUrl = null)
    {
        var personalInformation = avatarUrl is null
            ? new PersonalInformation()
            {
                AvatarUrl = _avatarSettings.DefaultAvatarUrl,
            }
            : new PersonalInformation()
            {
                AvatarUrl = avatarUrl,
            };
        var user = new User()
        {
            Email = email,
            UserName = username,
            DateOfRegistration = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = UserStatus.Online,
            PersonalInformation = personalInformation,
            RefreshTokenExpiryTime =
                DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays).ToUniversalTime()
        };

        return user;
    }
}