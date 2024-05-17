using AniTalkApi.DataLayer.DbModels;
using AniTalkApi.DataLayer.Models.Auth;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace AniTalkApi.ServiceLayer.AuthServices;

public class TokenManagerService
{
    private readonly JwtSettings _jwtSettings;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    private readonly UserManager<User> _userManager;

    public TokenManagerService(
        IOptions<JwtSettings> jwtSettings,
        ICryptoGeneratorService cryptoGenerator,
        UserManager<User> userManager)
    {
        _jwtSettings = jwtSettings.Value;
        _cryptoGenerator = cryptoGenerator;
        _userManager = userManager;

    }

    public JwtSecurityToken GenerateAccessToken(User user, IEnumerable<string> roles)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        authClaims
            .AddRange(roles
                .Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        return GenerateAccessToken(authClaims);
    }

    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.Secret!));
        var tokenValidityInMinutes = _jwtSettings.TokenValidityInMinutes;

        return new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }

    public ClaimsPrincipal? GetPrincipalFromToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtSettings.Secret!)),
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg
                .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;

    }

    public string GenerateRefreshToken() => _cryptoGenerator.GenerateRandomString(
               _jwtSettings.RefreshTokenLength);

    public async Task<TokenModel> RefreshTokenAsync(TokenModel? tokenModel)
    {
        if (tokenModel is null)
            throw new ArgumentException("Token model is null");

        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromToken(accessToken);
        if (principal == null)
            throw new ArgumentException("Invalid access token or refresh token");

        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new ArgumentException("Invalid access token or refresh token");

        var newAccessToken = GenerateAccessToken(principal.Claims.ToList());
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenModel()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken,
            ExpiresIn = newAccessToken.ValidTo
        };
    }

    public async Task<TokenModel> SignOutAsync(TokenModel? tokenModel)
    {
        if (tokenModel is null)
            throw new ArgumentException("Token model is null");

        var accessToken = tokenModel.AccessToken;
        var refreshToken = tokenModel.RefreshToken;

        var principal = GetPrincipalFromToken(accessToken);
        if (principal == null)
            throw new ArgumentException("Invalid access token or refresh token");

        var username = principal.Identity!.Name;
        var user = await _userManager.FindByNameAsync(username!);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            throw new ArgumentException("Invalid access token or refresh token");

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = DateTime.Now;
        await _userManager.UpdateAsync(user);

        return new TokenModel()
        {
            AccessToken = null,
            RefreshToken = null,
            ExpiresIn = DateTime.Now
        };
    }
}