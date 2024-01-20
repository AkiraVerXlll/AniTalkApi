#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IdentityModel.Tokens.Jwt;
using AniTalkApi.Filters;
using AniTalkApi.ServiceLayer.AuthServices.OAuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using Microsoft.AspNetCore.Mvc;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("api/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthService _googleOAuth;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly OAuthSignInService _oAuthSignIn;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuth,
        OAuthSignInService oAuthSignIn
        )
    {
        _googleOAuth = googleOAuth;
        _oAuthSignIn = oAuthSignIn;
    }

    [HttpGet]
    [Route("google-oauth")]
    public async Task<IActionResult> RedirectOnOAuthServer()
    {
        var codeVerifier = _googleOAuth.GenerateCodeVerifier();
        var codeChallenge = _googleOAuth.GenerateCodeChallenge(codeVerifier);

        HttpContext.Session.SetString("code_verifier", codeVerifier);

        var url = _googleOAuth.GetOAuthUrl(codeChallenge);
        return Ok(url);
    }

    [HttpGet]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromQuery] string code)
    {
        var codeVerifier = HttpContext.Session.GetString("code_verifier");

        var idToken = await _googleOAuth
            .ExchangeCodeToIdTokenAsync(code, codeVerifier!);

        var claims = _tokenHandler.ReadJwtToken(idToken)
            .Claims
            .ToDictionary(keySelector: claim => claim.Type, 
                elementSelector: claim => claim.Value);

        var tokenModel = await _oAuthSignIn.SignInAsync(claims);

        return Ok(tokenModel);
    }
}
