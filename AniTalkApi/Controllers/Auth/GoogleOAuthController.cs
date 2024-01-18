#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.Filters;
using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.OAuthServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[CustomExceptionFilter]
[Route("/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthSettings

    private readonly GoogleOAuthService _googleOAuthService;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly AuthHelper _authHelper;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuthService,
        ICryptoGeneratorService cryptoGenerator,
        IConfiguration configuration,
        AuthHelper authHelper
        )
    {
        _cryptoGenerator = cryptoGenerator;
        _googleOAuthService = googleOAuthService;
        _configuration = configuration;
        _authHelper = authHelper;
    }

    [HttpGet]
    [Route("google-oauth")]
    public async Task<IActionResult> RedirectOnOAuthServer()
    {
        var scope = _configuration["JWT:Scope"]!;
        var codeVerifier = _cryptoGenerator.GenerateRandomString(64);
        var codeChallenge = Base64UrlEncoder.Encode(SHA256
                .HashData(Encoding.UTF8.GetBytes(codeVerifier)));

        HttpContext.Session.SetString("code_verifier", codeVerifier);

        var url = _googleOAuthService.GetOAuthUrl(scope, codeChallenge);
        return Ok(url);
    }

    [HttpGet]
    [Route("sign-in")]
    public async Task<IActionResult> SignIn([FromQuery] string code)
    {
        var codeVerifier = HttpContext.Session.GetString("code_verifier");

        var idToken = await _googleOAuthService
            .ExchangeCodeToIdTokenAsync(code, codeVerifier!);

        var claims = _tokenHandler.ReadJwtToken(idToken)
            .Claims
            .ToDictionary(keySelector: claim => claim.Type, 
                elementSelector: claim => claim.Value);

        var refreshTokenValidityInDays = int.Parse(_configuration["JWT:RefreshTokenValidityInDays"]!);
        var tokenModel = await _authHelper
            .OAuthSignInAsync(claims, refreshTokenValidityInDays);

        return Ok(tokenModel);
    }
}
