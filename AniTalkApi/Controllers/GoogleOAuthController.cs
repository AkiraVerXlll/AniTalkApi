#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using AniTalkApi.ServiceLayer;
using Microsoft.AspNetCore.Mvc;

namespace AniTalkApi.Controllers;

[ApiController]
[Route("/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthService _googleOAuthService;

    public GoogleOAuthController(GoogleOAuthService googleOAuthService)
    {
        _googleOAuthService = googleOAuthService;
    }

    [HttpGet]
    [Route("google-oauth")]
    public async Task<IActionResult> RedirectOnOAuthServer()
    {
        var scope = "email profile openid";
        var redirectUrl = "https://localhost:5001/google-oauth/code";
        var codeVerifier = _googleOAuthService.GenerateCodeVerifier();


        var url = _googleOAuthService.GetGoogleOAuthUrl();
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
