#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.Helpers;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using AniTalkApi.ServiceLayer.OAuthServices;
using AniTalkApi.ServiceLayer.PhotoServices.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.Controllers.Auth;

[ApiController]
[Route("/[controller]")]
public class GoogleOAuthController : ControllerBase
{
    private readonly GoogleOAuthService _googleOAuthService;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    private readonly IConfiguration _configuration;

    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    private readonly HttpClientHelper _httpClient;

    private readonly IPhotoUploaderService _photoService;

    private readonly UserManager<User> _userManager;

    public GoogleOAuthController(
        GoogleOAuthService googleOAuthService,
        ICryptoGeneratorService cryptoGenerator,
        IConfiguration configuration,
        HttpClientHelper httpClient,
        IPhotoUploaderService photoService,
        UserManager<User> userManager
        )
    {
        _cryptoGenerator = cryptoGenerator;
        _googleOAuthService = googleOAuthService;
        _configuration = configuration;
        _httpClient = httpClient;
        _photoService = photoService;
        _userManager = userManager;
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
    public async Task<IActionResult> Code([FromQuery] string code)
    {
        var codeVerifier = HttpContext.Session.GetString("code_verifier");

        var idToken = await _googleOAuthService
            .ExchangeCodeToIdTokenAsync(code, codeVerifier!);

        var claims = _tokenHandler.ReadJwtToken(idToken)
            .Claims
            .ToDictionary(keySelector: claim => claim.Type, 
                elementSelector: claim => claim.Value);
        
        var imageUrl = claims["picture"];
        var userName = claims["name"];
        

        var user = new User
        {
            Email = claims["email"], 
            UserName = userName,
            PersonalInformation = new PersonalInformation()
            {
                Avatar = new Image()
                {
                    Url = avatarUrl
                },
            },
        };

        await _userManager.CreateAsync(user);

        return Ok(user);
    }
}
