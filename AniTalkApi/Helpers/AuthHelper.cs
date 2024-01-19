using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AniTalkApi.CRUD;
using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.DbModels.Enums;
using AniTalkApi.DataLayer.Models.Auth;
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

    private readonly IEmailSenderService _emailSenderService;
    
    private readonly CloudinarySettings _cloudinarySettings;

    private readonly ModalAuthSettings _modalAuthSettings;

    private readonly JwtSettings _jwtSettings;

    private readonly AvatarSettings _avatarSettings;

    private readonly SendGridSettings _sendGridSettings;

    public AuthHelper(
        ITokenManagerService tokenManager,
        UserManager<User> userManager,
        ImageCrud imageCrud,
        IEmailSenderService emailSenderService,
        IOptions<CloudinarySettings> cloudinaryOptions,
        IOptions<ModalAuthSettings> modalAuthOptions,
        IOptions<JwtSettings> jwtOptions,
        IOptions<AvatarSettings> avatarOptions,
        IOptions<SendGridSettings> sendGridOptions)
    {
        _cloudinarySettings = cloudinaryOptions.Value;
        _modalAuthSettings = modalAuthOptions.Value;
        _jwtSettings = jwtOptions.Value;
        _avatarSettings = avatarOptions.Value;
        _sendGridSettings = sendGridOptions.Value;
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
    /// Creates user in database by OAuth data
    /// </summary>
    /// <param name="claims"></param>
    /// <returns>Created user</returns>
    public async Task<User> CreateOAuthUserAsync(Dictionary<string, string> claims)
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
                DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays).ToUniversalTime()
        };

        await _userManager.CreateAsync(user);
        return user;
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

        await _emailSenderService.SendTemplateEmailAsync(
            email,
            _sendGridSettings.EmailTemplates!.EmailConfirmation!,
            new { Link = confirmationLink });
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
    /// Send two factor verification code to user email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task SendTwoFactorCodeAsync(string email)
    {
        var user = await GetUserByLoginAsync(email);
        var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
        await _emailSenderService.SendTemplateEmailAsync(
            user.Email!, 
            _sendGridSettings.EmailTemplates!.TwoFactorVerification!, 
            new {Code = token});
    }

    /// <summary>
    /// Validate two factor verification code
    /// </summary>
    /// <param name="email"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<TokenModel> TwoFactorVerificationValidateAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.VerifyTwoFactorTokenAsync(user!, "Email", code);
        if (!result)
            throw new ArgumentException("Invalid two factor code");

        return await SignInAsync(user!);
    }

    public async Task<bool> IsTwoFactorEnabledAsync(string email)
    {
        var user = await GetUserByLoginAsync(email);
        return await _userManager.GetTwoFactorEnabledAsync(user);
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