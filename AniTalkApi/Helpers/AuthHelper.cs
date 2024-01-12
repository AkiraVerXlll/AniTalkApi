using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.Helpers;

public class AuthHelper
{
    private readonly UserManager<User> _userManager;
    
    private readonly ITokenManagerService _tokenManager;

    public AuthHelper(
        ITokenManagerService tokenManager,
        UserManager<User> userManager)
    {
        _tokenManager = tokenManager;
        _userManager = userManager;
    }

    /// <summary>
    /// Creates user in database by LoginModel data
    /// (used for modal registration)
    /// </summary>
    /// <param name="modelData"></param>
    /// <param name="avatarId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If user is already exist</exception>
    /// <exception cref="Exception">Exception while adding user to the database</exception>
    public async Task CreateUserAsync(RegisterModel modelData, int avatarId)
    {
        if (await _userManager.FindByEmailAsync(modelData.Email!) is not null)
            throw new ArgumentException("User with this email already exists!");

        if (await _userManager.FindByNameAsync(modelData.Username!) is not null)
            throw new ArgumentException("User with this username already exists!");

        User user = new()
        {
            Email = modelData.Email,
            UserName = modelData.Username,
            DateOfRegistration = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = UserStatus.Online,
            PersonalInformation = new PersonalInformation()
            {
                AvatarId = avatarId
            }
        };

        var result = await _userManager.CreateAsync(user, modelData.Password!);
        if (!result.Succeeded)
            throw new Exception("User creation failed! Please check user data and try again!");

        await _userManager.AddToRoleAsync(user, UserRoles.User);
    }

    public async Task<TokenModel> SignInAsync(
        LoginModel modelData, 
        int refreshTokenValidityInDays)
    {

        var user = modelData.Login!.Contains('@')
            ? await _userManager.FindByEmailAsync(modelData.Login)
            : await _userManager.FindByNameAsync(modelData.Login);

        if (user == null || !await _userManager.CheckPasswordAsync(user, modelData.Password!))
            throw new ArgumentException("Bad login data");

        var userRoles = await _userManager.GetRolesAsync(user);
        var token = _tokenManager.GenerateAccessToken(user, userRoles);

        user.RefreshToken = _tokenManager.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now
            .AddDays(refreshTokenValidityInDays)
            .ToUniversalTime();

        var result = await _userManager.UpdateAsync(user);
        if(!result.Succeeded)
            throw new Exception("Error while updating user data");

        return new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken,
            ExpiresIn = token.ValidTo
        };
    }


    public async Task<TokenModel> RefreshTokenAsync(TokenModel? tokenModel)
    {
        if (tokenModel is null)
            throw new ArgumentException("Token model is null");

        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = _tokenManager.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            throw new ArgumentException("Invalid access token or refresh token");

        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new ArgumentException("Invalid access token or refresh token");

        var newAccessToken = _tokenManager.GenerateAccessToken(principal.Claims.ToList());
        var newRefreshToken = _tokenManager.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenModel()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            ExpiresIn = newAccessToken.ValidTo
        };
    }
}