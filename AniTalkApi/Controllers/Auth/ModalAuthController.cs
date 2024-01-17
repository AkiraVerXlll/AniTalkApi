using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.Helpers;
using AniTalkApi.DataLayer.Model.Auth;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("/[controller]")]
public class ModalAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly AuthHelper _authHelper;

    public ModalAuthController(
        IConfiguration configuration,
        AuthHelper authHelper)
    {
        _authHelper = authHelper;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterModel modelData)
    {
        await _authHelper
            .CreateModalUserAsync(modelData, int.Parse(_configuration["DefaultAvatarId"]!));
        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginModel modelData)
    {
        var refreshTokenValidityInDays = int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]!);
        
        var tokenModel = await _authHelper.ModalSignInAsync(modelData,
             refreshTokenValidityInDays);

        return Ok(tokenModel);
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
