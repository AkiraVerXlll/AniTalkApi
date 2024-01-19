using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.Helpers;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.ServiceLayer.AuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.AuthServices.SignUpServices;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("/[controller]")]
public class ModalAuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    private readonly ModalSignInService _modalSignIn;

    private readonly ModalSignUpService _modalSignUp;

    private readonly TwoFactorVerificationService _twoFactorVerification;

    private readonly EmailVerificationService _emailVerification;

    private readonly TokenManagerService _tokenManager;

    public ModalAuthController(
        ModalSignInService modalSignIn,
        ModalSignUpService modalSignUp,
        UserManager<User> userManager,
        EmailVerificationService emailVerification,
        TwoFactorVerificationService twoFactorVerification,
        TokenManagerService tokenManager)
    {
        _modalSignIn = modalSignIn;
        _modalSignUp = modalSignUp;
        _userManager = userManager;
        _emailVerification = emailVerification;
        _twoFactorVerification = twoFactorVerification;
        _tokenManager = tokenManager;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpFormModel modelData)
    {
        var claims = new Dictionary<string, string>
        {
            { "email", modelData.Email! },
            { "username", modelData.Username! },
            { "password", modelData.Password! }
        };
        await _modalSignUp.SignUpAsync(claims);
        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginFormModel formModelData)
    {
        var login = formModelData.Login!;
        var user = login.Contains('@') ? await _userManager.FindByEmailAsync(login) : 
            await _userManager.FindByNameAsync(login);

        if (user is null)
            return BadRequest("User not found!");

        if(!user.EmailConfirmed)
            return BadRequest("Email is not confirmed!");

        if (await _twoFactorVerification.IsEnabledAsync(user.Email!))
        {
            HttpContext.Response.Cookies.Append("Email", user.Email!);
            return StatusCode(300, "Redirect to two-factor authentication");
        }
        
        var claims = new Dictionary<string, string>
        {
            { "email", user.Email! },
            { "password", formModelData.Password!}
        };
        var tokenModel = await _modalSignIn.SignInAsync(claims);
        
        return Ok(tokenModel);
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

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    {
        return Ok(await _tokenManager.RefreshTokenAsync(tokenModel));
    }

    [HttpPost]
    [Route("sign-out")]
    public async Task<IActionResult> SignOut(TokenModel? tokenModel)
    {
        await _tokenManager.SignOutAsync(tokenModel);
        return Ok("User signed out successfully!");
    }
}
