using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.DataLayer.Settings;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.ServiceLayer.TokenManagerServices;

public class BaseTokenManagerService : ITokenManagerService
{
    private readonly JwtSettings _jwtSettings;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    public BaseTokenManagerService(
        IOptions<JwtSettings> jwtSettings,
        ICryptoGeneratorService cryptoGenerator)
    {
        _jwtSettings = jwtSettings.Value;
        _cryptoGenerator = cryptoGenerator;

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

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
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
}