using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.Helpers;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using Microsoft.Extensions.Options;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("/[controller]")]
public class ModalAuthController : ControllerBase
{
    private readonly JwtSettings _jwtSettings;

    private readonly AvatarSettings _avatarSettings;

    private readonly AuthHelper _authHelper;

    public ModalAuthController(
        IOptions<JwtSettings> options,
        IOptions<AvatarSettings> avatarOptions,
        AuthHelper authHelper)
    {
        _authHelper = authHelper;
        _jwtSettings = options.Value;
        _avatarSettings = avatarOptions.Value;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterModel modelData)
    {
        await _authHelper
            .CreateModalUserAsync
                (modelData, 
                _avatarSettings.DefaultAvatarId, 
                _jwtSettings.RefreshTokenValidityInDays);

        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel modelData)
    {
        var refreshTokenValidityInDays = _jwtSettings.RefreshTokenValidityInDays;
        
        var tokenModel = await _authHelper.ModalSignInAsync(modelData,
             refreshTokenValidityInDays);

        return Ok(tokenModel);
    }

    public async Task<IActionResult> VerifyEmail(string email, string token)
    {
        await _authHelper.SendVerificationLink(email, token);
        return Ok("Email verified successfully!");
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        return Ok(await _authHelper.RefreshTokenAsync(tokenModel));
    }

    [HttpPost]
    [Route("sign-out")]
    public async Task<IActionResult> SignOut(TokenModel? tokenModel)
    {
        
        return Ok("User signed out successfully!");
    }
}
