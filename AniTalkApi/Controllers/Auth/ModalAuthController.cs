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
        if(!await _authHelper.IsEmailConfirmedAsync(modelData.Login!))
            return BadRequest("Email is not confirmed!");

        var refreshTokenValidityInDays = _jwtSettings.RefreshTokenValidityInDays;
        
        var tokenModel = await _authHelper.ModalSignInAsync(modelData,
             refreshTokenValidityInDays);

        return Ok(tokenModel);
    }

    [HttpPost]
    [Route("send-verification-link")]
    public async Task<IActionResult> SendVerificationLink(string email)
    {
        await _authHelper.SendVerificationLink(email);
        return Ok("Verification link sent successfully!");
    }

    [HttpGet]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmail(string email, string token)
    {
        await _authHelper.VerifyEmailAsync(email, token);
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
