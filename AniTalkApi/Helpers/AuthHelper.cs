using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.Helpers;

public class AuthHelper
{
    private readonly UserManager<User> _userManager;
    
    private readonly ITokenManagerService _tokenManager;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    public AuthHelper(
        ITokenManagerService tokenManager,
        UserManager<User> userManager,
        ICryptoGeneratorService cryptoGenerator)
    {
        _tokenManager = tokenManager;
        _userManager = userManager;
        _cryptoGenerator = cryptoGenerator;
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
        int refreshTokenValidityInDays,
        int refreshTokenLength)
    {

        var user = modelData.Login!.Contains('@')
            ? await _userManager.FindByEmailAsync(modelData.Login)
            : await _userManager.FindByNameAsync(modelData.Login);

        if (user == null || !await _userManager.CheckPasswordAsync(user, modelData.Password!))
            throw new ArgumentException("Bad login data");

        var userRoles = await _userManager.GetRolesAsync(user);
        var token = _tokenManager.GenerateAccessToken(user, userRoles);

        user.RefreshToken = GenerateRefreshToken(refreshTokenLength);
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

    private string GenerateRefreshToken(int tokenLength) => _cryptoGenerator
        .GenerateRandomString(tokenLength);
}