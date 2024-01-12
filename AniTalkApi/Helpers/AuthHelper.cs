using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.CRUD;
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

    private readonly CrudManager _crudManager;

    private readonly IConfiguration _configuration;

    public AuthHelper(
        ITokenManagerService tokenManager,
        UserManager<User> userManager,
        CrudManager crudManager,
        IConfiguration configuration)
    {
        _tokenManager = tokenManager;
        _userManager = userManager;
        _crudManager = crudManager;
        _configuration = configuration;
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

    /// <summary>
    /// Creates user in database by LoginModel data
    /// </summary>
    /// <param name="modelData"></param>
    /// <param name="avatarId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If user is already exist</exception>
    /// <exception cref="Exception">Exception while adding user to the database</exception>
    public async Task CreateModalUserAsync(RegisterModel modelData, int avatarId)
    {
        if (!await IsEmailExistAsync(modelData.Email!))
            throw new ArgumentException("User with this email already exists!");

        var username = modelData.Username!;
        if (!await IsUsernameExistAsync(username) && !username.StartsWith("user-"))
            throw new ArgumentException("User with this username already exists!");

        User user = new()
        {
            Email = modelData.Email,
            UserName = username,
            DateOfRegistration = DateTime.Now,
            IsOAuthRegistered = false,
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

    /// <summary>
    /// Login user by LoginModel data
    /// </summary>
    /// <param name="modelData"></param>
    /// <param name="refreshTokenValidityInDays"></param>
    /// <returns> AccessToken and RefreshToken</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<TokenModel> ModalSignInAsync(
            LoginModel modelData,
            int refreshTokenValidityInDays)
    {

        var user = modelData.Login!.Contains('@')
            ? await _userManager.FindByEmailAsync(modelData.Login)
            : await _userManager.FindByNameAsync(modelData.Login);

        if (user == null || !await _userManager.CheckPasswordAsync(user, modelData.Password!))
            throw new ArgumentException("Bad login data");

        return await SignInAsync(user, refreshTokenValidityInDays);
    }

    /// <summary>
    /// Creates user in database by OAuth data
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="refreshTokenValidityInDays"></param>
    /// <returns>Created user</returns>
    public async Task<User> CreateOAuthUserAsync(
        Dictionary<string, string> claims,
        int refreshTokenValidityInDays)
    {
        var avatarExternalUrl = claims["picture"];
        var avatar = await _crudManager
            .ImageCrud
            .CreateAsync(avatarExternalUrl, _configuration["CloudinarySettings:Paths:Avatar"]!);

        var username = claims["name"];
        if (!await IsUsernameExistAsync(username))
            username = $"user-7200{_userManager.Users.Count()+1}";

        var user = new User
        {
            Email = claims["email"],
            UserName = username,
            PersonalInformation = new PersonalInformation()
            {
                Avatar = avatar,
            },
            DateOfRegistration = DateTime.Now,
            IsOAuthRegistered = true,
            Status = UserStatus.Online,
            EmailConfirmed = true,
        };

        await _userManager.CreateAsync(user);
        return user;
    }

    /// <summary>
    /// Login user by OAuth data
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="refreshTokenValidityInDays"></param>
    /// <returns></returns>
    public async Task<TokenModel> OAuthSignInAsync(
        Dictionary<string, string> claims, 
        int refreshTokenValidityInDays)
    {
        var email = claims["email"];

        User? user;

        if(await IsEmailExistAsync(email))
            user = await CreateOAuthUserAsync(claims, refreshTokenValidityInDays);
        else 
            user = await _userManager.FindByEmailAsync(email);

        return await SignInAsync(user!, refreshTokenValidityInDays);
            
    }

    private async Task<TokenModel> SignInAsync(User user, int refreshTokenValidityInDays)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var token = _tokenManager.GenerateAccessToken(user, userRoles);

        user.RefreshToken = _tokenManager.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.Now
            .AddDays(refreshTokenValidityInDays)
            .ToUniversalTime();

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new Exception("Error while updating user data");

        return new TokenModel
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = user.RefreshToken,
            ExpiresIn = token.ValidTo
        };
    }

    private async Task<bool> IsUsernameExistAsync(string username)
    {
        return await _userManager.FindByNameAsync(username) is null;
    }

    private async Task<bool> IsEmailExistAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email) is null;
    }

}