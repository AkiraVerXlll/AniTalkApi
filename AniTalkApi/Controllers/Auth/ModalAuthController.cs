using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer.TokenManagerServices;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[Route("/[controller]")]
public class ModalAuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    private readonly ITokenManagerService _tokenManager;

    private readonly AuthHelper _authHelper;

    public ModalAuthController(
        IConfiguration configuration,
        ITokenManagerService tokenManager,
        AuthHelper authHelper)
    {
        _authHelper = authHelper;
        _configuration = configuration;
        _tokenManager = tokenManager;
    }

    [HttpPost]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] RegisterModel modelData)
    {
        try
        {
            await _authHelper.CreateUserAsync(modelData);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] LoginModel modelData)
    {
        var refreshTokenValidityInDays = int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]!);
        var refreshTokenLength = int.Parse(_configuration["JWT:RefreshTokenLength"]!);
        TokenModel tokenModel;
        try
        {
           tokenModel = await _authHelper.SignInAsync(modelData,
                refreshTokenValidityInDays,
                refreshTokenLength);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        return Ok(tokenModel);
    }

    //[HttpPost]
    //[Route("refresh-token")]
    //public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    //{
    //    if (tokenModel is null)
    //        return BadRequest("Invalid client request");

    //    var accessToken = tokenModel.AccessToken;
    //    var refreshToken = tokenModel.RefreshToken;

    //    var principal = _tokenManager.GetPrincipalFromExpiredToken(accessToken);
    //    if (principal == null)
    //        return BadRequest("Invalid access token or refresh token");

    //    var username = principal.Identity!.Name;

    //    var user = await _userManager.FindByNameAsync(username!);

    //    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
    //        return BadRequest("Invalid access token or refresh token");

    //    var newAccessToken = _tokenManager.GenerateAccessToken(principal.Claims.ToList());
    //    var newRefreshToken = GenerateRefreshToken();

    //    user.RefreshToken = newRefreshToken;
    //    await _userManager.UpdateAsync(user);

    //    return new ObjectResult(new
    //    {
    //        accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
    //        refreshToken = newRefreshToken
    //    });
    //}
}
