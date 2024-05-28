using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.AuthServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AniTalkApi.Controllers.Auth;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly TwoFactorVerificationService _twoFactorVerification;

    private readonly EmailVerificationService _emailVerification;

    private readonly TokenManagerService _tokenManager;

    private readonly ResetPasswordService _resetPassword;

    private readonly CookieSettings _cookieSettings;

    public AuthController(
        TokenManagerService tokenManager, 
        ResetPasswordService resetPassword, 
        EmailVerificationService emailVerification, 
        TwoFactorVerificationService twoFactorVerification,
        IOptions<CookieSettings> cookieSettings)
    {
        _tokenManager = tokenManager;
        _resetPassword = resetPassword;
        _emailVerification = emailVerification;
        _twoFactorVerification = twoFactorVerification;
        _cookieSettings = cookieSettings.Value;
    }
    [HttpPost]
    [Route("send-two-factor-code")]
    public async Task<IActionResult> SendTwoFactorCode()
    {
        var email = HttpContext.Request.Cookies["Email"];
        if (email == null)
            return StatusCode(300, "Redirect to login");
        await _twoFactorVerification.SendCodeAsync(email);
        return Ok("Two factor verification code is sent");
    }

    [HttpPost]
    [Route("two-factor-verification-validate")]
    public async Task<IActionResult> TwoFactorVerificationValidate(string code)
    {
        var email = HttpContext.Request.Cookies["Email"];
        if (email == null)
            return StatusCode(300, "Redirect to login");
        var tokenModel = await _twoFactorVerification.ValidateCodeAsync(email, code);
        return Ok(tokenModel);
    }

    [HttpPost]
    [Route("send-verification-link")]
    public async Task<IActionResult> SendVerificationLink(string email)
    {
        await _emailVerification.SendVerificationLink(email);
        return Ok("Verification link sent successfully!");
    }

    [HttpGet]
    [Route("verify-email")]
    public async Task<IActionResult> VerifyEmail(string email, string token)
    {
        await _emailVerification.VerifyEmailAsync(email, token);
        return Ok("Email verified successfully!");
    }

    [HttpGet]
    [Route("send-reset-password-link")]
    public async Task<IActionResult> ResetPassword(string email)
    {
        await _resetPassword.ResetPasswordAsync(email);
        return Ok("Reset password link sent successfully!");
    }

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(
        string email,
        string token,
        [FromBody] string newPassword)
    {
        await _resetPassword
            .ChangeForgottenPasswordAsync(email, token, newPassword);

        return Ok("Password reset successfully!");
    }

    [HttpPost]
    [Route("change-password")]
    public async Task<IActionResult> ChangePassword(
        string email,
        [FromBody] ChangePasswordFormModel formData)
    {
        await _resetPassword
            .ChangePasswordAsync(email, formData.OldPassword!, formData.NewPassword!);

        return Ok("Password changed successfully!");
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        return Ok(await _tokenManager.RefreshTokenAsync(tokenModel));
    }

    [HttpPost]
    [Route("sign-out")]
    public new async Task<IActionResult> SignOut()
    {
        var username = HttpContext.Items["Username"]?.ToString();
        if(string.IsNullOrEmpty(username))
            throw new ArgumentException("Username is null or empty");
        var accessToken = HttpContext.Request.Cookies[_cookieSettings.AccessToken];
        var refreshToken = HttpContext.Request.Cookies[_cookieSettings.RefreshToken];
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            throw new ArgumentException("Access token or refresh token is null or empty");
        var tokenModel = new TokenModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        await _tokenManager.SignOutAsync(tokenModel, username);
        HttpContext.Response.Cookies.Delete(_cookieSettings.AccessToken);
        HttpContext.Response.Cookies.Delete(_cookieSettings.RefreshToken);
        return Ok();
    }
}
