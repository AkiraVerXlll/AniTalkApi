using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.Filters;
using Microsoft.AspNetCore.Mvc;
using AniTalkApi.Helpers;

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
}
