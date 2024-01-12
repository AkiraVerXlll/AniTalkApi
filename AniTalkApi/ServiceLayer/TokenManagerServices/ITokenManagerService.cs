using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AniTalkApi.DataLayer.Models;

namespace AniTalkApi.ServiceLayer.TokenManagerServices;

public interface ITokenManagerService
{
    public JwtSecurityToken GenerateAccessToken(User user, IEnumerable<string> roles);

    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> authClaims);

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);

    public string GenerateRefreshToken();
}