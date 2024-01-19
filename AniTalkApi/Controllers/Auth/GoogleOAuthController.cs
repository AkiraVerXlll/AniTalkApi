#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Filters;
using AniTalkApi.ServiceLayer.AuthServices.OAuthServices;
using AniTalkApi.ServiceLayer.AuthServices.SignInServices;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("api/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly ICryptoGeneratorService _cryptoGenerator;
    
    private readonly GoogleOAuthService _googleOAuth;

    private readonly JwtSettings _jwtSettings;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly OAuthSignInService _oAuthSignIn;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuth,
        ICryptoGeneratorService cryptoGenerator,
        IOptions<JwtSettings> options,
        OAuthSignInService oAuthSignIn
        )
    {
        _jwtSettings = options.Value;
        _cryptoGenerator = cryptoGenerator;
        _googleOAuth = googleOAuth;
        _oAuthSignIn = oAuthSignIn;
    }

    [HttpGet]
    [Route("google-oauth")]
    public async Task<IActionResult> RedirectOnOAuthServer()
    {
        var scope = _jwtSettings.Scope!;
        var codeVerifier = _cryptoGenerator.GenerateRandomString(64);
        var codeChallenge = Base64UrlEncoder.Encode(SHA256
                .HashData(Encoding.UTF8.GetBytes(codeVerifier)));

        HttpContext.Session.SetString("code_verifier", codeVerifier);

        var url = _googleOAuth.GetOAuthUrl(scope, codeChallenge);
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
