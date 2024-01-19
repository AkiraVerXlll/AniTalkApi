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





}