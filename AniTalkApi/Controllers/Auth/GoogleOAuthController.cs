#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Filters;
using AniTalkApi.ServiceLayer.AuthServices.OAuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("api/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthService _googleOAuth;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly OAuthSignInService _oAuthSignIn;

    private readonly CookieSettings _cookieSettings;

    private readonly JwtSettings _jwtSettings;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuth,
        OAuthSignInService oAuthSignIn,
        IOptions<CookieSettings> cookieOptions,
        IOptions<JwtSettings> jwtSettings
        )
    {
        _cookieSettings = cookieOptions.Value;
        _googleOAuth = googleOAuth;
        _oAuthSignIn = oAuthSignIn;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpGet]
    [Route("google-oauth")]
    public async Task<IActionResult> RedirectOnOAuthServer()
    {
        var codeVerifier = _googleOAuth.GenerateCodeVerifier();
        var codeChallenge = _googleOAuth.GenerateCodeChallenge(codeVerifier);

        HttpContext.Response.Cookies.Append(_cookieSettings.CodeVerifier, codeVerifier);

        var url = _googleOAuth.GetOAuthUrl(codeChallenge);
        return Ok(new JsonResult(url));
    }

    [HttpGet]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromQuery] string code)
    {
        var codeVerifier = HttpContext.Request.Cookies[_cookieSettings.CodeVerifier];
        HttpContext.Response.Cookies.Delete(_cookieSettings.CodeVerifier);

        var idToken = await _googleOAuth
            .ExchangeCodeToIdTokenAsync(code, codeVerifier!);

        var claims = _tokenHandler.ReadJwtToken(idToken)
            .Claims
            .ToDictionary(keySelector: claim => claim.Type, 
                elementSelector: claim => claim.Value);

        var tokenModel = await _oAuthSignIn.SignInAsync(claims);
        var cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(_jwtSettings.RefreshTokenValidityInDays)
        };
        HttpContext.Response.Cookies.Append(_cookieSettings.AccessToken, tokenModel.AccessToken, cookieOptions);
        HttpContext.Response.Cookies.Append(_cookieSettings.RefreshToken, tokenModel.RefreshToken, cookieOptions);
        return Ok();
    }
}
