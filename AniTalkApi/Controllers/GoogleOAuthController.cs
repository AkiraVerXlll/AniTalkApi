#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.Security.Cryptography;
using System.Text;
using AniTalkApi.ServiceLayer;
using AniTalkApi.ServiceLayer.CryptoGeneratorService;
using Microsoft.AspNetCore.Mvc;

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
        var codeVerifier = _cryptoGenerator.GenerateRandomString(32);
        var codeChallenge = Convert
            .ToBase64String(SHA256
                .HashData(Encoding.UTF8.GetBytes(codeVerifier)));

        HttpContext.Session.SetString("code_verifier", codeVerifier);

        var url = _googleOAuthService.GetGoogleOAuthUrl(scope, codeChallenge);
        return Ok(url);
    }

    [HttpPost]
    [Route("code")]
    public async Task<IActionResult> Code(string code)
    {
        var token = _googleOAuthService
            .ExchangeCodeToToken(code);
        return Ok(token);
    }
}
