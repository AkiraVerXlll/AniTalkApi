using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.Helpers;
using AniTalkApi.DataLayer.Models.Auth;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("/[controller]")]
public class ModalAuthController : ControllerBase
{
    private readonly AuthHelper _authHelper;

    public ModalAuthController(
        AuthHelper authHelper)
    {
        _authHelper = authHelper;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterModel modelData)
    {
        await _authHelper
            .CreateModalUserAsync(modelData);

        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel modelData)
    {
        var user = await _authHelper.GetUserByLoginAsync(modelData.Login!);

        if(!user.EmailConfirmed)
            return BadRequest("Email is not confirmed!");

        if (await _authHelper.IsTwoFactorEnabledAsync(user.Email!))
        {
            HttpContext.Response.Cookies.Append("Email", user.Email!);
            return StatusCode(300, "Redirect to two-factor authentication");
        }
        
        var tokenModel = await _authHelper.ModalSignInAsync(modelData);
        
        return Ok(tokenModel);
    }

    [HttpPost]
    [Route("send-two-factor-code")]
    public async Task<IActionResult> SendTwoFactorCode()
    {
        var email = HttpContext.Request.Cookies["Email"];
        if (email == null)
            return StatusCode(300, "Redirect to login");
        await _authHelper.SendTwoFactorCodeAsync(email);
        return Ok("Two factor verification code is sent");
    }

    [HttpPost]
    [Route("two-factor-verification-validate")]
    public async Task<IActionResult> TwoFactorVerificationValidate(string code)
    {
        var email = HttpContext.Request.Cookies["Email"];
        if (email == null)
            return StatusCode(300, "Redirect to login");
        var tokenModel = await _authHelper.TwoFactorVerificationValidateAsync(email, code);
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
        await _authHelper.SignOutAsync(tokenModel);
        return Ok("User signed out successfully!");
    }
}
