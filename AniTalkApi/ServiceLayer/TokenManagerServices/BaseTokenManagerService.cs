using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AniTalkApi.DataLayer.Models;
using AniTalkApi.ServiceLayer.CryptoGeneratorServices;
using Microsoft.IdentityModel.Tokens;

namespace AniTalkApi.ServiceLayer.TokenManagerServices;

public class BaseTokenManagerService : ITokenManagerService
{
    private readonly IConfiguration _configuration;

    private readonly ICryptoGeneratorService _cryptoGenerator;

    public BaseTokenManagerService(
        IConfiguration configuration,
        ICryptoGeneratorService cryptoGenerator)
    {
        _configuration = configuration;
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
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));
        var tokenValidityInMinutes = int.Parse(_configuration["JWT:TokenValidityInMinutes"]!);

        return new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!)),
            ValidIssuer = _configuration["JWT:Issuer"],
            ValidAudience = _configuration["JWT:Audience"],
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
               int.Parse(_configuration["JWT:RefreshTokenLength"]!));
}