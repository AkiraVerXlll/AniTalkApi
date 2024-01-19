using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.ServiceLayer.AuthServices.SignInServices;

public abstract class BaseSignInService
{
    protected readonly UserManager<User> UserManager;

    protected readonly JwtSettings JwtSettings;

    protected readonly ITokenManagerService TokenManager;

    protected BaseSignInService(
        UserManager<User> userManager,
        IOptions<JwtSettings> options,
        ITokenManagerService tokenManager)
    {
        UserManager = userManager;
        JwtSettings = options.Value;
        TokenManager = tokenManager;
    }

    /// <summary>
    ///  Sign in user and return access and refresh tokens
    /// </summary>
    /// <typeparam name="T">Type of signIn data</typeparam>
    /// <param name="signInData"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<TokenModel> SignInAsync<T>(T signInData)
    {
        var user = await SignInTemplate(signInData);
        var userRoles = await UserManager.GetRolesAsync(user);
        var token = TokenManager.GenerateAccessToken(user, userRoles);

        user.RefreshToken = TokenManager.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now
            .AddDays(JwtSettings.RefreshTokenValidityInDays)
            .ToUniversalTime();

        var result = await UserManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new Exception("Error while updating user data");

        return new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken,
            ExpiresIn = token.ValidTo
        };
    }

    protected abstract Task<User> SignInTemplate<T>(T signInData);
}
