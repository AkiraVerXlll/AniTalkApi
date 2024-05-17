using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.AuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.AuthServices.SignUpServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("api/[controller]")]
public class ManualAuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    private readonly ManualSignInService _manualSignIn;

    private readonly ManualSignUpService _manualSignUp;

    private readonly TwoFactorVerificationService _twoFactorVerification;

    private readonly CookieSettings _cookieSettings;

    public ManualAuthController(
        IOptions<CookieSettings> cookieOptions,
        ManualSignInService manualSignIn,
        ManualSignUpService manualSignUp,
        UserManager<User> userManager,
        TwoFactorVerificationService twoFactorVerification)
    {
        _cookieSettings = cookieOptions.Value;
        _manualSignIn = manualSignIn;
        _manualSignUp = manualSignUp;
        _userManager = userManager;
        _twoFactorVerification = twoFactorVerification;
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

        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        await _manualSignUp.SignUpAsync(claims);
        return Ok();
    }

    [HttpGet]
    [Route("email-exist/{email}")]
    public async Task<IActionResult> IsEmailExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return Ok(user is not null);
    }

    [HttpGet]
    [Route("username-exist/{username}")]
    public async Task<IActionResult> IsUsernameExist(string username)
    {
        var normalizedName = _userManager.KeyNormalizer.NormalizeName(username);
        var result = (await _userManager.FindByNameAsync(username) is not null ||
                      normalizedName.StartsWith("USER-"));
        return Ok(result);
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
        var tokenModel = await _manualSignIn.SignInAsync(claims);
        HttpContext.Response.Cookies.Append(_cookieSettings.AccessToken, tokenModel.AccessToken);
        HttpContext.Response.Cookies.Append(_cookieSettings.RefreshToken, tokenModel.RefreshToken);
        
        return Ok(tokenModel);
    }
}
