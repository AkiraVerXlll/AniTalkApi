using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.EmailServices;
using AniTalkApi.ServiceLayer.TokenManagerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.Helpers;

public class AuthHelper
{
    private readonly UserManager<User> _userManager;
    
    private readonly ITokenManagerService _tokenManager;

    private readonly ImageCrud _imageCrud;

    private readonly CloudinarySettings _cloudinarySettings;

    private readonly ModalAuthSettings _modalAuthSettings;

    private readonly IEmailSenderService _emailSenderService;

    public AuthHelper(
        ITokenManagerService tokenManager,
        UserManager<User> userManager,
        ImageCrud imageCrud,
        IEmailSenderService emailSenderService,
        IOptions<CloudinarySettings> cloudinaryOptions,
        IOptions<ModalAuthSettings> modalAuthOptions)
    {
        _cloudinarySettings = cloudinaryOptions.Value;
        _modalAuthSettings = modalAuthOptions.Value;
        _emailSenderService = emailSenderService;
        _tokenManager = tokenManager;
        _userManager = userManager;
        _imageCrud = imageCrud;
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
    /// <param name="refreshTokenValidityInDays"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">If user is already exist</exception>
    /// <exception cref="Exception">Exception while adding user to the database</exception>
    public async Task CreateModalUserAsync(
        RegisterModel modelData, 
        int avatarId,
        int refreshTokenValidityInDays)
    {
        if (!await IsEmailExistAsync(modelData.Email!))
            throw new ArgumentException("User with this email already exists!");

        var username = modelData.Username!;
        var normalizedName = _userManager.KeyNormalizer.NormalizeName(username);

        if (!await IsUsernameExistAsync(username) || normalizedName.StartsWith("USER-"))
            throw new ArgumentException("User with this username already exists!");

        User user = new()
        {
            Email = modelData.Email,
            UserName = username,
            DateOfRegistration = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = UserStatus.Online,
            PersonalInformation = new PersonalInformation()
            {
                AvatarId = avatarId
            },
            RefreshTokenExpiryTime = 
                DateTime.Now.AddDays(refreshTokenValidityInDays).ToUniversalTime()
        };

        var result = await _userManager.CreateAsync(user, modelData.Password!);
        if (!result.Succeeded)
            throw new ArgumentException($"User creation failed! {result.Errors.First().Description}");

        await _userManager.AddToRoleAsync(user, UserRoles.User);
        await SendVerificationLink(user.Email!);
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
        var avatar = await _imageCrud
            .CreateAsync(avatarExternalUrl, _cloudinarySettings.Paths!.Avatar!);

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
            Status = UserStatus.Online,
            EmailConfirmed = true,
            RefreshTokenExpiryTime = 
                DateTime.Now.AddDays(refreshTokenValidityInDays).ToUniversalTime()
        };

        await _userManager.CreateAsync(user);
        return user;
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

        var user = await GetUserAsync(modelData.Login!);

        if (!await _userManager.CheckPasswordAsync(user, modelData.Password!))
            throw new ArgumentException("Bad login data");

        return await SignInAsync(user, refreshTokenValidityInDays, false);
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

        return await SignInAsync(user!, refreshTokenValidityInDays, true);
            
    }

    /// <summary>
    /// Logout user
    /// </summary>
    /// <param name="tokenModel"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<TokenModel> SignOutAsync(TokenModel? tokenModel)
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

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.Now;
        await _userManager.UpdateAsync(user);

        return new TokenModel()
        {
            AccessToken = null,
            RefreshToken = null,
            ExpiresIn = DateTime.Now
        };
    }

    /// <summary>
    /// Send verification link to user email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task SendVerificationLink(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user!);
        token = Convert.ToBase64String(Encoding.UTF8.GetBytes(token));
        var confirmationLink = $"{_modalAuthSettings.EmailConfirmationLink}?email={email}&token={token}";

        await _emailSenderService.SendEmailVerificationLinkAsync(email, confirmationLink);
    }

    /// <summary>
    /// Verify user email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task VerifyEmailAsync(string email, string token)
    {
        token = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
            throw new ArgumentException("User not found");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded)
            throw new ArgumentException("Email confirmation failed");
    }

    /// <summary>
    /// Check if user email is confirmed
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public async Task<bool> IsEmailConfirmedAsync(string login)
    {
        var user = await GetUserAsync(login);
        return user.EmailConfirmed;
    }

    private async Task<TokenModel> SignInAsync( 
        User user, 
        int refreshTokenValidityInDays, 
        bool isOAuthSignIn)
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

    private async Task<User> GetUserAsync(string login)
    {
        var user = login.Contains('@') ? 
            await _userManager.FindByEmailAsync(login) :
            await _userManager.FindByNameAsync(login);

        return user ?? throw new ArgumentException("User not found"); ;
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