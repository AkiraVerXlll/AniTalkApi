﻿using AniTalkApi.DataLayer.DTO.Auth;
using AniTalkApi.DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AniTalkApi.DataLayer.Models.Enums;
using AniTalkApi.ServiceLayer.TokenManagerService;

namespace AniTalkApi.Controllers;
[Route("/[controller]")]

[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    private readonly IConfiguration _configuration;

    private readonly ITokenManagerService _tokenManager;

    public AuthController(
        UserManager<User> userManager,
        IConfiguration configuration,
        ITokenManagerService tokenManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenManager = tokenManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> ModalRegister([FromBody] RegisterForm formData)
    {
        if (await _userManager.FindByEmailAsync(formData.Email) is not null)
            return StatusCode(StatusCodes.Status409Conflict, "The user with this email is already exist");

        if (await _userManager.FindByNameAsync(formData.Username) is not null)
            return StatusCode(StatusCodes.Status409Conflict, "The user with this name is already exist");

        User user = new()
        {
            Email = formData.Email,
            UserName = formData.Username,
            DateOfRegistration = DateTime.Now,
            SecurityStamp = Guid.NewGuid().ToString(),
            Status = UserStatus.Online,
            PersonalInformation = new PersonalInformation()
        };
        var result = await _userManager.CreateAsync(user, formData.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError, 
                "User creation failed! Please check user details and try again." );

        return Ok("User created successfully!");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginForm formData)
    {

        var user = formData.Login.Contains('@')
            ? await _userManager.FindByEmailAsync(formData.Login)
            : await _userManager.FindByNameAsync(formData.Login);

        if (user == null || !await _userManager.CheckPasswordAsync(user, formData.Password)) 
            return Unauthorized();

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        authClaims
            .AddRange(userRoles
                .Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var token = _tokenManager.CreateToken(authClaims);
        var refreshToken = _tokenManager.GenerateRefreshToken();

        _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out var refreshTokenValidityInDays);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays).ToUniversalTime();

        await _userManager.UpdateAsync(user);

        return Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = refreshToken,
            Expiration = token.ValidTo
        });
    }

    [HttpPost]
    [Route("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenModel? tokenModel)
    { 
        if (tokenModel is null)
            return BadRequest("Invalid client request");

        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = _tokenManager.GetPrincipalFromExpiredToken(accessToken);
        if (principal == null)
            return BadRequest("Invalid access token or refresh token");

        var username = principal.Identity.Name;

        var user = await _userManager.FindByNameAsync(username);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            return BadRequest("Invalid access token or refresh token");

        var newAccessToken = _tokenManager.CreateToken(principal.Claims.ToList());
        var newRefreshToken = _tokenManager.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new ObjectResult(new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            refreshToken = newRefreshToken
        });
    }
}