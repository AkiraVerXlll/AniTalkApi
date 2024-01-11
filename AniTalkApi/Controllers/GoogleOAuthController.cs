#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Security.Cryptography;
using System.Text;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.OAuthServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.Controllers;

[ApiController]
[Route("/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthService _googleOAuthService;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    private readonly IConfiguration _configuration;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuthService, 
        ICryptoGeneratorService cryptoGenerator,
        IConfiguration configuration)
    {
        _cryptoGenerator = cryptoGenerator;
        _googleOAuthService = googleOAuthService;
        _configuration = configuration;
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
    [Route("code")]
    public async Task<IActionResult> Code([FromQuery]string code)
    {
        var codeVerifier = HttpContext.Session.GetString("code_verifier");

        var token = await _googleOAuthService
            .ExchangeCodeToTokenAsync(code, codeVerifier!);

        return Ok(token);
    }
}
