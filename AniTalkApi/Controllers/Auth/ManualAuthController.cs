using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.ServiceLayer.AuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.AuthServices.SignUpServices;
using Microsoft.AspNetCore.Identity;

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

    public ManualAuthController(
        ManualSignInService manualSignIn,
        ManualSignUpService manualSignUp,
        UserManager<User> userManager,
        TwoFactorVerificationService twoFactorVerification)
    {
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
        var tokenModel = await _manualSignIn.SignInAsync(claims);
        
        return Ok(tokenModel);
    }
}
